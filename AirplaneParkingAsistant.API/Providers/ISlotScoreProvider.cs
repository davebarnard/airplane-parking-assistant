using AirplaneParkingAsistant.API.Models;

namespace AirplaneParkingAsistant.API.Providers
{
    public interface ISlotScoreProvider
    {
        ScoredSlot ScoreSlot(Slot slot, Airplane airplane);
    }
}
