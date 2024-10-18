namespace PetAdopt.UnitTests.GeneratorBogus;

public class NgoGenerator
{
    private readonly Faker<Ngo> _faker;

    public NgoGenerator()
    {
        _faker = new Faker<Ngo>()
            .RuleFor(ngo => ngo.MainResponsibleId, f => f.Random.Guid())
            .RuleFor(ngo => ngo.MainResponsibleName, f => f.Name.FullName())
            .RuleFor(ngo => ngo.Apresentation, f => f.Lorem.Sentence())
            .RuleFor(ngo => ngo.History, f => f.Lorem.Paragraph())
            .RuleFor(ngo => ngo.CreationDate, f => f.Date.Past());
    }

    public Ngo Generate() => _faker.Generate();
}
