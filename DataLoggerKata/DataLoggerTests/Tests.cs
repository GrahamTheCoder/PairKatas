using System;
using System.Collections.Generic;
using System.Linq;
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
            var dataPlotter = new ThreadSafeDataPlotter(probeReader.Object, dataLogger.Object);
            dataPlotter.Collect(0);
            dataLogger.Verify(d => d.Plot(It.IsAny<DataValueAdapter>()), Times.Never);
        }

        [TestCase(5)]
        [TestCase(10)]
        public void CollectingFiveValuesPlotsOnce(int numberOfValuesToCollect)
        {
            var probeReader = new Mock<IProbeReaderAdapter>();
            probeReader.Setup(x => x.Read()).Returns(ReturnTestValue);
            var dataLogger = new Mock<IDataLoggerAdapter>();
            var dataPlotter = new ThreadSafeDataPlotter(probeReader.Object, dataLogger.Object);

            dataPlotter.Collect(numberOfValuesToCollect);

            int completeBatchesOfFive = numberOfValuesToCollect / 5;
            dataLogger.Verify(d => d.Plot(It.IsAny<DataValueAdapter>()), Times.Exactly(completeBatchesOfFive));
        }

        private DataValueAdapter ReturnTestValue()
        {
            return new DataValueAdapter();
        }
    }
}
