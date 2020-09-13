using System;

namespace AirplaneParkingAsistant.API.Exceptions
{
    public class NoAppropriateAvailableSlotsException : Exception
    {
        public NoAppropriateAvailableSlotsException()
        {
        }

        public NoAppropriateAvailableSlotsException(string message) : base(message)
        {
        }

        public NoAppropriateAvailableSlotsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
