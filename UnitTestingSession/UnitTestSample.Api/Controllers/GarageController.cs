using Microsoft.AspNetCore.Mvc;
using UnitTestSample.Api.Models;
using UnitTestSample.Api.Services;

namespace UnitTestSample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpGet(Name = "GetVehicleByPlate")]
        public ActionResult<IVehicle> Get(string plate)
        {

            try
            {
                if (string.IsNullOrEmpty(plate))
                    return BadRequest("License plate cannot be null or empty.");


                var vehicle = _garageService.GetVehicleByLicensePlate(plate);

                if (vehicle != null)
                    return Ok(vehicle);

                return NotFound();

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
