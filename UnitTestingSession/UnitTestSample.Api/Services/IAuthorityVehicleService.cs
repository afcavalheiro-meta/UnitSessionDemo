using UnitTestSample.Api.Models;

namespace UnitTestSample.Api.Services;

public interface IAuthorityVehicleService
{
    bool IsAuthorize(IVehicle vehicle);

    void InformPolice();
}