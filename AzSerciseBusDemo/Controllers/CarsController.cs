using AzSerciseBusDemo.Models;
using AzSerciseBusDemo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzSerciseBusDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private IserviceBus _serviceBus;
        public CarsController(IserviceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        [HttpPost("orderdetails")]
        public async  Task<IActionResult> OrderDetails(CarDetails carDetails)
        {
            if (carDetails != null)
            {
                await _serviceBus.SendMessageAsync(carDetails);
            }
            return Ok();
        }
        
    }
}
