using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class ServerConfig
    {
        //匹配倒计时 15s
        public const int ConfirmCountDown = 15;

        public const string LocalDevInnerIP = "127.0.0.1";
        public const int UdpPort = 17666;
    }
}
