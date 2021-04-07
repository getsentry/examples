using System;

namespace AspNetCoreSerilog
{
    public class SpecialException : Exception
    {
        public bool IsSpecial { get; set; } = true;

        public SpecialException(string message)
            : base(message)
        { }
    }
}
