namespace PetAdopt.UnitTests.GeneratorBogus.AdoptionAggregate;

public sealed class AdoptionGenerator : Faker<Adoption>
{
    public AdoptionGenerator()
    {
        RuleFor(a => a.PetId, f => Guid.NewGuid());
        RuleFor(a => a.AdopterId, f => Guid.NewGuid());
        RuleFor(a => a.AdoptionDate, f => f.Date.Past(1)); // data de adoção no último ano

        RuleFor(a => a.Pet, f => new PetGenerator().Generate());
    }
}