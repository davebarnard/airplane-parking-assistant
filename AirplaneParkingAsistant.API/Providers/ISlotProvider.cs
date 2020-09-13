using AirplaneParkingAsistant.API.Models;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Providers
{
    public interface ISlotProvider
    {
        Task<Slot> GetRecommendedSlot(Airplane airplane);
        Task ReserveSlot(Slot slot, Airplane airplane);
    }
}
