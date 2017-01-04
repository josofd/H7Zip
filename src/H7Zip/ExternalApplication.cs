using System.Diagnostics;

namespace H7Zip
{
    internal static class ExternalApplication
    {
        public static int RunCommandLine(string filePath, string args)
        {
            ProcessStartInfo process = new ProcessStartInfo(filePath, string.Format(" {0}", args))
            {
                UseShellExecute = false,
                RedirectStandardOutput = false,
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Minimized
            };

            return RunProcess(process);
        }

        private static int RunProcess(ProcessStartInfo processInfo)
        {
            using (Process process = new Process())
            {
                int exitCode = -1;

                process.StartInfo = processInfo;
                process.Start();
                process.WaitForExit();
                exitCode = process.ExitCode;

                return exitCode;
            }
        }
    }
}
