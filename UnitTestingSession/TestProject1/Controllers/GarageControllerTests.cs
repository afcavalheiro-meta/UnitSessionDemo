using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTestSample.Api.Controllers;
using UnitTestSample.Api.Models;
using UnitTestSample.Api.Services;

namespace TestProject1.Controllers;

[TestFixture]
public class GarageControllerTests
{
    private Mock<IGarageService> _mockGarageService;
    private Mock<IVehicle> _mockVehicle;
    private GarageController _garageController;

    [SetUp]
    public void SetUp()
    {
        _mockGarageService = new Mock<IGarageService>();
        _mockVehicle = new Mock<IVehicle>();
        _garageController = new GarageController(_mockGarageService.Object);
    }

    [Test]
    public void Constructor_WithValidGarageService_InitializesController()
    {
        // Arrange & Act
        var controller = new GarageController(_mockGarageService.Object);

        // Assert
        Assert.That(controller, Is.Not.Null);
    }

    [Test]
    public void Get_WithValidPlate_WhenVehicleExists_ReturnsOkWithVehicle()
    {


        // Arrange
        var licensePlate = "ABC123";
        _mockVehicle.Setup(v => v.LicensePlate).Returns(licensePlate);
        _mockGarageService.Setup(s => s.GetVehicleByLicensePlate(licensePlate))
                         .Returns(_mockVehicle.Object);

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(_mockVehicle.Object));
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(licensePlate), Times.Once);
    }

    [Test]
    public void Get_WithValidPlate_WhenVehicleDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var licensePlate = "XYZ789";
        _mockGarageService.Setup(s => s.GetVehicleByLicensePlate(licensePlate))
                         .Returns((IVehicle)null);

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(licensePlate), Times.Once);
    }

    [Test]
    public void Get_WithNullPlate_ReturnsBadRequest()
    {
        // Arrange
        string licensePlate = null;

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.EqualTo("License plate cannot be null or empty."));
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Get_WithEmptyPlate_ReturnsBadRequest()
    {
        // Arrange
        var licensePlate = string.Empty;

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.EqualTo("License plate cannot be null or empty."));
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(It.IsAny<string>()), Times.Never);
    }


    [Test]
    public void Get_WhenServiceThrowsException_ReturnsProblem()
    {
        // Arrange
        var licensePlate = "ABC123";
        var exceptionMessage = "Database connection failed";
        _mockGarageService.Setup(s => s.GetVehicleByLicensePlate(licensePlate))
                         .Throws(new Exception(exceptionMessage));

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        Assert.That(result.Result, Is.TypeOf<ObjectResult>());
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(licensePlate), Times.Once);
    }

    [Test]
    public void Get_WhenServiceThrowsSpecificException_ReturnsProblemWithCorrectMessage()
    {
        // Arrange
        var licensePlate = "ABC123";
        var exceptionMessage = "Specific error occurred";
        _mockGarageService.Setup(s => s.GetVehicleByLicensePlate(licensePlate))
                         .Throws(new InvalidOperationException(exceptionMessage));

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        Assert.That(result.Result, Is.TypeOf<ObjectResult>());
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(licensePlate), Times.Once);
    }

    [TestCase("ABC123")]
    [TestCase("XYZ-789")]
    [TestCase("12-AB-34")]
    [TestCase("TEST001")]
    public void Get_WithVariousValidPlateFormats_CallsServiceCorrectly(string licensePlate)
    {
        // Arrange
        _mockGarageService.Setup(s => s.GetVehicleByLicensePlate(licensePlate))
                         .Returns(_mockVehicle.Object);

        // Act
        var result = _garageController.Get(licensePlate);

        // Assert
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(licensePlate), Times.Once);
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
    }

    [Test]
    public void Get_SuccessfulFlow_VerifiesServiceInteractionOnce()
    {
        // Arrange
        var licensePlate = "VERIFY123";
        _mockGarageService.Setup(s => s.GetVehicleByLicensePlate(licensePlate))
                         .Returns(_mockVehicle.Object);

        // Act
        _garageController.Get(licensePlate);

        // Assert
        _mockGarageService.Verify(s => s.GetVehicleByLicensePlate(licensePlate), Times.Once);
        _mockGarageService.VerifyNoOtherCalls();
    }
}