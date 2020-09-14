using System.Threading.Tasks;
using AirplaneParkingAsistant.API.Requests;
using AirplaneParkingAsistant.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace AirplaneParkingAsistant.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingAssistantController : ControllerBase
    {
        private readonly ISlotService _recommendedSlotProvider;

        public ParkingAssistantController(ISlotService recommendedSlotProvider)
        {
            _recommendedSlotProvider = recommendedSlotProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetRecommendedSlot(GetRecommendedSlotRequest request)
        {
            return Ok(await _recommendedSlotProvider.GetRecommendedSlot(request.Airplane).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<ActionResult> ReserveSlot(ReserveSlotRequest request)
        {
            await _recommendedSlotProvider.ReserveSlot(request.Slot, request.Airplane).ConfigureAwait(false);
            return Ok();
        }
    }
}
