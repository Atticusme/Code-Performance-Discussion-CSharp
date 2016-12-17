using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceCodeIllustratorTests
{
    [TestClass]
    public class PrimeNumberTests
    {
        private Workers.PrimeNumbers_Practice primesPractice = new Workers.PrimeNumbers_Practice();
        private Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
        private Workers.PrimeNumberEvaluators primeEvaluator = new Workers.PrimeNumberEvaluators();

        [TestMethod]
        public void ZeroIsNotAPrime()
        {
            Assert.IsFalse(primesPractice.IsPrime_Original(0));
            Assert.IsFalse(primesPractice.IsPrime_Improvement1(0));
            Assert.IsFalse(primesPractice.IsPrime_Improvement2(0));
            Assert.IsFalse(primes.IsPrime(0));
        }

        [TestMethod]
        public void OneIsNotAPrime()
        {
            Assert.IsFalse(primesPractice.IsPrime_Original(1));
            Assert.IsFalse(primesPractice.IsPrime_Improvement1(1));
            Assert.IsFalse(primesPractice.IsPrime_Improvement2(1));
            Assert.IsFalse(primes.IsPrime(1));
        }

        [TestMethod]
        public void PrimeTestsFor2AreTrue()
        {
            Assert.IsTrue(primesPractice.IsPrime_Original(2));
            Assert.IsTrue(primesPractice.IsPrime_Improvement1(2));
            Assert.IsTrue(primesPractice.IsPrime_Improvement2(2));
            Assert.IsTrue(primes.IsPrime(2));
        }

        [TestMethod]
        public void PrimeTestsFor3AreTrue()
        {
            Assert.IsTrue(primesPractice.IsPrime_Original(3));
            Assert.IsTrue(primesPractice.IsPrime_Improvement1(3));
            Assert.IsTrue(primesPractice.IsPrime_Improvement2(3));
            Assert.IsTrue(primes.IsPrime(3));
        }

        [TestMethod]
        public void PrimeTestsFor5AreTrue()
        {
            Assert.IsTrue(primesPractice.IsPrime_Original(5));
            Assert.IsTrue(primesPractice.IsPrime_Improvement1(5));
            Assert.IsTrue(primesPractice.IsPrime_Improvement2(5));
            Assert.IsTrue(primes.IsPrime(5));
        }

        [TestMethod]
        public void PrimeTestsFor7AreTrue()
        {
            Assert.IsTrue(primesPractice.IsPrime_Original(7));
            Assert.IsTrue(primesPractice.IsPrime_Improvement1(7));
            Assert.IsTrue(primesPractice.IsPrime_Improvement2(7));
            Assert.IsTrue(primes.IsPrime(7));
        }

        [TestMethod]
        public void PrimeTestsFor9AreFalse()
        {
            Assert.IsFalse(primesPractice.IsPrime_Original(9));
            Assert.IsFalse(primesPractice.IsPrime_Improvement1(9));
            Assert.IsFalse(primesPractice.IsPrime_Improvement2(9));
            Assert.IsFalse(primes.IsPrime(9));
        }

        [TestMethod]
        public void PrimeTestFor1427AreTrue()
        {
            Assert.IsTrue(primes.IsPrime(1427));
        }

        [TestMethod]
        public void PrimeTestFor1428AreFalse()
        {
            Assert.IsFalse(primes.IsPrime(1428));
        }

        [TestMethod]
        public void PrimeTestFor1429AreTrue()
        {
            Assert.IsTrue(primes.IsPrime(1429));
        }

        [TestMethod]
        public void PrimeTestFor1430AreFalse()
        {
            Assert.IsFalse(primes.IsPrime(1430));
        }

        [TestMethod]
        public void PrimeTestFor1432AreFalse()
        {
            Assert.IsFalse(primes.IsPrime(1432));
        }

        [TestMethod]
        public void PrimeTestFor1433AreTrue()
        {
            Assert.IsTrue(primes.IsPrime(1433));
        }

        [TestMethod]
        public void PrimeTestFor104728AreFalse()
        {
            Assert.IsFalse(primes.IsPrime(104728));
        }

        [TestMethod]
        public void PrimeTestFor104729AreTrue()
        {
            Assert.IsTrue(primes.IsPrime(104729));
        }

        [TestMethod]
        public void PrimeTestFor2042041AreFalse()
        {
            Assert.IsFalse(primes.IsPrime(2042041));
        }

        [TestMethod]
        public void PrimeTestFor2036339AreTrue()
        {
            Assert.IsTrue(primes.IsPrime(2036339));
        }

        [TestMethod]
        public void PrimeTestFor10029889AreFalse()
        {
            Assert.IsFalse(primes.IsPrime(10029889));
        }

        [TestMethod]
        public void PrimeTestFor10004573AreTrue()
        {
            Assert.IsTrue(primes.IsPrime(10004573));
        }

        [TestMethod]
        public void EvaluatePrimesMatchesWhenParallel()
        {
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(0, 8, primes.IsPrime) == 
                primeEvaluator.EvaluatePrimesInParallel(0, 8, primes.IsPrime));
        }

        [TestMethod]
        public void EvaluatePrimesMatchesWhenParallelWhenLastOfRangeIsPrime()
        {
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(0, 7, primes.IsPrime) ==
                primeEvaluator.EvaluatePrimesInParallel(0, 7, primes.IsPrime));
        }

        [TestMethod]
        public void EvaluatePrimesWhenFromIsNot0()
        {
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(5, 7, primes.IsPrime) == 2);
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(6, 7, primes.IsPrime) == 1);
        }

        [TestMethod]
        public void EvaluatePrimesRangeResolved()
        {
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(7, 0, primes.IsPrime) == 4);
        }

        [TestMethod]
        public void EvaluatePrimesRangeResolvedInParallel()
        {
            Assert.IsTrue(primeEvaluator.EvaluatePrimesInParallel(7, 0, primes.IsPrime) == 4);
            
        }

        [TestMethod]
        public void GetMaxPrimeInRangeResolvedInParallel()
        {
            Assert.IsTrue(primeEvaluator.GetMaxPrimeInRangeInParallel(7, 0, primes.IsPrime) == 7);
        }

        [TestMethod]
        public void EvaluatePrimesRangeIsEqual()
        {
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(7, 7, primes.IsPrime) == 1);
            Assert.IsTrue(primeEvaluator.EvaluatePrimes(9, 9, primes.IsPrime) == 0);
        }

        [TestMethod]
        public void GetMaxPrimeInRangeInParallelFrom0To10Is7()
        {
            Assert.IsTrue(primeEvaluator.GetMaxPrimeInRangeInParallel(0, 10, primes.IsPrime) == 7);
        }

        [TestMethod]
        public void GetMaxPrimeIHighRange()
        {
            Assert.IsTrue(primeEvaluator.GetMaxPrimeInRange(29999990, 30000000, primes.IsPrime) == 29999999);
        }

        [TestMethod]
        public void GetMaxPrimeIHighRangeInParallel()
        {
            Assert.IsTrue(primeEvaluator.GetMaxPrimeInRangeInParallel(29999990, 30000000, primes.IsPrime) == 29999999);
        }
    }
}
