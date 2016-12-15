using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceCodeIllustratorTests
{
    [TestClass]
    public class PrimeNumberTests
    {
        [TestMethod]
        public void ZeroIsNotAPrime()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsFalse(primes.IsPrime(0));
            Assert.IsFalse(primes.IsPrime_Improvement1(0));
            Assert.IsFalse(primes.IsPrime_Improvement2(0));
        }

        [TestMethod]
        public void OneIsNotAPrime()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsFalse(primes.IsPrime(1));
            Assert.IsFalse(primes.IsPrime_Improvement1(1));
            Assert.IsFalse(primes.IsPrime_Improvement2(1));
        }

        [TestMethod]
        public void PrimeTestsAreEqual()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsTrue(primes.IsPrime(7));
            Assert.IsTrue(primes.IsPrime(7) == primes.IsPrime_Improvement1(7));
            Assert.IsTrue(primes.IsPrime(7) == primes.IsPrime_Improvement2(7));

            Assert.IsFalse(primes.IsPrime(9));
            Assert.IsTrue(primes.IsPrime(9) == primes.IsPrime_Improvement1(9));
            Assert.IsTrue(primes.IsPrime(9) == primes.IsPrime_Improvement2(9));
        }

        [TestMethod]
        public void EvaluatePrimesMatchesWhenParallel()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsTrue(primes.EvaluatePrimes(0, 8, primes.IsPrime_Improvement2) == primes.EvaluatePrimesInParallel(0, 8, primes.IsPrime_Improvement2));
        }

        [TestMethod]
        public void EvaluatePrimesMatchesWhenParallelWhenLastOfRangeIsPrime()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsTrue(primes.EvaluatePrimes(0, 7, primes.IsPrime_Improvement2) == primes.EvaluatePrimesInParallel(0, 7, primes.IsPrime_Improvement2));
        }

        [TestMethod]
        public void EvaluatePrimesWhenFromIsNot0()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsTrue(primes.EvaluatePrimes(5, 7, primes.IsPrime_Improvement2) == 2);
            Assert.IsTrue(primes.EvaluatePrimes(6, 7, primes.IsPrime_Improvement2) == 1);
        }

        [TestMethod]
        public void EvaluatePrimesRangeIsLogical()
        {
            var exceptionCount = 0;
            try
            {
                Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
                Assert.IsTrue(primes.EvaluatePrimes(7, 0, primes.IsPrime_Improvement2) == 3);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == @"EvaluatePrimes range is invalid");
                ++exceptionCount;
            }
            Assert.IsTrue(exceptionCount == 1);
        }

        [TestMethod]
        public void EvaluatePrimesRangeInParallelIsLogical()
        {
            var exceptionCount = 0;
            try
            {
                Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
                Assert.IsTrue(primes.EvaluatePrimesInParallel(7, 0, primes.IsPrime_Improvement2) == 3);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == @"EvaluatePrimes range is invalid");
                ++exceptionCount;
            }
            Assert.IsTrue(exceptionCount == 1);
        }

        [TestMethod]
        public void GetMaxPrimeInRangeInParallelRangeIsLogical()
        {
            var exceptionCount = 0;
            try
            {
                Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
                Assert.IsTrue(primes.GetMaxPrimeInRangeInParallel(7, 0, primes.IsPrime_Improvement2) == 3);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == @"EvaluatePrimes range is invalid");
                ++exceptionCount;
            }
            Assert.IsTrue(exceptionCount == 1);
        }

        [TestMethod]
        public void EvaluatePrimesRangeIsEqual()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsTrue(primes.EvaluatePrimes(7, 7, primes.IsPrime_Improvement2) == 1);
            Assert.IsTrue(primes.EvaluatePrimes(9, 9, primes.IsPrime_Improvement2) == 0);
        }

        [TestMethod]
        public void GetMaxPrimeInRangeInParallelFrom0To10Is7()
        {
            Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
            Assert.IsTrue(primes.GetMaxPrimeInRangeInParallel(0, 10, primes.IsPrime_Improvement2) == 7);
        }
    }
}
