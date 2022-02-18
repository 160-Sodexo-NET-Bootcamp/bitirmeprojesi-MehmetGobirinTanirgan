using PCS.Core.CustomExceptions.Abstract;
using System;

namespace PCS.Core.CustomExceptions
{
    public class ConflictException : Exception, IClientSideException
    {
        public ConflictException()
        {

        }
        public ConflictException(string message) : base(message)
        {

        }
    }
}
