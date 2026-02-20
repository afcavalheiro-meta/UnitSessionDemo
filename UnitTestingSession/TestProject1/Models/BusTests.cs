using NUnit.Framework;
using UnitTestSample.Api.Models;

namespace TestProject1.Models;

[TestFixture]
public class BusTests
{
    [Test]
    public void Bus_Constructor_SetsCorrectInitialValues()
    {
        // Arrange & Act
        var bus = new Bus("AA-BB-CC");

        // Assert
        Assert.That(bus.FuelLevel, Is.EqualTo(15.0));
        Assert.That(bus.MaxPassengers, Is.EqualTo(50));
        Assert.That(bus.CurrentPassengers, Is.EqualTo(0));
        Assert.That(bus.IsRunning, Is.False);
    }

    [Test]
    public void Bus_Start_WhenHasFuel_StartsSuccessfully()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");

        // Act
        bus.Start();

        // Assert
        Assert.That(bus.IsRunning, Is.True);
    }

    [Test]
    public void Bus_Start_WhenNoFuel_DoesNotStart()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        bus.ConsumeFuel(15); // Consume all fuel

        // Act
        bus.Start();

        // Assert
        Assert.That(bus.IsRunning, Is.False);
    }

    [Test]
    public void Bus_Stop_StopsRunningBus()
        
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        bus.Start();

        // Act
        bus.Stop();

        // Assert
        Assert.That(bus.IsRunning, Is.False);
    }

    [Test]
    public void Bus_AddPassenger_IncreasesPassengerCount()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");

        // Act
        bus.AddPassenger();

        // Assert
        Assert.That(bus.CurrentPassengers, Is.EqualTo(1));
    }

    [Test]
    public void Bus_AddPassenger_CanAddUpTo50Passengers()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");

        // Act
        for (int i = 0; i < 50; i++)
        {
            bus.AddPassenger();
        }

        // Assert
        Assert.That(bus.CurrentPassengers, Is.EqualTo(50));
    }

    [Test]
    public void Bus_AddPassenger_DoesNotExceedMaxCapacity()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        for (int i = 0; i < 50; i++)
        {
            bus.AddPassenger();
        }

        // Act
        bus.AddPassenger(); // Try to add 51st passenger

        // Assert
        Assert.That(bus.CurrentPassengers, Is.EqualTo(50));
    }

    [Test]
    public void Bus_RemovePassenger_DecreasesPassengerCount()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        bus.AddPassenger();
        bus.AddPassenger();

        // Act
        bus.RemovePassenger();

        // Assert
        Assert.That(bus.CurrentPassengers, Is.EqualTo(1));
    }

    [Test]
    public void Bus_RemovePassenger_DoesNotGoBelowZero()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");

        // Act
        bus.RemovePassenger();

        // Assert
        Assert.That(bus.CurrentPassengers, Is.EqualTo(0));
    }

    [Test]
    public void Bus_Refuel_IncreasesFuelLevel()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        bus.ConsumeFuel(5);

        // Act
        bus.Refuel(10);

        // Assert
        Assert.That(bus.FuelLevel, Is.EqualTo(20.0));
    }

    [Test]
    public void Bus_ConsumeFuel_WhenRunning_DecreasesFuelLevel()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        bus.Start();

        // Act
        bus.ConsumeFuel(5);

        // Assert
        Assert.That(bus.FuelLevel, Is.EqualTo(10.0));
        Assert.That(bus.IsRunning, Is.True);
    }

    [Test]
    public void Bus_ConsumeFuel_WhenFuelRunsOut_StopsAutomatically()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");
        bus.Start();

        // Act
        bus.ConsumeFuel(20); // Consume more than available

        // Assert
        Assert.That(bus.FuelLevel, Is.EqualTo(0.0));
        Assert.That(bus.IsRunning, Is.False);
    }

    [Test]
    public void Bus_ConsumeFuel_WhenNotRunning_DoesNotConsumeFuel()
    {
        // Arrange
        var bus = new Bus("AA-BB-CC");

        // Act
        bus.ConsumeFuel(5);

        // Assert
        Assert.That(bus.FuelLevel, Is.EqualTo(15.0));
    }
}