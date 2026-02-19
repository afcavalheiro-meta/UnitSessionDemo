namespace ConsoleApp3;

public class GarageService
{
    private readonly IAuthorityVehicleService _authorityVehicleService;

    public GarageService(IAuthorityVehicleService authorityVehicleService)
    {
        ArgumentNullException.ThrowIfNull(authorityVehicleService);

        _authorityVehicleService = authorityVehicleService;
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

public interface IAuthorityVehicleService
{
    bool IsAuthorize(IVehicle vehicle);

    void InformPolice();
}