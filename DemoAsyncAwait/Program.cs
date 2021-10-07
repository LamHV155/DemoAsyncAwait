using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoAsyncAwait
{
    class Program
    {
        static void DoSomeThing(int seconds, int times, string msg, ConsoleColor color)
        {
            Console.WriteLine($"{msg + " ...Start",13}");
            for (int i = 1; i <= times; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{msg,10}:{i,3}");
                    Console.ResetColor();
                }
                Thread.Sleep(seconds);
            }
            Console.WriteLine($"{msg + " ...End",10}");

        }
        static async Task RunTask(int seconds, int times, string msg, ConsoleColor color)
        {
            Task t = new Task(() =>
            {
                DoSomeThing(seconds, times, msg, color);
            });

            t.Start();
            await t;
            // t1.Wait();

        }
        static async Task<string> RunTaskReturnStr(int seconds, int times, string msg, ConsoleColor color)
        {
            Task<string> t = new Task<string>(() =>
            {
                DoSomeThing(seconds, times, msg, color);
                return $"{msg} đã hoàn thành !";
            });

            t.Start();
            // t1.Wait();
            var res = await t;
            return res;

        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Start!");
            Task<string> t1 = RunTaskReturnStr(1000, 7, "T1", ConsoleColor.Red);
            Task<string> t2 = RunTaskReturnStr(1000, 3, "T2", ConsoleColor.Cyan);
            DoSomeThing(1000, 5, "T3", ConsoleColor.DarkYellow);

            var res1 = await t1;
            var res2 = await t2;
            Console.WriteLine(res1);
            Console.WriteLine(res2);
            // Task.WaitAll(t1, t2);
            Console.WriteLine("Hello World!");
        }
        // static async Task Main(string[] args)
        // {
        //     Console.OutputEncoding = Encoding.UTF8;
        //     Console.WriteLine("Start!");
        //     Task t1 = RunTask(1000, 7, "T1", ConsoleColor.Red);
        //     Task t2 = RunTask(1000, 3, "T2", ConsoleColor.Cyan);
        //     DoSomeThing(1000, 5, "T3", ConsoleColor.DarkYellow);

        //     await t1;
        //     await t2;
        //     // Task.WaitAll(t1, t2);
        //     Console.WriteLine("Hello World!");
        // }


        //static void Main(string[] args)
        // {
        //     Console.WriteLine("Start!");
        //     Task t1 = new Task(() =>
        //     {
        //         DoSomeThing(1000, 10, "T1", ConsoleColor.Red);
        //     });
        //     Task t2 = new Task((object obj) =>
        //     {
        //         DoSomeThing(1000, 8, obj.ToString(), ConsoleColor.Green);
        //     }, "T2");
        //     t1.Start();
        //     t2.Start();
        //     DoSomeThing(1000, 5, "T3", ConsoleColor.DarkYellow);

        //     // t1.Wait();
        //     // t2.Wait();
        //     Task.WaitAll(t1, t2);
        //     Console.WriteLine("Hello World!");
        // }
    }
}
