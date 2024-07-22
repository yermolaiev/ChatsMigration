using Amazon.Auth.AccessControlPolicy;
using ChatsMigration.Models;
using Riok.Mapperly.Abstractions;

namespace ChatsMigration.Mapping;

[Mapper]
public static partial class AutoMapper
{
    public static partial IEnumerable<MessageMongoEntity> ToEntities(this ICollection<MessageObject> model);
    [MapProperty(nameof(MessageObject.Id), nameof(MessageMongoEntity.MessageId))]
    [MapperIgnoreTarget(nameof(MessageMongoEntity.Id))]
    public static partial MessageMongoEntity ToEntity(this MessageObject model);

    private static List<string> MapFileIds(ICollection<string> FileIds)
    {
        if (FileIds != null)
        {
            var target = new string?[FileIds.Count];
            var i = 0;
            foreach (var item in FileIds)
            {
                target[i] = item == null ? default : item;
                i++;
            }
            return target?.ToList();
        }
        return null;
    }

    private static Content MapCustomField(MessageContentObject source)
    {
        if (source == null)
            return default;
        var target = new Content();
        if (source.Text != null)
        {
            target.Text = MapToText(source.Text);
        }
        if (source.ImageFile != null)
        {
            target.ImageFile = MapToFile(source.ImageFile);
        }
        if (source.FileCitation != null)
        {
            target.FileCitation = MapToFile(source.FileCitation);
        }
        if (source.FilePath != null)
        {
            target.FilePath = MapToFile(source.FilePath);
        }
        target.Type = source.Type;
        return target;
    }

    private static Text MapToText(TextObject source)
    {
        var target = new Text();
        if (source.Annotations != null)
        {
            target.Annotations = MapToIList(source.Annotations);
        }
        target.Value = source.Value == null ? default : source.Value;
        return target;
    }

    private static IList<string?> MapToIList(ICollection<MessageContentTextAnnotationsFileCitationObject> source)
    {
        var target = new global::System.Collections.Generic.List<string?>(source.Count);
        foreach (var item in source)
        {
            target.Add(item == null ? default : item.ToString());
        }
        return target;
    }

    private static ChatsMigration.Models.File MapToFile(FileObject source)
    {
        var target = new ChatsMigration.Models.File();
        target.FileId = source.FileId == null ? default : source.FileId;
        return target;
    }
}
