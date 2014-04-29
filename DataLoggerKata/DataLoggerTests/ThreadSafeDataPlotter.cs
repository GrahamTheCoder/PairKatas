namespace DataLoggerTests
{
    public class ThreadSafeDataPlotter
    {
        private readonly IProbeReaderAdapter _probeReaderAdapter;
        private readonly IDataLoggerAdapter _dataLoggerAdapter;

        public ThreadSafeDataPlotter(IProbeReaderAdapter probeReaderAdapter, IDataLoggerAdapter dataLoggerAdapter)
        {
            _probeReaderAdapter = probeReaderAdapter;
            _dataLoggerAdapter = dataLoggerAdapter;
        }

        public void Collect(int readingsToCollect)
        {
            for (int i = 0; i < readingsToCollect; i += 5)
            {
                _dataLoggerAdapter.Plot(new DataValueAdapter());
            }
        }
    }
}