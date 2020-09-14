using System;

namespace AirplaneParkingAsistant.API.Exceptions
{
    public class SlotAlreadyReservedException : Exception
    {
        public SlotAlreadyReservedException()
        {
        }

        public SlotAlreadyReservedException(string message) : base(message)
        {
        }

        public SlotAlreadyReservedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
