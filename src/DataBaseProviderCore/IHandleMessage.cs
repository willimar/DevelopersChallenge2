using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProviderCore
{
    public interface IHandleMessage
    {
        string MessageType { get; }
        string Message { get; }
        HandlesCode Code { get; }
    }
}
