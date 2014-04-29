using System.Collections.Generic;

namespace DataLoggerTests
{
    public interface IDataLoggerAdapter
    {
        void Plot(IEnumerable<DataValueAdapter> isAny);
    }
}