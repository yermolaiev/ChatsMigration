namespace ChatsMigration.Models;

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public class ListMessagesResponse
{
    [Newtonsoft.Json.JsonProperty("object", Required = Newtonsoft.Json.Required.Default)]
    public string Object { get; set; }

    [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.Default)]
    public System.Collections.Generic.ICollection<MessageObject> Data { get; set; } = new System.Collections.ObjectModel.Collection<MessageObject>();

    [Newtonsoft.Json.JsonProperty("first_id", Required = Newtonsoft.Json.Required.Default)]
    public string FirstId { get; set; }

    [Newtonsoft.Json.JsonProperty("last_id", Required = Newtonsoft.Json.Required.Default)]
    public string LastId { get; set; }

    [Newtonsoft.Json.JsonProperty("has_more", Required = Newtonsoft.Json.Required.Default)]
    public bool HasMore { get; set; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    public string ToJson()
    {

        return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

    }
    public static ListMessagesResponse FromJson(string data)
    {

        return Newtonsoft.Json.JsonConvert.DeserializeObject<ListMessagesResponse>(data, new Newtonsoft.Json.JsonSerializerSettings());

    }


}

/// <summary>
/// Represents a message within a [thread](/docs/api-reference/threads).
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public partial class MessageObject
{
    /// <summary>
    /// The identifier, which can be referenced in API endpoints.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Default)]
    public string Id { get; set; }

    /// <summary>
    /// The object type, which is always `thread.message`.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("object", Required = Newtonsoft.Json.Required.Default)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public MessageObjectObject Object { get; set; }

    /// <summary>
    /// The Unix timestamp (in seconds) for when the message was created.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("created_at", Required = Newtonsoft.Json.Required.Default)]
    public int CreatedAt { get; set; }

    /// <summary>
    /// The [thread](/docs/api-reference/threads) ID that this message belongs to.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("thread_id", Required = Newtonsoft.Json.Required.Default)]
    public string ThreadId { get; set; }

    /// <summary>
    /// The entity that produced the message. One of `user` or `assistant`.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("role", Required = Newtonsoft.Json.Required.Default)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public MessageObjectRole Role { get; set; }

    /// <summary>
    /// The content of the message in array of text and/or images.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("content", Required = Newtonsoft.Json.Required.Default)]
    public System.Collections.Generic.ICollection<MessageContentObject> Content { get; set; } = new System.Collections.ObjectModel.Collection<MessageContentObject>();

    /// <summary>
    /// If applicable, the ID of the [assistant](/docs/api-reference/assistants) that authored this message.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("assistant_id", Required = Newtonsoft.Json.Required.AllowNull)]
    public string AssistantId { get; set; }

    /// <summary>
    /// If applicable, the ID of the [run](/docs/api-reference/runs) associated with the authoring of this message.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("run_id", Required = Newtonsoft.Json.Required.AllowNull)]
    public string RunId { get; set; }

    /// <summary>
    /// A list of [file](/docs/api-reference/files) IDs that the assistant should use. Useful for tools like retrieval and code_interpreter that can access files. A maximum of 10 files can be attached to a message.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("file_ids", Required = Newtonsoft.Json.Required.Default)]
    public System.Collections.Generic.ICollection<string> FileIds { get; set; } = new System.Collections.ObjectModel.Collection<string>();

    /// <summary>
    /// Set of 16 key-value pairs that can be attached to an object. This can be useful for storing additional information about the object in a structured format. Keys can be a maximum of 64 characters long and values can be a maxium of 512 characters long.
    /// <br/>
    /// </summary>
    [Newtonsoft.Json.JsonProperty("metadata", Required = Newtonsoft.Json.Required.AllowNull)]
    public object Metadata { get; set; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    public string ToJson()
    {

        return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

    }
    public static MessageObject FromJson(string data)
    {

        return Newtonsoft.Json.JsonConvert.DeserializeObject<MessageObject>(data, new Newtonsoft.Json.JsonSerializerSettings());

    }

}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public enum MessageObjectRole
{

    [System.Runtime.Serialization.EnumMember(Value = @"user")]
    User = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"assistant")]
    Assistant = 1,

}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public partial class MessageContentObject
{
    [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default)]
    public MessageContentType Type { get; set; }

    [Newtonsoft.Json.JsonProperty("image_file", Required = Newtonsoft.Json.Required.Default)]
    public FileObject ImageFile { get; set; }

    [Newtonsoft.Json.JsonProperty("text", Required = Newtonsoft.Json.Required.Default)]
    public TextObject Text { get; set; }

    [Newtonsoft.Json.JsonProperty("file_citation", Required = Newtonsoft.Json.Required.Default)]
    public FileObject FileCitation { get; set; }

    [Newtonsoft.Json.JsonProperty("file_path", Required = Newtonsoft.Json.Required.Default)]
    public FileObject FilePath { get; set; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    public string ToJson()
    {

        return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

    }
    public static MessageContentObject FromJson(string data)
    {

        return Newtonsoft.Json.JsonConvert.DeserializeObject<MessageContentObject>(data, new Newtonsoft.Json.JsonSerializerSettings());

    }
}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public partial class FileObject
{
    /// <summary>
    /// The ID of the specific File the citation is from.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("file_id", Required = Newtonsoft.Json.Required.Default)]
    public string FileId { get; set; }

    /// <summary>
    /// The specific quote in the file.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("quote", Required = Newtonsoft.Json.Required.Default)]
    public string Quote { get; set; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    public string ToJson()
    {

        return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

    }
    public static FileObject FromJson(string data)
    {

        return Newtonsoft.Json.JsonConvert.DeserializeObject<FileObject>(data, new Newtonsoft.Json.JsonSerializerSettings());

    }

}

public enum MessageContentType
{

    [System.Runtime.Serialization.EnumMember(Value = @"text")]
    Text = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"file_citation")]
    FileCitation = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"file_path")]
    FilePath = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"image_file")]
    ImageFile = 3,
}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public class TextObject
{
    /// <summary>
    /// The data that makes up the text.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default)]
    public string Value { get; set; }

    [Newtonsoft.Json.JsonProperty("annotations", Required = Newtonsoft.Json.Required.Default)]
    public System.Collections.Generic.ICollection<MessageContentTextAnnotationsFileCitationObject> Annotations { get; set; } = new System.Collections.ObjectModel.Collection<MessageContentTextAnnotationsFileCitationObject>();

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    public string ToJson()
    {

        return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

    }
    public static TextObject FromJson(string data)
    {

        return Newtonsoft.Json.JsonConvert.DeserializeObject<TextObject>(data, new Newtonsoft.Json.JsonSerializerSettings());

    }

}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public partial class MessageContentTextAnnotationsFileCitationObject
{
    /// <summary>
    /// Always `file_citation`.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public MessageContentTextAnnotationsFileCitationObjectType Type { get; set; }

    /// <summary>
    /// The text in the message content that needs to be replaced.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("text", Required = Newtonsoft.Json.Required.Default)]
    public string Text { get; set; }

    [Newtonsoft.Json.JsonProperty("file_citation", Required = Newtonsoft.Json.Required.Default)]
    public FileObject File_citation { get; set; } = new FileObject();

    [Newtonsoft.Json.JsonProperty("start_index", Required = Newtonsoft.Json.Required.Default)]
    public int Start_index { get; set; }

    [Newtonsoft.Json.JsonProperty("end_index", Required = Newtonsoft.Json.Required.Default)]
    public int End_index { get; set; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    public string ToJson()
    {

        return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

    }
    public static MessageContentTextAnnotationsFileCitationObject FromJson(string data)
    {

        return Newtonsoft.Json.JsonConvert.DeserializeObject<MessageContentTextAnnotationsFileCitationObject>(data, new Newtonsoft.Json.JsonSerializerSettings());

    }

}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public enum MessageContentTextAnnotationsFileCitationObjectType
{

    [System.Runtime.Serialization.EnumMember(Value = @"file_citation")]
    File_citation = 0,

}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.3.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
public enum MessageObjectObject
{

    [System.Runtime.Serialization.EnumMember(Value = @"thread.message")]
    Thread_message = 0,

}
