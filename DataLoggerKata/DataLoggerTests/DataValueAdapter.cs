using System;
using CodeKata.Nox;

namespace DataLoggerTests
{
    public class DataValueAdapter
    {
        private readonly DateTime _dateTime;
        private readonly DataValue _dataValue;

        public DataValueAdapter(DateTime dateTime, DataValue dataValue)
        {
            _dateTime = dateTime;
            _dataValue = dataValue;
        }

        public override string ToString()
        {
            return string.Format("DateTime: {0}, DataValue: {1}", _dateTime, _dataValue);
        }
    }
}