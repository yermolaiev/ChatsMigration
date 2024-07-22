using ChatsMigration.Mapping;
using ChatsMigration.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Npgsql;
using System.Net.Http.Headers;

class Program
{
    private static readonly string PostgresConnectionString = "Host=195.201.30.220;Port=5435;Database=inura;Username=postgres;Password=12345";
    private static readonly string MongoDBConnectionString = "mongodb+srv://Yermolaiev:SOE1q5BJS8ooD9SNxigC1@inura.mongocluster.cosmos.azure.com/?tls=true&authMechanism=SCRAM-SHA-256&retrywrites=false&maxIdleTimeMS=120000";
    private static readonly string OpenAIApiKey = "sk-inura-chattt-d6X4iIRezo8MEqRFYszZT3BlbkFJckmC2B4mT4DRaDS1ACDv";

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

            foreach (var pair in userThreadPairs)
            {
                var (userId, threadId) = pair;
                if (threadId != null)
                {
                    var chats = await GetChatsFromOpenAI(threadId);
                    if (chats.Any())
                    {
                        await SaveChatsToMongoDB(userId, chats);
                    }
                }
            }
            i++;

        } while (dataCount == CounterStepSize);

        Console.WriteLine("Data migration completed.");
        Console.ReadKey();
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

    private static async Task<List<MessageMongoEntity>> GetChatsFromOpenAI(string threadId)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAIApiKey);
        httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
        httpClient.DefaultRequestHeaders.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

        var chats = new List<MessageMongoEntity>();
        var hasMore = true;
        string? afterMessageId = null;
        while (hasMore)
        {
            HttpResponseMessage response;
            if (afterMessageId == null)
            {
                 response = await httpClient.GetAsync($"https://api.openai.com/v1/threads/{threadId}/messages");
                if (!response.IsSuccessStatusCode)
                {
                    break;
                }
            }
            else
            {
                response = await httpClient.GetAsync($"https://api.openai.com/v1/threads/{threadId}/messages?after={afterMessageId}");
                if (!response.IsSuccessStatusCode)
                {
                    break;
                }
            }
        

            var content = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<ListMessagesResponse>(content);
            hasMore = responseModel.HasMore;
            var entities = responseModel.Data.ToEntities();
            afterMessageId = entities.LastOrDefault()?.MessageId;
        }

        Console.WriteLine($"Thread with id: {threadId} saved");

        return chats;
    }

    private static async Task SaveChatsToMongoDB(int userId, List<MessageMongoEntity> chats)
    {
        var client = new MongoClient(MongoDBConnectionString);
        var database = client.GetDatabase("Inura");
        var collection = database.GetCollection<MessageMongoEntity>("MessageMongoEntity");
        chats.ForEach(x => 
        { 
            x.UserId = userId; 
            x.IsActive = true; 
        });
        await collection.InsertManyAsync(chats);
    }
}

