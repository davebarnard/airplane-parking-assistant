using System;

namespace AirplaneParkingAsistant.API.Exceptions
{
    public class NoAvailableSlotsException : Exception
    {
        public NoAvailableSlotsException()
        {
        }

        public NoAvailableSlotsException(string message) : base(message)
        {
        }

        public NoAvailableSlotsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
