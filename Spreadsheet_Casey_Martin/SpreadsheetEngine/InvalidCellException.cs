using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class InvalidCellException : Exception
    {
        public InvalidCellException() { }

        public InvalidCellException(string message) : base(message)
        {
        }

        public InvalidCellException(string message, Exception innerException) : base(message, innerException) 
        {
        }

    }
}
