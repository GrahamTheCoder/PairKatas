using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ParseArguments
{
    public class ParsedArgs
    {
        private readonly IDictionary<char, object> m_Dictionary;
        private const bool IsPresentValue = true;
        private const char FlagSpecifier = '-';

        private ParsedArgs(IDictionary<char, object> dictionary)
        {
            m_Dictionary = dictionary;
        }
        
        public static ParsedArgs ParseArgs(string[] args)
        {
            return new ParsedArgs(args.Select((s, i) => ParseArg(args, s, i)).Where(x => x != null).Select(x => x.Value).ToDictionary(x => x.Key, x => x.Value));
        }
        
        private static KeyValuePair<char, object>? ParseArg(string[] args, string arg, int index)
        {

            if (arg[0] == FlagSpecifier)
            {
                var flagLetter = arg[1];
                var nextArg = args.Length -1 == index ? "-nomoreargs" : args[index + 1];
                bool hasAssociatedValue = nextArg[0] != FlagSpecifier;

                var value = hasAssociatedValue ? GetParsedValue(nextArg) : (object)IsPresentValue;

                return new KeyValuePair<char, object>(flagLetter, value);
            }
            else return null;
        }

        private static int GetParsedValue(string nextArg)
        {
            return int.Parse(nextArg);
        }

        public int Count()
        {
            return m_Dictionary.Count;
        }

        public object this[char flag]
        {
            get
            {
                object value;
                if (m_Dictionary.TryGetValue(flag, out value))
                {
                    return value;
                }
                else
                {
                    return false;
                };

            }
        }
    }

    [TestFixture]
    public class Tests
    {
        private const char m_ArgPrefix = '-';

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
            var fullFlag = new char[] {m_ArgPrefix, flag};
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

        [Test]
        public void GivenOneIntegerValueParsedArgsReturnsOneIntegerValue()
        {
            const char flag = 'i';
            var fullFlag = new char[] {m_ArgPrefix, flag};
            var value = 2;
            var parsedArgs = ParsedArgs.ParseArgs(new[] {new string(fullFlag), value.ToString()});
            Assert.That(parsedArgs[flag], Is.EqualTo(value));
        }
    }
}
