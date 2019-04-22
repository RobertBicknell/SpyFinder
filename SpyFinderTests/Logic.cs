using System;
using Xunit;
using SpyFinderLogic;

namespace SpyFinderTests
{
    public class Logic
    {
        [Fact]
        public void Test1()
        {
            var testCase = new LogicTestCase {
                Message = new int[] {0, 1, 2, 3, 4 },
                Code = new int[] {1, 3},
                Result = true
            };
            testCase.Run(SpyChecker.MessageContainsSpy);
        }

        [Fact]
        public void Test2()
        {
            var testCase = new LogicTestCase
            {
                Message = new int[] { 0, 1, 2, 3, 4 },
                Code = new int[] { 1, 0 },
                Result = false
            };
            testCase.Run(SpyChecker.MessageContainsSpy);
        }


        [Fact]
        public void Test3()
        {
            var testCase = new LogicTestCase
            {
                Message = new int[] { 0, 1, 2, 3, 4 },
                Code = new int[] { 1, 0,2,3,4,5,6,7,8,9,10,11 },
                Result = false
            };
            testCase.Run(SpyChecker.MessageContainsSpy);
        }

        [Fact]
        public void Test4()
        {
            var testCase = new LogicTestCase
            {
                Message = new int[] { 0, 1, 2, 3, 100 },
                Code = new int[] { 1, 0 },
                Result = false //NOTE: check with requirements team if this test cases works as required
            };
            testCase.Run(SpyChecker.MessageContainsSpy);
        }

        [Fact]
        public void Test5()
        {
            var testCase = new LogicTestCase
            {
                Message = new int[] { 0, 1, 2, 3, -1, 0 },
                Code = new int[] { 1, -1 },
                Result = true
            };
            testCase.Run(SpyChecker.MessageContainsSpy);
        }
    }

    class LogicTestCase {
        public int[] Message;
        public int[] Code;
        public bool Result;

        public void Run(Func<int[], int[], bool> f)  {
            Assert.Equal(Result, f(Message, Code));
        }
    }
}
