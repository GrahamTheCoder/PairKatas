using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLoggerTests
{
    public class ThreadSafeDataPlotter
    {
        private readonly IEnumerable<IProbeReaderAdapter> _probeReaderAdapters;
        private readonly IDataLoggerAdapter _dataLoggerAdapter;

        public ThreadSafeDataPlotter(IEnumerable<IProbeReaderAdapter> probeReaderAdapters, IDataLoggerAdapter dataLoggerAdapter)
        {
            _probeReaderAdapters = probeReaderAdapters;
            _dataLoggerAdapter = dataLoggerAdapter;
        }

        public void Collect(int readingsToCollect)
        {
            var readings = new List<DataValueAdapter>(5);
            for (int i = 1; i <= readingsToCollect; i++)
            {
                readings.Add(_probeReaderAdapters.First().Read());
                if (i%5 == 0) _dataLoggerAdapter.Plot(readings);
            }

        }
    }
}