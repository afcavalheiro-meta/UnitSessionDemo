namespace UnitTestSample.Api.Models;

public interface IVehicle
{
    bool IsRunning { get; }
    double FuelLevel { get; }
    int MaxPassengers { get; }
    int CurrentPassengers { get; }
    int Mileage { get; set; }

    string LicensePlate { get; }

    string @Type { get; }
}