namespace PetAdopt.Domain.ValueObjects;

public class AddressVO 
{
    // Construtor protegido, garantindo a imutabilidade do Value Object
    protected AddressVO() { }

    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }

    public string FormatedAddress => $"{Street}, {City} - {PostalCode} - {State}";

    public AddressVO(string street, string city, string state, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required", nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required", nameof(city));

        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("State is required", nameof(state));

        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("PostalCode is required", nameof(postalCode));

        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    // Sobrescreve Equals para comparar os Value Objects
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AddressVO)obj);
    }

    // Sobrescreve GetHashCode para garantir que objetos iguais tenham o mesmo hash
    public override int GetHashCode() => HashCode.Combine(Street, City, State, PostalCode);

}