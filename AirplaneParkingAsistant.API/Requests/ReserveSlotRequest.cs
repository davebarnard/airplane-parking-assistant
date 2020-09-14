using AirplaneParkingAsistant.API.Models;
using System;

namespace AirplaneParkingAsistant.API.Requests
{
    public class ReserveSlotRequest
    {
        public Airplane Airplane { get; set; }
        public ReservedSlot Slot { get; set; }
    }
}
