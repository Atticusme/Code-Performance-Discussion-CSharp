using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication1
{
    public static class MyExtensions
    {
        public static string FormatElapsed(this Stopwatch stopWatch)
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            return ts.ToString();
        }
    }

    class Program
    {
        static void WriteIntro()
        {
            Console.WriteLine("Press any key at each pause to proceed to the next example.");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void DoEvaluatePrimes(Func<int, int, Func<int, bool>, int> EvaluatePrimesFunction, int from, int to, Func<int, bool> IsPrimeFunction)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine(String.Format("{0} using {1}", EvaluatePrimesFunction.Method.Name, IsPrimeFunction.Method.Name));
            Console.WriteLine(String.Format("Evaluating {0} numbers yielded {1} primes and took {2}", 
                                             to - from, 
                                             EvaluatePrimesFunction.Invoke(from, to, IsPrimeFunction),
                                             stopWatch.FormatElapsed()));
        }

        static void Main(string[] args)
        {
            WriteIntro();

            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            int smallMax =  100000;         // a maximum for the original implementation - we don't have all day
            int largeMax =  1000000;       // a good test for optimized operations
            int badParallelMax = 1000;     // running in parallel may hurt performance

            // Test the original implementation
            Console.WriteLine("The first implementation is fairly easy to intuit.");
            DoEvaluatePrimes(primes.EvaluatePrimes, 0, smallMax, primes.IsPrime);
            Console.WriteLine();
            Console.ReadKey();

            // Test the better formula
            Console.WriteLine("A quick google search revealed a better solution.");
            DoEvaluatePrimes(primes.EvaluatePrimes, 0, largeMax, primes.IsPrime_Improvement1);
            Console.WriteLine();
            Console.ReadKey();

            // Remove the common loop mistake
            DoEvaluatePrimes(primes.EvaluatePrimes, 0, largeMax, primes.IsPrime_Improvement2);
            Console.WriteLine();
            Console.ReadKey();

            // Remove the common loop mistake
            DoEvaluatePrimes(primes.EvaluatePrimes, 0, largeMax, primes.IsPrime_Improvement_PseudoDuffsDevice);
            Console.WriteLine();
            Console.ReadKey();

            // Parallel hurts when the sample size is too small
            DoEvaluatePrimes(primes.EvaluatePrimes, 0, badParallelMax, primes.IsPrime_Improvement2);
            DoEvaluatePrimes(primes.EvaluatePrimesInParallel, 0, badParallelMax, primes.IsPrime_Improvement2);
            Console.WriteLine();
            Console.ReadKey();

            // 
            DoEvaluatePrimes(primes.EvaluatePrimesInParallel, 0, largeMax, primes.IsPrime_Improvement2);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
