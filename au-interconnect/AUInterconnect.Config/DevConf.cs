using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AUInterconnect.Configuration
{
    public class DevConf
    {
        public const int DebugEventId = 2;
        public const int DebugUserId = 1;
        public const int DebugProposalId = 3;

        public static string DebugEventIdStr
        {
            get { return DebugEventId.ToString(); }
        }
    }
}