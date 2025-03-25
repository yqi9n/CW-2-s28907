using System;

namespace ConsoleApp1
{
    public class OverfillException : Exception
    {
        public OverfillException()
            : base("OverfillException: Cargo exceeds container capacity") { }

        public OverfillException(string message) 
            : base(message){ }
    }
}