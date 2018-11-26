using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceCodeIllustratorTests
{
    [TestClass]
    public class PrimeNumberTests
    {
        public TestContext TestContext { set; get; }
        private Workers.PrimeNumbers_Practice primesPractice = new Workers.PrimeNumbers_Practice();
        private Workers.PrimeNumbers primes = new Workers.PrimeNumbers();
        private Workers.PrimeNumberEvaluators primeEvaluator = new Workers.PrimeNumberEvaluators();

        [TestMethod()] 
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", 
            "|DataDirectory|\\Data\\PrimeNumbers.csv", "PrimeNumbers#csv", 
            DataAccessMethod.Sequential)]
        public void PrimeNumberConfirmations()
        {
            int prime = Convert.ToInt32(TestContext.DataRow["Prime"]);
            TestContext.WriteLine("Testing that {0} is a prime number.", prime);
            string msg = String.Format("{0} is a not prime number.", prime);
            Assert.IsTrue(primesPractice.IsPrime_Original(prime), msg);
            Assert.IsTrue(primesPractice.IsPrime_Improvement1(prime), msg);
            Assert.IsTrue(primesPractice.IsPrime_Improvement2(prime), msg);
            Assert.IsTrue(primes.IsPrime(prime), msg);
        }

        [TestMethod()]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", 
            "|DataDirectory|\\Data\\NonPrimeNumbers.csv", "NonPrimeNumbers#csv", 
            DataAccessMethod.Sequential)]
        public void NonPrimeNumberConfirmations()
        {
            int nonPrime = Convert.ToInt32(TestContext.DataRow["NonPrime"]);
            TestContext.WriteLine("Testing that {0} is not a prime number.", nonPrime);
            string msg = String.Format("{0} is a prime number.", nonPrime);
            Assert.IsFalse(primesPractice.IsPrime_Original(nonPrime), msg);
            Assert.IsFalse(primesPractice.IsPrime_Improvement1(nonPrime), msg);
            Assert.IsFalse(primesPractice.IsPrime_Improvement2(nonPrime), msg);
            Assert.IsFalse(primes.IsPrime(nonPrime), msg);
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

        [TestMethod]
        public void GetNextPrime()
        {
            Assert.IsTrue(primeEvaluator.NextPrime(2, primes.IsPrime) == 3);
            Assert.IsTrue(primeEvaluator.NextPrime(3, primes.IsPrime) == 5);
            Assert.IsTrue(primeEvaluator.NextPrime(3137, primes.IsPrime) == 3163);
            Assert.IsTrue(primeEvaluator.NextPrime(3137, primesPractice.IsPrime_Original) == 3163);
            Assert.IsTrue(primeEvaluator.NextPrime(3164, primesPractice.IsPrime_Original) == 3167);
            Assert.IsTrue(primeEvaluator.NextPrime(3167, primesPractice.IsPrime_Original) == 3169);
        }
    }
}
