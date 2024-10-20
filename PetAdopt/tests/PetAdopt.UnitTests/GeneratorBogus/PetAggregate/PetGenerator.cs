namespace PetAdopt.UnitTests.GeneratorBogus.PetAggregate;

public sealed class PetGenerator : Faker<Pet>
{
    public PetGenerator()
    {
        //var tagGenerator = new TagGenerator();
        //var petImageGenerator = new PetImageGenerator();

        RuleFor(p => p.NgoId, f => Guid.NewGuid());
        RuleFor(p => p.Name, f => f.Name.FirstName());
        RuleFor(p => p.Species, f => f.PickRandom<SpecieEnum>()); // Espécie aleatória
        RuleFor(p => p.Breed, f => f.PickRandom(new[] { "Labrador", "Poodle", "Bulldog", "Siamese", "Persian", "Golden Retriever" })); // Gera uma raça aleatória
        RuleFor(p => p.About, f => f.Lorem.Paragraph()); // Descrição aleatória
        RuleFor(p => p.Age, f => f.Random.Int(1, 15)); // Idade aleatória
        RuleFor(p => p.IsNeutered, f => f.Random.Bool()); // Define aleatoriamente 
        RuleFor(p => p.HasSpecialNeeds, f => f.Random.Bool()); // Define aleatoriamente 
        RuleFor(p => p.StatusPet, f => f.PickRandom<StatusPetEnum>()); // Gera um status

        //RuleFor(p => p.Tags, f => tagGenerator.Generate(f.Random.Int(1, 5)).ToList());
        //RuleFor(p => p.PetImages, f => petImageGenerator.Generate(f.Random.Int(1, 3)).ToList());
    }
}