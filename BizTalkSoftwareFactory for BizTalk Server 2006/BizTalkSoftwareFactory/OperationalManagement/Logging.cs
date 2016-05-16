using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BizTalkSoftwareFactory.OperationalManagement
{
    /// <summary>
    /// Generic logging class 
    /// </summary>
    static class Logging
    {
        /// <summary>
        /// Method for logging messages
        /// </summary>
        /// <param name="className">Class name this was called from</param>
        /// <param name="methodName">Method name this was called from</param>
        /// <param name="msg">Message to log</param>
        public static void LogMessage(string className, string methodName, string msg)
        {
            if(msg != null)
            {
                Trace.WriteLine(string.Format("{0} - {1} : {2}", className, methodName, msg));
            }
        }
    }
}
