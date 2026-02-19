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
}

public interface IGarageService
{
    IVehicle? GetVehicleByLicensePlate(string licensePlate);
    void ResetMileage(IVehicle vehicle);
}