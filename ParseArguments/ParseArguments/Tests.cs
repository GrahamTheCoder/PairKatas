using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ParseArguments
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GivenZeroArgsReturnsZeroParsedArgs()
        {
            var parsedArgs = ParseArgs(new String[0]);
            Assert.That(parsedArgs.Count, Is.EqualTo(0));
        }

        [Test]
        public void GivenZeroValuedFlagReturnsOneParsedBooleanArg()
        {
            const char flag = 's';
            const char argPrefix = '-';
            var fullFlag = new char[] {argPrefix, flag};
            var parsedArgs = ParseArgs(new string[] {new string(fullFlag)});
            Assert.That(parsedArgs[flag], Is.EqualTo(true));
        }

        private IDictionary<char, object> ParseArgs(string[] args)
        {
            return new Dictionary<char, object>();
        }
    }
}
