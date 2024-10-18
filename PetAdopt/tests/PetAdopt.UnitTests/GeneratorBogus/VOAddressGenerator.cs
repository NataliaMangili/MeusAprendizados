namespace PetAdopt.UnitTests.GeneratorBogus;

public class VOAddressGenerator
{
    private readonly Faker<AddressVO> _faker;

    public VOAddressGenerator()
    {
        _faker = new Faker<AddressVO>()
            .CustomInstantiator(f => new AddressVO(
                f.Address.StreetAddress(),
                f.Address.City(),
                f.Address.State(),
                f.Address.ZipCode()));
    }

    public AddressVO Generate() => _faker.Generate();
}
