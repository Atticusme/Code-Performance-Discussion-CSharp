using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PerformanceCodeIllustrator
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
            Console.WriteLine("This project is intended to facillitate a");
            Console.WriteLine("discussion on performant code as it relates to");
            Console.WriteLine("the development mindset.");
            Console.WriteLine();
            Console.WriteLine("Please feel free to improve and \\ or reuse this presentation providedd");
            Console.WriteLine("Atticus Ellena is credited for the original preparation.");
            Console.WriteLine();
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

            int smallMax = 100000;         // a maximum for the original implementation (too slow to use the same max as otheres)
            int largeMax = 10000000;       // a good test for optimized operations
            int badParallelMax = 1000;     // running in parallel may hurt performance when the test set does not warrant it

            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Workers.PrimeNumbers_Practice primesPractice = new Workers.PrimeNumbers_Practice();
            Workers.PrimeNumberEvaluators primeEvaluator = new Workers.PrimeNumberEvaluators();

            // Test the original implementation
            Console.WriteLine("With the first implementation, we have a functional but slow outcome.");
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimes, 0, smallMax, primesPractice.IsPrime_Original);
            Console.WriteLine();
            Console.ReadKey();

            // Test the better formula
            Console.WriteLine("A quick google search revealed a better solution.");
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimes, 0, smallMax, primesPractice.IsPrime_Improvement1);
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimes, 0, largeMax, primesPractice.IsPrime_Improvement1);
            Console.WriteLine();
            Console.ReadKey();

            // Remove the common loop mistake
            Console.WriteLine("Minimal evaluation yields even better results.");
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimes, 0, largeMax, primesPractice.IsPrime_Improvement2);
            Console.WriteLine();
            Console.WriteLine("Getting beyond this point required more involved evaluation.");
            Console.WriteLine();
            Console.ReadKey();

            // Demo the final implementation
            Console.WriteLine("The final implemenation.");
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimes, 0, largeMax, primes.IsPrime);
            Console.WriteLine();
            Console.ReadKey();

            // Parallel hurts when the sample size is too small
            Console.WriteLine("Now that we have a solid IsPrime function, ");
            Console.WriteLine("we can consider other options such as multi-threaded evaluations.");
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimes, 0, badParallelMax, primes.IsPrime);
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimesInParallel, 0, badParallelMax, primes.IsPrime);
            Console.WriteLine("These will of course expose even more complexity to account for.");
            Console.WriteLine("This example illustrates how optimizing has actually ");
            Console.WriteLine("hurt performance with a smaller data set.");
            Console.WriteLine();
            Console.ReadKey();

            // Compare our optimized version against our cheap version.
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimesInParallel, 0, largeMax, primesPractice.IsPrime_Improvement2);
            DoEvaluatePrimes(primeEvaluator.EvaluatePrimesInParallel, 0, largeMax, primes.IsPrime);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
