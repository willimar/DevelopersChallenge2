using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialService.Exceptions
{
    internal class ParserValueException: Exception
    {
        public ParserValueException(string name): base($"No value found to parse '{name}'.")
        {

        }
    }
}
