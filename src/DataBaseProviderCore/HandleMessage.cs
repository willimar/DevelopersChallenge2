using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProviderCore
{
    public class HandleMessage : IHandleMessage
    {
        public string MessageType { get; }

        public string Message { get; }

        public HandlesCode Code { get; }

        public HandleMessage(string messageType, string message, HandlesCode code)
        {
            this.MessageType = messageType;
            this.Message = message;
            this.Code = code;
        }

        public HandleMessage(Exception e)
        {
            this.Code = HandlesCode.InternalException;
            this.MessageType = e.GetType().Name;
            this.Message = $"Message: {e.Message} with Stack Trace: {e.StackTrace}";
        }
    }
}
