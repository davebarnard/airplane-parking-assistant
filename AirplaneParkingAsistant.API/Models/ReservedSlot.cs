using System;

namespace AirplaneParkingAsistant.API.Models
{
    public class ReservedSlot : Slot
    {
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
