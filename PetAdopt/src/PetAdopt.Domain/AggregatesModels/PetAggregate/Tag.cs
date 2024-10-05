namespace PetAdopt.Domain.AggregatesModels.PetAggregate;

public class Tag : BaseModel
{
    public string Text { get; set; }

    public static Tag NewTag(string value) => new Tag { Text = value.ToUpper() };
}
