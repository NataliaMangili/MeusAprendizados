namespace PetAdopt.UnitTests.Domain.NgoAggregate;

public class NgoTests
{
    private readonly NgoGenerator _ngo;
    private readonly VOAddressGenerator _address;
    private readonly VOContactGenerator _contact;

    public NgoTests()
    {
        // Instancia o gerador 
        _ngo = new NgoGenerator();
        _address = new VOAddressGenerator();
        _contact = new VOContactGenerator();
    }

    [Fact]
    public void Ngo_ShouldInitializeCorrectly()
    {
        // Arrange
        var ngo = _ngo.Generate();

        // Assert
        Assert.NotNull(ngo);
        Assert.NotEqual(Guid.Empty, ngo.MainResponsibleId);
        Assert.False(string.IsNullOrWhiteSpace(ngo.MainResponsibleName));
        Assert.False(string.IsNullOrWhiteSpace(ngo.Apresentation));
        Assert.False(string.IsNullOrWhiteSpace(ngo.History));
        Assert.True(ngo.CreationDate < DateTime.Now);
    }

    [Fact]
    public void ValidateOng_ShouldThrowException_WhenApresentationIsEmpty()
    {
        // Arrange
        var ngo = _ngo.Generate();
        ngo.Apresentation = string.Empty;

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => ngo.ValidateOng());

        // Assert
        Assert.Equal("The NGO must have a apresentation.", exception.Message);
    }


    [Fact]
    public void UpdateMainResponsible_ShouldUpdateMainResponsibleId()
    {
        // Arrange
        var ngo = _ngo.Generate();
        var newResponsibleId = Guid.NewGuid();

        // Act
        ngo.UpdateMainResponsible(newResponsibleId);

        // Assert
        Assert.Equal(newResponsibleId, ngo.MainResponsibleId);
    }

    [Fact]
    public void UpdateApresentation_ShouldUpdateApresentation_WhenValid()
    {
        // Arrange
        var ngo = _ngo.Generate();
        var newApresentation = "New Apresentation";

        // Act
        ngo.UpdateApresentation(newApresentation);

        // Assert
        Assert.Equal(newApresentation, ngo.Apresentation);
    }

    [Fact]
    public void UpdateApresentation_ShouldThrowException_WhenNewApresentationIsEmpty()
    {
        // Arrange
        var ngo = _ngo.Generate();

        // Act
        var exception = Assert.Throws<ArgumentException>(() => ngo.UpdateApresentation(string.Empty));

        // Assert
        Assert.Equal("New Apresentation cannot be null or empty.", exception.Message);
    }

    [Fact]
    public void AddAddress_ShouldAddNgoAddress()
    {
        // Arrange
        var ngo = _ngo.Generate();
        var address = _address.Generate();

        // Act
        ngo.AddAddress(new List<AddressVO> { address });

        // Assert
        Assert.Single(ngo.NgoAddresses);
    }

    [Fact]
    public void AddVolunteersContact_ShouldAddVolunteersContact()
    {
        // Arrange
        var ngo = _ngo.Generate();
        var contact = _contact.Generate();

        // Act
        ngo.AddVolunteersContact(new List<ContactVO> { contact });

        // Assert
        Assert.Single(ngo.VolunteersContacts);
    }

    [Fact]
    public void ValidateOng_ShouldThrowException_WhenApresentationIsNullOrEmpty()
    {
        // Arrange
        var ngo = new Ngo
        {
            Apresentation = string.Empty,
            CreationDate = DateTime.Now
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => ngo.ValidateOng());

        // Assert
        Assert.Equal("The NGO must have a apresentation.", exception.Message);
    }

    [Fact]
    public void ValidateOng_ShouldThrowException_WhenCreationDateIsDefault()
    {
        // Arrange
        var ngo = new Ngo
        {
            Apresentation = "Some presentation",
            CreationDate = default(DateTime)
        };

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => ngo.ValidateOng());

        // Assert
        Assert.Equal("The NGO must have a valid creation date.", exception.Message);
    }


    [Fact]
    public void UpdateApresentation_ShouldThrowException_WhenNewApresentationIsNullOrEmpty()
    {
        // Arrange
        var ngo = new Ngo
        {
            Apresentation = "Original presentation"
        };

        // Act
        var exception = Assert.Throws<ArgumentException>(() => ngo.UpdateApresentation(string.Empty));

        // Assert
        Assert.Equal("New Apresentation cannot be null or empty.", exception.Message);
    }

    [Fact]
    public void UpdateApresentation_ShouldUpdateValue_WhenNewApresentationIsValid()
    {
        // Arrange
        var ngo = new Ngo
        {
            Apresentation = "Original presentation"
        };

        // Act
        ngo.UpdateApresentation("Updated presentation");

        // Assert
        Assert.Equal("Updated presentation", ngo.Apresentation);
    }
}
