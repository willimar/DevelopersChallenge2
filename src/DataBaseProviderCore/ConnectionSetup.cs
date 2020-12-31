using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProviderCore
{
    public class ConnectionSetup
    {
        public string UserName { get; set; }
        public string HostInfo { get; set; }
        public int Port { get; set; }
        public string UserPws { get; set; }
        public string DataBaseAuth { get; set; }
        public string DataBaseName { get; set; }
    }
}
