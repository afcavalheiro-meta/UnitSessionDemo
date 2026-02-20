using Moq;
using UnitTestSample.Api.Models;
using UnitTestSample.Api.Services;

namespace TestProject1.Controllers;

[TestFixture]
public class GarageServiceTests
{
    private Mock<IAuthorityVehicleService> _mockAuthorityService;
    private Mock<IVehicle> _mockVehicle;
    private GarageService _garageService;

    [SetUp]
    public void SetUp()
    {
        _mockAuthorityService = new Mock<IAuthorityVehicleService>();
        _mockVehicle = new Mock<IVehicle>();
        _garageService = new GarageService(_mockAuthorityService.Object);
    }

    [Test]
    public void GarageService_Constructor_SetsAuthorityServiceCorrectly()
    {
        // Arrange & Act
        var service = new GarageService(_mockAuthorityService.Object);

        // Assert
        Assert.That(service, Is.Not.Null);
    }

    [Test]
    public void ResetMileage_WhenAuthorized_ResetsMileageToZero()
    {
        // Arrange
        _mockAuthorityService.Setup(x => x.IsAuthorize(_mockVehicle.Object))
                           .Returns(true);
        _mockVehicle.SetupProperty(x => x.Mileage, 100);

        // Act
        _garageService.ResetMileage(_mockVehicle.Object);

        // Assert
        Assert.That(_mockVehicle.Object.Mileage, Is.EqualTo(0));
        _mockAuthorityService.Verify(x => x.IsAuthorize(_mockVehicle.Object), Times.Once);
    }

    [Test]
    public void ResetMileage_WhenNotAuthorized_ThrowsException()
    {
        // Arrange
        _mockAuthorityService.Setup(x => x.IsAuthorize(_mockVehicle.Object))
                           .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() =>
            _garageService.ResetMileage(_mockVehicle.Object));

        Assert.That(exception.Message, Is.EqualTo("Not Allowed to change Mileage"));
        _mockAuthorityService.Verify(x => x.IsAuthorize(_mockVehicle.Object), Times.Once);
    }

    [Test]
    public void ResetMileage_WhenNotAuthorized_DoesNotChangeMileage()
    {
        // Arrange
        _mockAuthorityService.Setup(x => x.IsAuthorize(_mockVehicle.Object))
                           .Returns(false);
        _mockVehicle.SetupProperty(x => x.Mileage, 100);

        // Act & Assert
        Assert.Throws<Exception>(() =>
            _garageService.ResetMileage(_mockVehicle.Object));

        // Verify mileage was not changed
        Assert.That(_mockVehicle.Object.Mileage, Is.EqualTo(100));
    }

    [Test]
    public void ResetMileage_WithNullVehicle_CallsAuthorityService()
    {
        // Arrange
        _mockAuthorityService.Setup(x => x.IsAuthorize(null))
                           .Returns(false);

        // Act & Assert
        Assert.Throws<Exception>(() =>
            _garageService.ResetMileage(null));

        _mockAuthorityService.Verify(x => x.IsAuthorize(null), Times.Once);
    }
}