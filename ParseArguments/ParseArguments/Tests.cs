using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

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

        private IDictionary<char, object> ParseArgs(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
