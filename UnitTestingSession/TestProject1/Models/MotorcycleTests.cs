using NUnit.Framework;
using UnitTestSample.Api.Models;

namespace TestProject1.Models;

[TestFixture]
public class MotorcycleTests
{
    [Test]
    public void Motorcycle_Constructor_SetsCorrectInitialValues()
    {
        // Arrange & Act
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);

        // Assert
        Assert.That(motorcycle.FuelLevel, Is.EqualTo(15.0));
        Assert.That(motorcycle.MaxPassengers, Is.EqualTo(2));
        Assert.That(motorcycle.CurrentPassengers, Is.EqualTo(0));
        Assert.That(motorcycle.IsRunning, Is.False);
        Assert.That(motorcycle.LicensePlate, Is.EqualTo("MC-123"));
        Assert.That(motorcycle.EngineType, Is.EqualTo(EngineType.Gasoline));
    }

    [Test]
    public void Motorcycle_Start_WhenHasFuel_StartsSuccessfully()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);

        motorcycle.Start();

        Assert.That(motorcycle.IsRunning, Is.True);
    }

    [Test]
    public void Motorcycle_Start_WhenNoFuel_DoesNotStart()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.ConsumeFuel(15);

        motorcycle.Start();

        Assert.That(motorcycle.IsRunning, Is.False);
    }

    [Test]
    public void Motorcycle_Stop_StopsRunningMotorcycle()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.Start();

        motorcycle.Stop();

        Assert.That(motorcycle.IsRunning, Is.False);
    }

    [Test]
    public void Motorcycle_AddPassenger_IncreasesPassengerCount()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);

        motorcycle.AddPassenger();

        Assert.That(motorcycle.CurrentPassengers, Is.EqualTo(1));
    }

    [Test]
    public void Motorcycle_AddPassenger_CanAddUpTo2Passengers()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);

        motorcycle.AddPassenger();
        motorcycle.AddPassenger();

        Assert.That(motorcycle.CurrentPassengers, Is.EqualTo(2));
    }

    [Test]
    public void Motorcycle_AddPassenger_DoesNotExceedMaxCapacity()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.AddPassenger();
        motorcycle.AddPassenger();

        Assert.Throws<Exception>(() => motorcycle.AddPassenger());
        Assert.That(motorcycle.CurrentPassengers, Is.EqualTo(2));
    }

    [Test]
    public void Motorcycle_RemovePassenger_DecreasesPassengerCount()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.AddPassenger();
        motorcycle.AddPassenger();

        motorcycle.RemovePassenger();

        Assert.That(motorcycle.CurrentPassengers, Is.EqualTo(1));
    }

    [Test]
    public void Motorcycle_RemovePassenger_DoesNotGoBelowZero()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);

        Assert.Throws<Exception>(() => motorcycle.RemovePassenger());
        Assert.That(motorcycle.CurrentPassengers, Is.EqualTo(0));
    }

    [Test]
    public void Motorcycle_Refuel_IncreasesFuelLevel()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.ConsumeFuel(5);

        motorcycle.Refuel(10);

        Assert.That(motorcycle.FuelLevel, Is.EqualTo(20.0));
    }

    [Test]
    public void Motorcycle_ConsumeFuel_WhenRunning_DecreasesFuelLevel()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.Start();

        motorcycle.ConsumeFuel(5);

        Assert.That(motorcycle.FuelLevel, Is.EqualTo(10.0));
        Assert.That(motorcycle.IsRunning, Is.True);
    }

    [Test]
    public void Motorcycle_ConsumeFuel_WhenFuelRunsOut_StopsAutomatically()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);
        motorcycle.Start();

        motorcycle.ConsumeFuel(20);

        Assert.That(motorcycle.FuelLevel, Is.EqualTo(0.0));
        Assert.That(motorcycle.IsRunning, Is.False);
    }

    [Test]
    public void Motorcycle_ConsumeFuel_WhenNotRunning_DoesNotConsumeFuel()
    {
        var motorcycle = new Motorcycle("MC-123", EngineType.Gasoline);

        motorcycle.ConsumeFuel(5);

        Assert.That(motorcycle.FuelLevel, Is.EqualTo(15.0));
    }
}