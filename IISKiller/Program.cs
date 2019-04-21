using System.Diagnostics;
using System.Threading;

namespace IISKiller
{
    class Program
    {
        const string IIS_EXPRESS = "iisexpress";

        static void Main(string[] args)
        {
            bool shouldKill = false;

            while (true)
            {
                shouldKill |= IsInternetExplorerRunning() && IsProcessRunning(IIS_EXPRESS);

                if (shouldKill && !IsInternetExplorerRunning())
                {
                    KillProcess(IIS_EXPRESS);
                    shouldKill = false;
                }

                Thread.Sleep(3000);
            }
        }

        static bool IsInternetExplorerRunning()
        {
            return IsProcessRunning("iexplore");
        }

        static bool IsBrowserRunning()
        {
            return IsProcessRunning("chrome", "iexplore", "firefox", "opera");
        }

        static bool IsProcessRunning(params string[] processNames)
        {
            foreach (var processName in processNames)
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        static void KillProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                process.Kill();
                process.Close();
            }
        }
    }
}
