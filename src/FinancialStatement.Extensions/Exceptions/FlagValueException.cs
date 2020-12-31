using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialExtensions.Exceptions
{
    public class FlagValueException: Exception
    {
        public FlagValueException(string name): base($"Invalid value to Flag '{name}' in parser string.")
        {

        }
    }
}
