﻿using AirplaneParkingAsistant.API.Models;
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

        public Task SaveAirplaneToSlot(Slot slot, Airplane airplane)
        {
            throw new System.NotImplementedException();
        }
    }
}
