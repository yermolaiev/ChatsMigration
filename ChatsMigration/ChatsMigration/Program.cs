using ChatsMigration.Mapping;
using ChatsMigration.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Npgsql;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;

class Program
{
    private static readonly string PostgresConnectionString = "Host=49.13.130.48;Port=5435;Database=inura;Username=postgres;Password=12345";
    private static readonly string MongoDBConnectionString = "mongodb+srv://Yermolaiev:SOE1q5BJS8ooD9SNxigC1@inura.mongocluster.cosmos.azure.com/?tls=true&authMechanism=SCRAM-SHA-256&retrywrites=false&maxIdleTimeMS=120000";
    private static readonly string OpenAIApiKey = "sk-inura-chattt-d6X4iIRezo8MEqRFYszZT3BlbkFJckmC2B4mT4DRaDS1ACDv";

    static int counOfSavedThreads = 1;
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Press any button...");
        Console.ReadKey();
        Console.WriteLine("In progress...");

        const int CounterStepSize = 1000;
        int i = 0;
        int dataCount = 0;

        do
        {
            var userThreadPairs = await GetUserThreadPairsFromPostgres(CounterStepSize, i);
            dataCount = userThreadPairs.Count;

            var batches = SplitList(userThreadPairs.Where(x => x.ThreadId!= null).ToList(), 1);

            var tasks = batches.Select((batch, index) => ProcessBatch(batch)).ToList();
            await Task.WhenAll(tasks);

            i++;

        } while (dataCount == CounterStepSize);

        Console.WriteLine("Data migration completed.");
        Console.ReadKey();
    }

    private static async Task ProcessBatch(List<(int UserId, string ThreadId)> batch)
    {
        foreach (var (userId, threadId) in batch)
        {
            if (threadId != null)
            {
                var chats = await GetChatsFromOpenAIAsync(threadId);
                if (chats.Any())
                {
                    await SaveChatsToMongoDBAsync(userId, chats);
                    Console.WriteLine($"{counOfSavedThreads}) {DateTime.Now.ToString("HH:mm:ss")} Thread with id: {threadId} saved");
                    counOfSavedThreads++;
                }
            }
        }
    }

    private static async Task<List<(int UserId, string ThreadId)>> GetUserThreadPairsFromPostgres(int counterStepSize, int interation)
    {
        var userThreadPairs = new List<(int UserId, string? ThreadId)>();

        using (var connection = new NpgsqlConnection(PostgresConnectionString))
        {
            await connection.OpenAsync();

            using (var command = new NpgsqlCommand($"SELECT \"Id\", \"ThreadId\" FROM public.\"Users\"  Limit {counterStepSize} Offset {interation * counterStepSize}", connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    if (!reader.IsDBNull(1))
                    {
                        var userId = reader.GetInt32(0);
                        string threadId = reader.GetString(1);
                        userThreadPairs.Add((userId, threadId));
                    }
                    else
                    {
                        var userId = reader.GetInt32(0);
                        userThreadPairs.Add((userId, null));
                    }
                }
            }
        }

        return userThreadPairs;
    }

    private static async Task<List<MessageMongoEntity>> GetChatsFromOpenAIAsync(string threadId)
    {
        var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(20);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAIApiKey);
        httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

        var chats = new List<MessageMongoEntity>();
        var hasMore = true;
        string? afterMessageId = null;
        while (hasMore)
        {
            HttpResponseMessage response;
            var isCompleated = false;
            const int MaxCountOfTry = 5;
            int countOfTry = 0;
            while (!isCompleated && countOfTry < MaxCountOfTry)
            {
                countOfTry++;
                try
                {
                    if (afterMessageId == null)
                    {
                        response = await httpClient.GetAsync($"https://api.openai.com/v1/threads/{threadId}/messages");
                        if (!response.IsSuccessStatusCode)
                        {
                            isCompleated = true;
                            break;
                        }
                    }
                    else
                    {
                        response = await httpClient.GetAsync($"https://api.openai.com/v1/threads/{threadId}/messages?after={afterMessageId}");
                        if (!response.IsSuccessStatusCode)
                        {
                            isCompleated = true;
                            break;
                        }
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var responseModel = JsonConvert.DeserializeObject<ListMessagesResponse>(content);
                    hasMore = responseModel.HasMore;
                    var entities = responseModel.Data.ToEntities();
                    chats.AddRange(entities);
                    afterMessageId = entities.LastOrDefault()?.MessageId;
                    isCompleated = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        return chats;
    }

    private static async Task SaveChatsToMongoDBAsync(int userId, List<MessageMongoEntity> chats)
    {
        var client = new MongoClient(MongoDBConnectionString);
        var database = client.GetDatabase("Inura");
        var collection = database.GetCollection<MessageMongoEntity>("MessageMongoEntity");
        chats.ForEach(x => 
        { 
            x.UserId = (int)userId; 
            x.IsActive = true; 
        });

        try
        {
            await collection.InsertManyAsync(chats);
        }
        catch (Exception e)
        {
            try
            {
                foreach (var message in chats)
                {
                    await collection.InsertOneAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private static List<List<T>> SplitList<T>(List<T> list, int n)
    {
        var k = (int)Math.Ceiling(list.Count / (double)n);
        return list.Select((x, i) => new { Index = i, Value = x })
                   .GroupBy(x => x.Index / k)
                   .Select(g => g.Select(x => x.Value).ToList())
                   .ToList();
    }
}

