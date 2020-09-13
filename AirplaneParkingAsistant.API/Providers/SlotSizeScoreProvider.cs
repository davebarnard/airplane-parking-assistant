using AirplaneParkingAsistant.API.Models;

namespace AirplaneParkingAsistant.API.Providers
{
    public class SlotSizeScoreProvider : ISlotScoreProvider
    {
        public ScoredSlot ScoreSlot(Slot slot, Airplane airplane)
        {
            // Default score for no match.
            int score = 0;

            if (airplane.Type.Size == slot.Size)
            {
                // Arbitrary high score for the best match. Could be increased for a greater range of scores.
                score = 5;
            }
            else if (airplane.Type.Size < slot.Size)
            {
                // Arbitrary low score for an acceptable match.
                score = 1;
            }

            return new ScoredSlot { Slot = slot, Score = score };
        }
    }
}
