using System;
using System.Runtime.Serialization;

namespace PokédexLib.Exceptions
{
    [Serializable]
    public class TeamIsFullException : Exception
    {
        public TeamIsFullException()
        {
        }

        public TeamIsFullException(string message) : base(message)
        {
        }

        public TeamIsFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeamIsFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
