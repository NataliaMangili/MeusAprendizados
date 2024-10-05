namespace PetAdopt.Domain.ValueObjects;

public class ContactVO
{
    // Construtor protegido, garantindo a imutabilidade do Value Object
    protected ContactVO() { }

    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }

    public string FormatContact => $"{ Name } {Phone} - {Email}";

    public ContactVO(string name, string phone, string email)
    {
        if (string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("At least one information is required.");

        Name = name;
        Phone = phone;
        Email = email;
    }

    // Sobrescreve Equals para comparar os Value Objects
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ContactVO)obj);
    }

    // Sobrescreve GetHashCode para garantir que objetos iguais tenham o mesmo hash
    public override int GetHashCode() => HashCode.Combine(Name, Phone, Email);
}
