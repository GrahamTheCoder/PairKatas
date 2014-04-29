using System;
using System.Collections.Generic;

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
            for (int i = 0; i < readingsToCollect; i += 5)
            {
                _dataLoggerAdapter.Plot(new[] { new DataValueAdapter(new DateTime(), null)});
            }
        }
    }
}