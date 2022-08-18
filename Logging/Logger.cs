using System.Diagnostics;

namespace AudioScript.Logging
{
    public static class Logger
    {
        public static void Debug(string message)
        {
            Trace.WriteLine(message);

            var logger = MainWindowViewModel.GetMainWindowViewModel();
            if (logger != null)
                logger.AddStatusText(message);
        }

        public static void Info(string message)
        {
            Trace.WriteLine(message);

            var logger = MainWindowViewModel.GetMainWindowViewModel();
            if (logger != null)
                logger.AddStatusText(message);
        }

        public static void Warning(string message)
        {
            Trace.WriteLine(message);

            var logger = MainWindowViewModel.GetMainWindowViewModel();
            if (logger != null)
                logger.AddStatusText(message);
        }

        public static void Error(string message)
        {
            Trace.WriteLine(message);

            var logger = MainWindowViewModel.GetMainWindowViewModel();
            if (logger != null)
                logger.AddStatusText(message);
        }

        public static void Fatal(string message)
        {
            Trace.WriteLine(message);

            var logger = MainWindowViewModel.GetMainWindowViewModel();
            if (logger != null)
                logger.AddStatusText(message);
        }
    }
}
