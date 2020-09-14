using System;

namespace AirplaneParkingAsistant.API.Models
{
    public class ReservedSlot
    {
        public Slot Slot { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
