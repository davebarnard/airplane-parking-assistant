using AirplaneParkingAsistant.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Repositories
{
    public interface ISlotRepository
    {
        Task<IEnumerable<Slot>> GetAvailableSlots();
        Task AddSlot(Slot slot);
        Task SaveAirplaneToSlot(Slot slot, Airplane airplane);
    }
}
