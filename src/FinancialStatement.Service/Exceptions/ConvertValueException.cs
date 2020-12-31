using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialService.Exceptions
{
    public class ConvertValueException: Exception
    {
        public ConvertValueException(object value, Type type): base($"Impossible convert value '{value}' to the type {type.Name}.")
        {

        }
    }
}
