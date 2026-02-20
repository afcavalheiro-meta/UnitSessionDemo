namespace UnitTestSample.Api.Models;

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
    public string LicensePlate { get; }

    public virtual string @Type => GetType().Name;
    public string IssueDescription { get; }

    public EngineType EngineType { get; protected set; }


    public Vehicle(double initialFuel, int maxPassengers, string licensePlate, EngineType engineType)
    {
        FuelLevel = initialFuel;
        MaxPassengers = maxPassengers;
        IsRunning = false;
        CurrentPassengers = 0;
        LicensePlate = licensePlate;
        EngineType = engineType; 
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
    public virtual void ConsumeFuel(double amount)
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
    public Motorcycle(string licensePlate, EngineType engineType) : base(15, 2, licensePlate, engineType)
    {

    }
}

public class Car : Vehicle
{
    public Car(string licensePlate, EngineType engineType) : base(15, 5, licensePlate, engineType)
    {

    }
}

public class Van : Vehicle
{
    public Van(string licensePlate, EngineType engineType) : base(15, 9, licensePlate, engineType)
    {

    }
}


public class Bus : Vehicle
{
    public Bus(string licensePlate, EngineType engineType) : base(15, 50, licensePlate, engineType)
    {

    }

    public Bus(string licensePlate) : base(15, 50, licensePlate, EngineType.Diesel)
    {

    }
}
