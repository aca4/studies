using System;

namespace MerchantsGuideToGalaxy.Exceptions
{
    public class InvalidRomanNumeralException : Exception
    {
        public InvalidRomanNumeralException()
        {

        }

        public InvalidRomanNumeralException(string message) : base(message)
        {
            
        }
    }
}
