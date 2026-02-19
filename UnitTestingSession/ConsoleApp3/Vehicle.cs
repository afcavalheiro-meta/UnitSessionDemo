namespace ConsoleApp3;


public interface IVehicle
{
        bool IsRunning { get; }
        double FuelLevel { get; }
        int MaxPassengers { get; }
        int CurrentPassengers { get; }
        int Mileage { get; set; }
}

public enum EngineType
{
    Gasoline,
    Diesel,
    Electric,
    Hybrid
}


public abstract class Vehicle : IVehicle
{
    // State
    public bool IsRunning { get; private set; }
    public double FuelLevel { get; private set; }      
    public int MaxPassengers { get; }                 
    public int CurrentPassengers { get; private set; }
    public int Mileage { get; set; } 

    public Vehicle(double initialFuel, int maxPassengers)
    {
        FuelLevel = initialFuel;
        MaxPassengers = maxPassengers;
        IsRunning = false;
        CurrentPassengers = 0;
    }

    public void Start()
    {
        if (!IsRunning && FuelLevel > 0)
        {
            IsRunning = true;
        }
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void AddPassenger()
    {
        if (CurrentPassengers < MaxPassengers)
        {
            CurrentPassengers++;
        }
        else
        {
            throw new Exception("There is no space for more passengers");
        }
    }

    public void RemovePassenger()
    {
        if (CurrentPassengers > 0)
        {
            CurrentPassengers--;
        }
        else
        {
            throw new Exception("There are no more passengers to remove");
        }
        
    }

    public void Refuel(double amount)
    {
        if (amount > 0)
        {
            FuelLevel += amount;
        }
    }

    // Example of fuel usage when running
    public void ConsumeFuel(double amount)
    {
        if (IsRunning && amount > 0)
        {
            FuelLevel -= amount;
            if (FuelLevel <= 0)
            {
                FuelLevel = 0;
                Stop();
            }
        }
    }
}




public class Motorcycle : Vehicle
{
    public Motorcycle() : base(15, 2)
    {

    }
}

public class Car : Vehicle
{
    public Car() : base(15, 5)
    {

    }
}

public class Van : Vehicle
{
    public Van() : base(15, 9)
    {

    }
}


public class Bus : Vehicle
{
    public Bus() : base(15, 50)
    {

    }
}
