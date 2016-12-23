﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Workers
{
    public class PrimeNumbers
    {
        /// <summary>
        /// This array helps to optimize the routine.
        /// The decision of how many items to put in the array was based on 
        /// the primary test value of 2,000,000 defined in the console application and then extended to 10,000,000
        /// </summary>
        private const int _primeCount = 444;// 222 AND 1427 COVERS 2000000
        private int[] _primes = new int[_primeCount] { /*2, 3, 5,*/ 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 
            137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 
            317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 
            523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 
            743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 
            977, 983, 991, 997, 1009, 1013,1019,1021,1031,1033,1039,1049,1051,1061,1063,1069,1087,1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 
            1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 
            1399, 1409, 1423, 1427, /*222 count covering 2000000 at this point*/
            1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511,
            1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613,
            1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733,
            1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867,
            1871, 1873, 1877, 1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987,
            1993, 1997, 1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053,
            2063, 2069, 2081, 2083, 2087, 2089, 2099, 2111, 2113, 2129,
            2131, 2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213,
            2221, 2237, 2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287,
            2293, 2297, 2309, 2311, 2333, 2339, 2341, 2347, 2351, 2357,
            2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423,
            2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531,
            2539, 2543, 2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617,
            2621, 2633, 2647, 2657, 2659, 2663, 2671, 2677, 2683, 2687,
            2689, 2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731, 2741,
            2749, 2753, 2767, 2777, 2789, 2791, 2797, 2801, 2803, 2819,
            2833, 2837, 2843, 2851, 2857, 2861, 2879, 2887, 2897, 2903,
            2909, 2917, 2927, 2939, 2953, 2957, 2963, 2969, 2971, 2999,
            3001, 3011, 3019, 3023, 3037, 3041, 3049, 3061, 3067, 3079,
            3083, 3089, 3109, 3119, 3121, 3137, 3163};

        /// <summary>
        /// This is the latest optimized implementation of of the IsPrime function,
        /// </summary>
        /// <param name="testValue">Value to be validated as prime or not.</param>
        /// <returns>returns true when testValue is a prime number, otherwise false</returns>
        public bool IsPrime(int testValue)
        {
            if (testValue < 2) return false;

            #region Any modification to one part of this region requires modification to the other part!
            {
                // NOTE: Keeping these here in hopes of clarifying that a change 
                //       to IsLowPrime requires a change to CommonElimination.
                // Of course unit tests should indicate as much
                
                // Factoring out the most common eliminators to better performance
                // 1/2 of all numbers will be eliminated if odd
                // The first non prime number to fall through is 49 and the next is 77 and so on.
                if (/*IsLowPrime(testValue)*/
                    (testValue == 2) ||
                    (testValue == 3) ||
                    (testValue == 5))
                {
                    return true;
                }
                if (/*CommonElimination(testValue)*/
                    ((testValue & 1) == 0) ||       // Found small benefit when testing even vs. remainder.
                    (testValue % 3) == 0 || 
                    (testValue % 5) == 0)
                {
                    return false;
                }
            }
            #endregion

            int maxIteration = (int)Math.Floor(Math.Sqrt(testValue));   // minimize the number of evaluations

            int iteration = 0;
            do
            {
                if (_primes[iteration] > maxIteration)
                {
                    return true;
                }
                if (testValue % _primes[iteration] == 0)
                {
                    return false;
                }
                ++iteration;
            } while (iteration < _primeCount);

            //System.Diagnostics.Trace.TraceInformation("IsPrime iterations: {0}", iteration);

            return (IsPrimeForVeryLargeNumbers(testValue, maxIteration));
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private bool IsLowPrime(int testValue)
        //{
        //    return ((testValue == 2) || (testValue == 3) || (testValue == 5));
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private bool CommonElimination(int testValue)
        //{
        //    return (((testValue & 1) == 0) || // Found small benefit when testing even vs. remainder.
        //            (testValue % 3) == 0 ||
        //            (testValue % 5) == 0);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsPrimeForVeryLargeNumbers(int testValue, int maxIteration)
        {
            int iteration = 3164;
            // A minor optimization, but if the array changes, so must this hard coded item.
            System.Diagnostics.Debug.Assert(iteration == _primes[_primeCount - 1] + 1);

            do
            {
                if ((testValue % iteration) == 0)
                {
                    //System.Diagnostics.Trace.TraceInformation("IsPrimeForVeryLargeNumbers iterations: {0}", iteration);
                    return false;
                }
                ++iteration;
            } while (iteration <= maxIteration);
            
            //System.Diagnostics.Trace.TraceInformation("IsPrimeForVeryLargeNumbers iterations: {0}", iteration);
            return true;
        }
    }
}
