using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwaitConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var n = await GetNumberOfTasksToRun();

            if (n <= 0)
                return;

            await RunTasks(n);
            await MainAsync(args);
        }

        private static async Task RunTasks(int n)
        {
            var service = new MyService();

            var sw = new Stopwatch();
            sw.Start();

            var tasks = new Task<double>[n];
            for (int i = 0; i < n; i++)
                tasks[i] = service.RunTask();

            Task.WaitAll(tasks);
            sw.Stop();

            await Console.Out.WriteLineAsync(".");
            await Console.Out.WriteLineAsync($"Sum of tasks delay: {tasks.Sum(t => t.Result)}s");
            await Console.Out.WriteLineAsync($"{n} tasks ran in {sw.ElapsedMilliseconds / 1000.0}s");
            await Console.Out.WriteLineAsync(".");
        }

        static async Task<int> GetNumberOfTasksToRun()
        {
            await Console.Out.WriteLineAsync($"Type a positive integer number of tasks to run, or anything else to quit:");

            if (!int.TryParse(Console.ReadLine(), out int i))
                return 0;

            return i;
        }
    }
}
