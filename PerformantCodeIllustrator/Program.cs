using System;
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
        static bool _demo = true;              // Set to false if ReadKey has Shift pressed - usefult when debugging and only want the final tests run
        static bool _pauses = true;             // Set to false if ReadKey has Ctrl pressed - useful in pre-demo to JIT compile everything

        static int _smallMax = 100000;         // a maximum for the original implementation (too slow to use the same max as otheres)
        static int _largeMax = 10000000;       // a good test for optimized operations
        static int _badParallelMax = 1000;     // running in parallel may hurt performance when the test set does not warrant it
        
        static string _pause = ("Press any key to continue...");
        static Workers.PrimeNumbers _primes = new Workers.PrimeNumbers();
        static Workers.PrimeNumbers_Practice _primesPractice = new Workers.PrimeNumbers_Practice();
        static Workers.PrimeNumberEvaluators _primeEvaluator = new Workers.PrimeNumberEvaluators();

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            SetInfoColor();

            WriteIntro();
            ReadKey();
            Console.Clear();

            if (_demo)
            {
                // Test the original implementation
                SetInfoColor();
                Console.WriteLine("With the first implementation, we have a functional but slow outcome.");
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _smallMax, _primesPractice.IsPrime_Original);
                ReadKey();
            }

            if (_demo)
            {
                // Test the better formula
                SetInfoColor();
                Console.WriteLine("A quick google search revealed a better solution.");
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _smallMax, _primesPractice.IsPrime_Improvement1);
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _largeMax, _primesPractice.IsPrime_Improvement1);
                ReadKey();
            }

            if (_demo)
            {
                // Trivial optimizations and compiler discussion (compare release and debug builds)
                SetInfoColor();
                Console.WriteLine("Minimal evaluation yields even better results.");
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _smallMax, _primesPractice.IsPrime_Improvement2);
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _largeMax, _primesPractice.IsPrime_Improvement2);
                ReadKey();
            }

            if (_demo)
            {
                // Demo the final implementation
                SetInfoColor();
                Console.WriteLine("The final implemenation required more consideration to acheive.");
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _smallMax, _primes.IsPrime);
                DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _largeMax, _primes.IsPrime);
                ReadKey();
            }

            if (_demo)
            {
                CompareSmallSetInParallel();
                ReadKey();
            }

            // Compare our optimized version against our cheap version.
            BenchmarkSingleValues();
            DoEvaluatePrimes(_primeEvaluator.EvaluatePrimesInParallel, 0, _largeMax, _primesPractice.IsPrime_Improvement2);
            DoEvaluatePrimes(_primeEvaluator.EvaluatePrimesInParallel, 0, _largeMax, _primes.IsPrime);

            //if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine(_pause);
                Console.ReadKey();
            }

            Console.ResetColor();
        }

        static void ReadKey()
        {
            SetInfoColor();
            Console.WriteLine();
            if (_pauses)
            {
                Console.WriteLine(_pause);
                ConsoleKeyInfo cki = Console.ReadKey();
                if ((cki.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    _demo = false;
                }
                else if ((cki.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    _pauses = false;
                }
                Console.WriteLine();
            }

        }

        static void SetInfoColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        static void SetIstructionColor()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        static void SetResultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void WriteIntro()
        {
            Console.WriteLine("This project is intended to facillitate a discussion on performant code.");
            Console.WriteLine();
            Console.WriteLine("Please feel free to improve and \\ or reuse this presentation provided");
            Console.WriteLine("Atticus Ellena is credited for the original preparation.");
            Console.WriteLine();
#if DEBUG
            Console.WriteLine("--- DEBUG BUILD ---");
#else
            Console.WriteLine("--- RELEASE BUILD ---");
#endif
        }

        static void DoEvaluatePrimes(Func<int, int, Func<int, bool>, int> EvaluatePrimesFunction, int from, int to, Func<int, bool> IsPrimeFunction)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int primes = EvaluatePrimesFunction.Invoke(from, to, IsPrimeFunction);
            string time = stopWatch.FormatElapsed();

            SetIstructionColor();
            Console.WriteLine(String.Format("{0} using {1}", EvaluatePrimesFunction.Method.Name, IsPrimeFunction.Method.Name));
            SetResultColor();
            Console.WriteLine(String.Format("Evaluating {0} numbers yielded {1} primes and took {2}",
                                             to - from,
                                             primes,
                                             time));
        }

        static void BenchmarkSingleValues()
        {
            BenchmarkSingleValue(49);
            BenchmarkSingleValue(53);
            BenchmarkSingleValue(1427);
            BenchmarkSingleValue(2036329);
            BenchmarkSingleValue(10004573);
            BenchmarkSingleValue(10029889);
        }

        static void BenchmarkSingleValue(int testValue)
        {
            SetInfoColor();
            Console.WriteLine("Benmark for value: " + testValue.ToString());

            SetResultColor();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bool isPrime = _primesPractice.IsPrime_Original(testValue);
            string timeElapsed = stopWatch.FormatElapsed();
            Console.WriteLine(String.Format("\t    IsPrime_Original returned {0}: {1}", isPrime, timeElapsed));

            stopWatch.Restart();
            isPrime = _primesPractice.IsPrime_Improvement1(testValue);
            timeElapsed = stopWatch.FormatElapsed();
            Console.WriteLine(String.Format("\tIsPrime_Improvement1 returned {0}: {1}", isPrime, timeElapsed));

            stopWatch.Restart();
            isPrime = _primesPractice.IsPrime_Improvement2(testValue);
            timeElapsed = stopWatch.FormatElapsed();
            Console.WriteLine(String.Format("\tIsPrime_Improvement2 returned {0}: {1}", isPrime, timeElapsed));

            stopWatch.Restart();
            isPrime = _primes.IsPrime(testValue);
            timeElapsed = stopWatch.FormatElapsed();
            Console.WriteLine(String.Format("\t             IsPrime returned {0}: {1}", isPrime, timeElapsed));

            Console.WriteLine();
        }

        static void CompareSmallSetInParallel()
        {
            // Parallel hurts when the sample size is too small
            SetInfoColor();
            Console.WriteLine("Now that we have a solid IsPrime function, ");
            Console.WriteLine("we can consider other options such as multi-threaded evaluations.");
            Console.WriteLine();
            Console.WriteLine("These will of course expose even more complexity to account for.");
            Console.WriteLine("This example illustrates how optimizing has actually ");
            Console.WriteLine("hurt performance with a smaller data set.");
            Console.WriteLine();
            DoEvaluatePrimes(_primeEvaluator.EvaluatePrimes, 0, _badParallelMax, _primes.IsPrime);
            DoEvaluatePrimes(_primeEvaluator.EvaluatePrimesInParallel, 0, _badParallelMax, _primes.IsPrime);
            Console.WriteLine();
        }
    }
}
