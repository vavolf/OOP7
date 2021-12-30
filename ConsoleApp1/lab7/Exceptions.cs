using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public class PriceIsLessThanZeroException : ArgumentException
    {
        public PriceIsLessThanZeroException(string message)
            : base(message)
        {

        }
    }
    public class InvalidDocTypeException : ApplicationException
    {
        public InvalidDocTypeException(string message)
            : base(message)
        {

        }
    }
    public class DateException : ApplicationException
    {
        public DateException(string message)
            : base(message)
        {

        }
    }
    public class NullReferenceException : ApplicationException
    {
        public NullReferenceException(string message)
            : base(message)
        {

        }
    }
}
