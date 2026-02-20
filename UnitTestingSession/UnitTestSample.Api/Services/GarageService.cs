using UnitTestSample.Api.Models;

namespace UnitTestSample.Api.Services;

public class GarageService : IGarageService
{
    private readonly IAuthorityVehicleService _authorityVehicleService;


    private readonly IDictionary<string, IVehicle> _vehicles = new Dictionary<string, IVehicle>()
    {

    };

    public GarageService(IAuthorityVehicleService authorityVehicleService)
    {
        ArgumentNullException.ThrowIfNull(authorityVehicleService);

        _authorityVehicleService = authorityVehicleService;
    }

    public IVehicle? GetVehicleByLicensePlate(string licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate))
            throw new ArgumentException("Invalid license plate", nameof(licensePlate));

        _vehicles.TryGetValue(licensePlate, out var vehicle);


        return vehicle;
    }
    

    public void ResetMileage(IVehicle vehicle)
    {

        if (vehicle is Bus)
        {
            _authorityVehicleService.InformPolice();
            throw new Exception("Not Allowed to change Mileage");
        }

        if (!_authorityVehicleService.IsAuthorize(vehicle))
            throw new Exception("Not Allowed to change Mileage");

        vehicle.Mileage = 0;
        
    }


    private static double _totalHoursToday = 0;
    private readonly Dictionary<string, int> _visits = new();
    private readonly Random _random = new();

    public string ProcessVehicle(IVehicle vehicle)
    {
        
        double baseHours = 1;

        
        if (vehicle.IssueDescription.Contains("engine"))
        {
            baseHours += 7;
        }

        
        if (vehicle.MaxPassengers > 4)
        {
            baseHours += 3;
        }

        
        if (vehicle.Type == "Bus")
        {
            baseHours += 2;
        }

        
        if (vehicle.EngineType == EngineType.Hybrid)
            baseHours += 1;
        else
            baseHours += 0.5;
            
        

        // Mutating static state
        _totalHoursToday += baseHours;

        // Mutating internal state
        if (_visits.ContainsKey(vehicle.LicensePlate))
            _visits[vehicle.LicensePlate]++;
        else
            _visits[vehicle.LicensePlate] = 1;

        // Side effects
        Console.WriteLine("Vehicle processed: " + vehicle.LicensePlate);
        Console.WriteLine("Passengers: " + vehicle.MaxPassengers);
        Console.WriteLine("Issue: " + vehicle.IssueDescription);

        // Mixing logic + formatting
        return $"Plate {vehicle.LicensePlate} will take {baseHours} hours. " +
               $"Total today: {_totalHoursToday}. " +
               $"Visits: {_visits[vehicle.LicensePlate]}.";
    }
}

public interface IGarageService
{
    IVehicle? GetVehicleByLicensePlate(string licensePlate);
    void ResetMileage(IVehicle vehicle);
    string ProcessVehicle(IVehicle vehicle);
}