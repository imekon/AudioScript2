using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioScript.Logging
{
    public static class Logger
    {
        public static void Debug(string message)
        {
            Trace.WriteLine(message);
        }

        public static void Info(string message)
        {
            Trace.WriteLine(message);
        }

        public static void Warning(string message)
        {
            Trace.WriteLine(message);
        }

        public static void Error(string message)
        {
            Trace.WriteLine(message);
        }

        public static void Fatal(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
