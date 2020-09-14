using AirplaneParkingAsistant.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        public Task AddSlot(Slot slot)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Slot>> GetAvailableSlots()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsSlotEmpty(int slotId)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAirplaneToSlot(ReservedSlot slot, Airplane airplane)
        {
            throw new System.NotImplementedException();
        }
    }
}
