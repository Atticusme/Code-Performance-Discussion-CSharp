﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    public class PrimeNumberEvaluators
    {
        private Object ParallelLock = new Object();

        private void ValidateRange(ref int from, ref int to)
        {
            if (to < from)
            {
                int temp = to;
                to = from;
                from = temp;
            }
        }

        public int EvaluatePrimes(int from, int to, Func<int, bool> IsPrimeFunction)
        {
            ValidateRange(ref from, ref to);
            int count = 0;
            for (; from <= to; from++)
            {
                if (IsPrimeFunction(from)) count++;
            }
            return count;
        }

        public int EvaluatePrimesInParallel(int from, int to, Func<int, bool> IsPrimeFunction)
        {
            ValidateRange(ref from, ref to);
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
            ValidateRange(ref from, ref to);
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