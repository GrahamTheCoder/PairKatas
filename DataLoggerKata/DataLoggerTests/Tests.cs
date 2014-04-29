using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeKata.Nox;
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
    }
}
