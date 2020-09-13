using System.Threading.Tasks;
using AirplaneParkingAsistant.API.Providers;
using AirplaneParkingAsistant.API.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AirplaneParkingAsistant.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingAssistantController : ControllerBase
    {
        private readonly ISlotProvider _recommendedSlotProvider;

        public ParkingAssistantController(ISlotProvider recommendedSlotProvider)
        {
            _recommendedSlotProvider = recommendedSlotProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetRecommendedSlot(GetRecommendedSlotRequest request)
        {
            return Ok(await _recommendedSlotProvider.GetRecommendedSlot(request.Airplane).ConfigureAwait(false));
        }
    }
}
