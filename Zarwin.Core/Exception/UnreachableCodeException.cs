using System;
using System.Runtime.Serialization;

namespace Zarwin.Core.Engine
{
    [Serializable]
    internal class UnreachableCodeException : Exception
    {
        public UnreachableCodeException()
        {
        }

        public UnreachableCodeException(string message) : base(message)
        {
        }

        public UnreachableCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnreachableCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}