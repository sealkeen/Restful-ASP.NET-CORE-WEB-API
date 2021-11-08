using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Logging
{
    public static class ILoggerExtensions
    {
        public static EventId EventError()
        {
            return new EventId(0xAAAA, "Error") { };
        }
    }
}