using AirplaneParkingAsistant.API.Models;
using System;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Service
{
    public interface ISlotService
    {
        Task<Slot> GetRecommendedSlot(DateTime startTime, int duration, Airplane airplane);
        Task ReserveSlot(ReservedSlot slot, Airplane airplane);
    }
}
