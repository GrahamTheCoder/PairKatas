using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ParseArguments
{
    public class ParsedArgs
    {
        public IDictionary<char, object> ParseArgs(string[] args)
        {
            return args.ToDictionary(a => a[1], _ => (object) true);
        }
    }

    [TestFixture]
    public class Tests
    {
        private readonly ParsedArgs m_ParsedArgs = new ParsedArgs();

        [Test]
        public void GivenZeroArgsReturnsZeroParsedArgs()
        {
            var parsedArgs = m_ParsedArgs.ParseArgs(new String[0]);
            Assert.That(parsedArgs.Count, Is.EqualTo(0));
        }

        [Test]
        public void GivenZeroValuedFlagReturnsOneParsedBooleanArg()
        {
            const char flag = 's';
            const char argPrefix = '-';
            var fullFlag = new char[] {argPrefix, flag};
            var parsedArgs = m_ParsedArgs.ParseArgs(new string[] {new string(fullFlag)});
            Assert.That(parsedArgs[flag], Is.EqualTo(true));
        }

        [Test]
        public void GivenZeroArgsParsedArgsAreFalse()
        {
            const char flag = 's';
            var parsedArgs = m_ParsedArgs.ParseArgs(new string[0]);
            Assert.That(parsedArgs[flag], Is.EqualTo(false));
        }
    }
}
