using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    public class PrimeNumbers
    {
        private Object ParallelLock = new Object();

        public bool IsPrime(int testValue)
        {
            if (testValue < 2) return false;
            for (int numerator = 2; numerator < testValue; numerator++)
            {
                if ((testValue % numerator) == 0)
                    return false;
            }
            return true;
        }

        public bool IsPrime_Improvement1(int testValue)
        {
            if (testValue < 2) return false;
            for (int iteration = 2; iteration <= Math.Sqrt(testValue); iteration++)
            {
                if ((testValue % iteration) == 0)
                    return false;
            }
            return true;
        }

        public bool IsPrime_Improvement2(int testValue)
        {
            if (testValue < 2) return false;
            int y = (int)Math.Floor(Math.Sqrt(testValue));
            int iteration = 2;
            while (iteration <= y)
            {
                if ((testValue % iteration) == 0)
                    return false;
                ++iteration;
            }
            return true;
        }

        public bool IsPrime_Improvement_PseudoDuffsDevice(int testValue)
        {
            if (testValue < 2) return false;
            int y = (int)Math.Floor(Math.Sqrt(testValue));
            int iteration = 2;
            int z = (int)Math.Floor((double)y / 10);


            while (iteration < z)
            {
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
                if ((testValue % iteration++) == 0) return false;
            }

            while (iteration <= y)
            {
                if ((testValue % iteration) == 0)
                    return false;
                iteration++;
            }
            return true;
        }

        public int EvaluatePrimes(int from, int to, Func<int, bool> IsPrimeFunction)
        {
            if (to < from)
            {
                throw new System.Exception(@"EvaluatePrimes range is invalid");
            }
            int count = 0;
            for (; from <= to; from++)
            {
                if (IsPrimeFunction(from)) count++;
            }
            return count;
        }

        public int EvaluatePrimesInParallel(int from, int to, Func<int, bool> IsPrimeFunction)
        {
            if (to < from)
            {
                throw new System.Exception(@"EvaluatePrimes range is invalid");
            }
            int count = 0;
            int tests = (to - from) + 1;
            Parallel.For(from, tests, i =>
            {
                if (IsPrimeFunction(i)) { System.Threading.Interlocked.Add(ref count, 1); }
            });
            return count;
        }

        public int GetMaxPrimeInRangeInParallel(int from, int to, Func<int, bool> IsPrimeFunction)
        {
            if (to < from)
            {
                throw new System.Exception(@"EvaluatePrimes range is invalid");
            }
            int max = -1;
            int tests = to - from + 1;
            Parallel.For(from, tests, i =>
            {
                if (IsPrimeFunction(i)) 
                { 
                    lock (ParallelLock)
                    {
                        max = Math.Max(i, max);
                    }
                }
            });
            return max;
        }
    }
}
