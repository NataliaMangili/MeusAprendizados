using Microsoft.Extensions.Logging;
using PetAdopt.Application.CommandQuerys.CreateNgo;
using PetAdopt.Domain.Aggregates.NgoAggregate;
using PetAdopt.Domain.Interfaces;
using PetAdopt.Domain.ValueObjects;

namespace PetAdopt.IntegrationTests.Handlers.NGO;

//public class CreateNgoHandlerTests
//{
//    private readonly Mock<INgoRepository> _ngoRepositoryMock;
//    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//    private readonly Mock<IMapper> _mapperMock;
//    private readonly Mock<ILogger<CreateNgoCommandHandler>> _loggerMock;
//    private readonly CreateNgoCommandHandler _handler;

//    public CreateNgoCommandHandlerTests()
//    {
//        _ngoRepositoryMock = new Mock<INgoRepository>();
//        _unitOfWorkMock = new Mock<IUnitOfWork>();
//        _mapperMock = new Mock<IMapper>();
//        _loggerMock = new Mock<ILogger<CreateNgoCommandHandler>>();

//        _handler = new CreateNgoCommandHandler(
//            _ngoRepositoryMock.Object,
//            _unitOfWorkMock.Object,
//            _mapperMock.Object,
//            _loggerMock.Object);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnSuccess_WhenNgoIsCreatedAndSaved()
//    {
//        // Arrange
//        var command = new CreateNgoCommand
//        {
//            request = new NgoRequest
//            {
//                address = new List<AddressVO>(), // Preencha com dados de teste conforme necessário
//                contacts = new List<ContactVO>(), // Preencha com dados de teste conforme necessário
//            }
//        };

//        var newNgo = new Ngo(); // Simule um novo Ngo

//        _mapperMock.Setup(m => m.Map<Ngo>(command.request)).Returns(newNgo);
//        _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(3); // Simula que 3 registros foram salvos

//        // Act
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert
//        Assert.True(result.IsSuccess);
//        _ngoRepositoryMock.Verify(r => r.Add(newNgo), Times.Once);
//        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnFailure_WhenNoChangesDetected()
//    {
//        // Arrange
//        var command = new CreateNgoCommand
//        {
//            request = new NgoRequest
//            {
//                address = new List<AddressVO>(),
//                contacts = new List<ContactVO>(),
//            }
//        };

//        var newNgo = new Ngo();

//        _mapperMock.Setup(m => m.Map<Ngo>(command.request)).Returns(newNgo);
//        _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1); // Simula que não houve alterações

//        // Act
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert
//        Assert.False(result.IsSuccess);
//        Assert.Equal("No changes were detected.", result.Error);
//        _ngoRepositoryMock.Verify(r => r.Add(newNgo), Times.Once);
//        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
//    }

//    [Fact]
//    public async Task Handle_ShouldLogErrorAndThrow_WhenExceptionOccurs()
//    {
//        // Arrange
//        var command = new CreateNgoCommand
//        {
//            request = new NgoRequest
//            {
//                address = new List<AddressVO>(),
//                contacts = new List<ContactVO>(),
//            }
//        };

//        var newNgo = new Ngo();

//        _mapperMock.Setup(m => m.Map<Ngo>(command.request)).Returns(newNgo);
//        _unitOfWorkMock.Setup(u => u.CommitAsync()).ThrowsAsync(new Exception("DB error"));

//        // Act & Assert
//        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
//        Assert.Equal("Unexpected error occurred while attempting to save Ngo.", exception.Message);
//        _loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
//    }
//}
