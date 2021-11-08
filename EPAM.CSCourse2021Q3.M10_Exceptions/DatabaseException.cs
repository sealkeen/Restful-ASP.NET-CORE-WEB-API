using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Exceptions
{
    public class DatabaseException : ApplicationCoreException
    {
        public DatabaseException(string message) : base(message)
        {

        }
    }
}
