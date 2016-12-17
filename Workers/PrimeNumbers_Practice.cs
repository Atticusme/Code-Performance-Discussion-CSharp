using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis; 

namespace Workers
{
    public class PrimeNumbers_Practice
    {
        private const int _baseIndex = 2;

        /// <summary>
        /// This is the first implementation of the IsPrime test.
        /// It is easy to intuit and functional, but slow.
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public bool IsPrime_Original(int testValue)
        {
            if (testValue < _baseIndex) return false;
            for (int numerator = _baseIndex; numerator < testValue; numerator++)
            {
                if ((testValue % numerator) == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// With a quick google search, it is clear there is a better way to accomplish the same result.
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public bool IsPrime_Improvement1(int testValue)
        {
            if (testValue < _baseIndex) return false;
            for (int iteration = _baseIndex; iteration <= Math.Sqrt(testValue); iteration++)
            {
                if ((testValue % iteration) == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Reviewing the code reveals that some simple changes in coding style can have a meaninful impact.
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public bool IsPrime_Improvement2(int testValue)
        {
            if (testValue < _baseIndex) return false;
            int y = (int)Math.Floor(Math.Sqrt(testValue));
            int iteration = _baseIndex;
            while (iteration <= y)
            {
                if ((testValue % iteration) == 0)
                    return false;
                ++iteration;
            }
            return true;
        }

        /// <summary>
        /// This function was an exploration of optimizing the 2nd improvement, but 
        /// a failure to get unit test coverage led to deeper evaluation 
        /// resulting in the final implementation.
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        [Obsolete("This function has been deemed invalid.", true)]
        [ExcludeFromCodeCoverageAttribute]
        public bool IsPrime_PseudoDuffsDevice(int testValue)
        {
            if (testValue < _baseIndex) return false;
            int y = (int)Math.Floor(Math.Sqrt(testValue));
            int iteration = _baseIndex;

            while (iteration + 20 < y)
            {
                if ((testValue % iteration++) == 0) return false;   // 2
                if ((testValue % iteration++) == 0) return false;   // 3
                if ((testValue % iteration++) == 0) return false;   // Never hit
                if ((testValue % iteration++) == 0) return false;   // 5
                if ((testValue % iteration++) == 0) return false;   // Never hit
                if ((testValue % iteration++) == 0) return false;   // 7
                if ((testValue % iteration++) == 0) return false;   // etc.
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
    }
}
