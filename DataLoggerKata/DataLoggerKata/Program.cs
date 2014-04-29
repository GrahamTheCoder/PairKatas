using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeKata.Nox;

namespace DataLoggerKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataLogger = DataLoggerFactory.Create();
            var reader = ProbeReaderFactory.Create();
            var dataValue = reader.Read();
            dataLogger.Plot(new[] {dataValue});
        }
    }
}
