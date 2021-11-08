using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public interface ILogable
    {
        Microsoft.Extensions.Logging.ILogger Logger { get; set; }
    }
}
