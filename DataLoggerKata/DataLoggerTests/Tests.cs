using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace DataLoggerTests
{
    public class Tests
    {
        [Test]
        public void CollectingNoValuesDoesNotPlotAnything()
        {
            var probeReader = new Mock<IProbeReaderAdapter>();
            var dataLogger = new Mock<IDataLoggerAdapter>();
            var dataPlotter = new ThreadSafeDataPlotter(new [] { probeReader.Object}, dataLogger.Object);
            dataPlotter.Collect(0);
            dataLogger.Verify(d => d.Plot(It.IsAny<IEnumerable<DataValueAdapter>>()), Times.Never);
        }

        [TestCase(5)]
        [TestCase(10)]
        public void CollectsValuesInFullBatchesFive(int numberOfValuesToCollect)
        {
            var probeReader = new Mock<IProbeReaderAdapter>();
            probeReader.Setup(x => x.Read()).Returns(ReturnTestValue);
            var dataLogger = new Mock<IDataLoggerAdapter>();
            var dataPlotter = new ThreadSafeDataPlotter(new[] { probeReader.Object}, dataLogger.Object);

            dataPlotter.Collect(numberOfValuesToCollect);

            int completeBatchesOfFive = numberOfValuesToCollect / 5;
            dataLogger.Verify(d => d.Plot(It.IsAny<IEnumerable<DataValueAdapter>>()), Times.Exactly(completeBatchesOfFive));
        }

        [Test]
        public void CollectedValuesArePlotted()
        {
            var values = Enumerable.Range(0, 5).Select(x => new DateTime(x)).Select(x => new DataValueAdapter(x, null));
            var probeReader = new MockProbeReader(values); 

            var dataLogger = new Mock<IDataLoggerAdapter>();
            dataLogger.Setup(x => x.Plot(It.IsAny<IEnumerable<DataValueAdapter>>()))
                .Callback<IEnumerable<DataValueAdapter>>(plottedValues => Assert.That(plottedValues, Is.EqualTo(values)));
            var dataPlotter = new ThreadSafeDataPlotter(new[] { probeReader }, dataLogger.Object);

            dataPlotter.Collect(5);
            dataLogger.Verify(d => d.Plot(It.IsAny<IEnumerable<DataValueAdapter>>()));
        }

        private DataValueAdapter ReturnTestValue()
        {
            return new DataValueAdapter(new DateTime(), null);
        }
    }

    public class MockProbeReader : IProbeReaderAdapter
    {
        private readonly Queue<DataValueAdapter> _values;

        public MockProbeReader(IEnumerable<DataValueAdapter> values)
        {
            _values = new Queue<DataValueAdapter>(values);
        }


        public DataValueAdapter Read()
        {
            return _values.Dequeue();
        }
    }
}
