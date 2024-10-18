using PetAdopt.Domain.AggregatesModels.AdoptionAggregate;
using PetAdopt.Domain.Enums;

namespace PetAdopt.UnitTests.GeneratorBogus.AdoptionAggregate;

public sealed class AdoptionFormGenerator : Faker<AdoptionForm>
{
    public AdoptionFormGenerator()
    {
        var addressGenerator = new VOAddressGenerator();

        RuleFor(a => a.PetId, f => Guid.NewGuid());
        RuleFor(a => a.AdopterId, f => Guid.NewGuid());
        RuleFor(a => a.AdopterName, f => f.Name.FullName());
        RuleFor(a => a.addressVO, f => addressGenerator.Generate());
        RuleFor(a => a.Feedback, f => f.Lorem.Sentence());
        RuleFor(a => a.AdopterContact, f => f.Phone.PhoneNumber());
        RuleFor(a => a.ReasonForAdoption, f => f.Lorem.Paragraph());
        RuleFor(a => a.AdopterHouseholdSize, f => f.Random.Int(1, 10));
        RuleFor(a => a.AdopterHasOtherPets, f => f.Random.Bool());
        RuleFor(a => a.FormStatus, f => f.PickRandom<StatusFormEnum>());
    }
}