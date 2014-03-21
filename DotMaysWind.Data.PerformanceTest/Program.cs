using System;
using System.Diagnostics;

namespace DotMaysWind.Data.PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            Int32 cycle = 10000;

            Console.WriteLine("DotMaysWind.Data Performance Test, Cycle = {0}", cycle);
            Console.WriteLine(".NET Framework CLR : {0}", Environment.Version);

            Console.WriteLine();
            Console.WriteLine("Select Command Generate:");
            RunTest(stopWatch, cycle, new Action(SelectCommandCreateTest.BaseCreateSelectCommand));
            RunTest(stopWatch, cycle, new Action(SelectCommandCreateTest.DatabaseNormalCreateSelectCommand));
            RunTest(stopWatch, cycle, new Action(SelectCommandCreateTest.DatabaseLinqCreateSelectCommand));
            RunTest(stopWatch, cycle, new Action(SelectCommandCreateTest.ProviderLinqCreateSelectCommand));

            Console.WriteLine();
            Console.WriteLine("Insert Command Generate:");
            RunTest(stopWatch, cycle, new Action(InsertCommandCreateTest.BaseCreateInsertCommand));
            RunTest(stopWatch, cycle, new Action(InsertCommandCreateTest.DatabaseNormalCreateInsertCommand));
            RunTest(stopWatch, cycle, new Action(InsertCommandCreateTest.DatabaseEntityCreateInsertCommand));
            RunTest(stopWatch, cycle, new Action(InsertCommandCreateTest.ProviderEntityCreateInsertCommand));
            RunTest(stopWatch, cycle, new Action(InsertCommandCreateTest.DatabaseLinqCreateInsertCommand));
            RunTest(stopWatch, cycle, new Action(InsertCommandCreateTest.ProviderLinqCreateInsertCommand));
        }

        private static void RunTest(Stopwatch stopWatch, Int32 cycle, Action action)
        {
            stopWatch.Reset();
            stopWatch.Start();

            for (Int32 i = 0; i < cycle; i++)
            {
                action.Invoke();
            }

            stopWatch.Stop();

            Console.WriteLine("{0} : {1:N4} ms", action.Method, (Double)stopWatch.ElapsedTicks * 1000 / Stopwatch.Frequency);
        }
    }
}