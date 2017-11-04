using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;
using System.Threading;

namespace Test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var summary = BenchmarkRunner.Run<TS>();
            Console.WriteLine(summary.TotalTime);
            Console.ReadKey();
            Console.ReadKey();
        }      
    }
}
