using AirplaneParkingAsistant.API.Models;
using System;

namespace AirplaneParkingAsistant.API.Requests
{
    public class GetRecommendedSlotRequest
    {
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public Airplane Airplane { get; set; }
    }
}
