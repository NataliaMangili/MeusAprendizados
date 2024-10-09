namespace PetAdopt.Domain.AggregatesModels.PetAggregate;

public class Pet : BaseModel
{
    private readonly List<Tag> _tags;
    public virtual IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    private readonly List<PetImage> _petImages;
    public virtual IReadOnlyCollection<PetImage> PetImages => _petImages.AsReadOnly();

    public Pet()
    {
        _tags = new List<Tag>();
        _petImages = new List<PetImage>();
    }

    public Pet(Guid ngoId, string name, SpecieEnum species, string about, string breed, int? age, bool isNeutered, bool hasSpecialNeeds, StatusPetEnum status) : this()
    {
        if (status == StatusPetEnum.Adopted) throw new ArgumentException("Newly created pets cannot have the status 'Adopted'.");

        NgoId = ngoId;
        Name = name;
        Species = species;
        About = about;
        Breed = breed;
        Age = age;
        IsNeutered = isNeutered;
        HasSpecialNeeds = hasSpecialNeeds;
        StatusPet = status;
        //ResponsibleId = userId;
    }

    [ForeignKey("Ngo")]
    public Guid NgoId { get; set; }
    public virtual Ngo Ngo { get; set; }

    public string Name { get; private set; }
    public SpecieEnum Species { get; private set; }
    public string Breed { get; private set; }
    public string About { get; private set; }
    public int? Age { get; private set; }
    public bool IsNeutered { get; private set; }
    public bool HasSpecialNeeds { get; private set; }
    public StatusPetEnum StatusPet { get; private set; }

    public void UpdateIsNeutered() => IsNeutered = true;

    public void AddTags(List<Tag> tags) => _tags.AddRange(tags);
    public void AddImageLinks(List<PetImage> petImages) => _petImages.AddRange(petImages);

    public void RemoveTags(List<Tag> tags)
    {
        if (tags == null || !tags.Any())
            throw new ArgumentException("Tag list cannot be null or empty.");

        foreach (var tag in tags)
        {
            if (_tags.Contains(tag))
                _tags.Remove(tag);
        }
    }

    public void MakeAvailable()
    {
        if (StatusPet == StatusPetEnum.Adopted)
            throw new InvalidOperationException("Adopted pets cannot be made available again.");

        StatusPet = StatusPetEnum.PendingAdoption;
    }
}