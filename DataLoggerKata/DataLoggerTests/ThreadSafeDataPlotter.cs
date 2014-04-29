using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLoggerTests
{
    public class ThreadSafeDataPlotter
    {
        private readonly IEnumerable<IProbeReaderAdapter> _probeReaderAdapters;
        private readonly IDataLoggerAdapter _dataLoggerAdapter;
        private readonly BlockingCollection<DataValueAdapter> _valuesToPlot;
        private bool m_Stop = false;
        public ThreadSafeDataPlotter(IEnumerable<IProbeReaderAdapter> probeReaderAdapters, IDataLoggerAdapter dataLoggerAdapter)
        {
            _probeReaderAdapters = probeReaderAdapters;
            _dataLoggerAdapter = dataLoggerAdapter;
            _valuesToPlot = new BlockingCollection<DataValueAdapter>();
        }

        public void Collect(int readingsToCollect)
        {
            var taskFactory = new TaskFactory();
            foreach (var probeReaderAdapter in _probeReaderAdapters)
            {
                taskFactory.StartNew(() => ReadFromProbeForever(probeReaderAdapter));
            }

            int collected = 0;
            while (collected < readingsToCollect)
            {
                var values = _valuesToPlot.GetConsumingEnumerable().Take(5);
                _dataLoggerAdapter.Plot(values);
                collected += 5;
            }

            m_Stop = true;

        }

        private void ReadFromProbeForever(IProbeReaderAdapter probeReaderAdapter)
        {
            while (!m_Stop)
            {
                _valuesToPlot.Add(probeReaderAdapter.Read());
            }
        }
    }
}