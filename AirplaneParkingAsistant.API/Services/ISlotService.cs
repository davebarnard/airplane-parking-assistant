using AirplaneParkingAsistant.API.Models;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Service
{
    public interface ISlotService
    {
        Task<Slot> GetRecommendedSlot(Airplane airplane);
        Task ReserveSlot(ReservedSlot slot, Airplane airplane);
    }
}
