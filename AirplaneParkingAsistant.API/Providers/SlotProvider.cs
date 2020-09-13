﻿using AirplaneParkingAsistant.API.Exceptions;
using AirplaneParkingAsistant.API.Models;
using AirplaneParkingAsistant.API.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Providers
{
    public class SlotProvider : ISlotProvider
    {
        private readonly ISlotScoreProvider _scoreProvider;
        private readonly ISlotRepository _slotRepository;

        public SlotProvider(ISlotScoreProvider scoreProvider, ISlotRepository slotRepository)
        {
            _scoreProvider = scoreProvider;
            _slotRepository = slotRepository;
        }

        public async Task<Slot> GetRecommendedSlot(Airplane airplane)
        {
            var slots = await _slotRepository.GetAvailableSlots().ConfigureAwait(false);
            if (!slots.Any()) throw new NoAvailableSlotsException();

            var scoredSlots = slots.Select(x => _scoreProvider.ScoreSlot(x, airplane));
            if (!scoredSlots.Any(s => s.Score > 0)) throw new NoAppropriateAvailableSlotsException();

            return scoredSlots.OrderByDescending(x => x.Score).First().Slot;
        }

        public async Task ReserveSlot(Slot slot, Airplane airplane)
        {
            await _slotRepository.SaveAirplaneToSlot(slot, airplane).ConfigureAwait(false);
        }
    }
}
