namespace PetAdopt.UnitTests.GeneratorBogus;

public class VOContactGenerator
{
    private readonly Faker<ContactVO> _faker;

    public VOContactGenerator()
    {
        _faker = new Faker<ContactVO>()
            .CustomInstantiator(f => new ContactVO(
                f.Name.FullName(),
                f.Phone.PhoneNumber(),
                f.Internet.Email()));
    }

    public ContactVO Generate() => _faker.Generate();
}