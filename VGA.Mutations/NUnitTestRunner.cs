namespace VGA.Mutations
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Mutators;

    internal class NUnitTestRunner : ITestRunner
    {
        public IEnumerable<TestToExecute> RunTests(IEnumerable<TestToExecute> testsToExecute, string tempPath)
        {
            const string nUnitConsoleRunner = ".\\NUnitConsoleRunner\\nunit-console.exe";
            var greenTests = new List<TestToExecute>();

            foreach (var testToExecute in testsToExecute)
            {
                var arguments = string.Format("/run:{0} {1}",
                    string.Format("{0}.{1}", testToExecute.ClassName, testToExecute.MethodName),
                    Path.Combine(tempPath, testToExecute.AssemblyName));

                var nunitProces = GetNUnitProcess(nUnitConsoleRunner, arguments);

                nunitProces.Start();

                nunitProces.WaitForExit(30000);

                if (nunitProces.ExitCode == 0)
                {
                    greenTests.Add(testToExecute);
                }
            }

            return greenTests;
        }

        private static Process GetNUnitProcess(string nUnitConsoleRunner, string arguments)
        {
            var nunitProces = new Process
            {
                StartInfo = new ProcessStartInfo(nUnitConsoleRunner, arguments)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };
            return nunitProces;
        }
    }
}