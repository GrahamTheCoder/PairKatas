using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ParseArguments
{
    public class ParsedArgs
    {
        private readonly IDictionary<char, object> m_Dictionary;

        private ParsedArgs(IDictionary<char, object> dictionary)
        {
            m_Dictionary = dictionary;
        }
        
        public static ParsedArgs ParseArgs(string[] args)
        {
            return new ParsedArgs(args.ToDictionary(a => a[1], _ => (object) true));
        }

        public int Count()
        {
            return m_Dictionary.Count;
        }

        public object this[char flag]
        {
            get { return m_Dictionary[flag]; }
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void GivenZeroArgsReturnsZeroParsedArgs()
        {
            var parsedArgs = ParsedArgs.ParseArgs(new String[0]);
            Assert.That(parsedArgs.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GivenZeroValuedFlagReturnsOneParsedBooleanArg()
        {
            const char flag = 's';
            const char argPrefix = '-';
            var fullFlag = new char[] {argPrefix, flag};
            var parsedArgs = ParsedArgs.ParseArgs(new string[] {new string(fullFlag)});
            Assert.That(parsedArgs[flag], Is.EqualTo(true));
        }

        [Test]
        public void GivenZeroArgsParsedArgsAreFalse()
        {
            const char flag = 's';
            var parsedArgs = ParsedArgs.ParseArgs(new string[0]);
            Assert.That(parsedArgs[flag], Is.EqualTo(false));
        }
    }
}
