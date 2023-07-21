

using System.Diagnostics;

namespace WSAustral.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {

        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(14400));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count
                );


            string ironPythonPath = @"Scripts\ipython.exe";
            
            /* Ejecutar procesos */
            string pythonScript = @"Scripts\ReporteFaenasyCalasScript.py";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ironPythonPath,
                Arguments = pythonScript,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            using (Process process = Process.Start(startInfo))
            {
                // Leer la salida estándar del proceso
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                process.WaitForExit();
            }

        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
