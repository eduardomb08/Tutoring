using System;
using System.Threading.Tasks;

namespace AsyncAwaitConsoleExample
{
    public class MyService
    {
        public async Task<double> RunTask()
        {
            return await AddRandomDelay();
        }

        private static async Task<double> AddRandomDelay()
        {
            var time = new Random().NextDouble() * 3.0;
            await Console.Out.WriteLineAsync($"RunTask1 delaying {time}s");
            await Task.Delay(TimeSpan.FromSeconds(time));
            return time;
        }
    }
}
