using AirplaneParkingAsistant.API.Exceptions;
using AirplaneParkingAsistant.API.Models;
using AirplaneParkingAsistant.API.Providers;
using AirplaneParkingAsistant.API.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirplaneParkingAsistant.API.Service
{
    public class SlotService : ISlotService
    {
        private readonly ISlotScoreProvider _scoreProvider;
        private readonly ISlotRepository _slotRepository;

        public SlotService(ISlotScoreProvider scoreProvider, ISlotRepository slotRepository)
        {
            _scoreProvider = scoreProvider;
            _slotRepository = slotRepository;
        }

        public async Task<Slot> GetRecommendedSlot(DateTime startTime, int duration, Airplane airplane)
        {
            var slots = await _slotRepository.GetAvailableSlots().ConfigureAwait(false);
            if (!slots.Any()) throw new NoAvailableSlotsException();

            var scoredSlots = slots.Select(x => _scoreProvider.ScoreSlot(x, airplane));
            if (!scoredSlots.Any(s => s.Score > 0)) throw new NoAppropriateAvailableSlotsException();

            return scoredSlots.OrderByDescending(x => x.Score).First();
        }

        public async Task ReserveSlot(ReservedSlot slotReservation, Airplane airplane)
        {
            var isAvailable = await _slotRepository.IsSlotEmpty(slotReservation.SlotId).ConfigureAwait(false);
            if (!isAvailable) throw new SlotAlreadyReservedException();

            await _slotRepository.SaveAirplaneToSlot(slotReservation, airplane).ConfigureAwait(false);
        }
    }
}
