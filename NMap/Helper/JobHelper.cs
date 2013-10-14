using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRPlugIn;

namespace NMap.Helper
{
    public static class JobHelper
    {
        public static IList<IFlawTypeName> FlawTypes { set; get; }
        public static IList<ILaneInfo> Lanes { set; get; }
        public static IList<ISeverityInfo> SeverityInfo { set; get; }
        public static IJobInfo JobInfo { set; get; }
        public static bool IsOnline { set; get; }
        public static bool IsOpenHistory { set; get; }
        public static string DbConnectString { set; get; }
        public static int JobKey { set; get; }

        static JobHelper()
        {
            // Default
            IsOnline = false;
            IsOpenHistory = false;
            DbConnectString = "";
            JobKey = 0;
        }
    }
}
