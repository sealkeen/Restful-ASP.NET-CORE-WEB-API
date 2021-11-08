using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EPAM.CSCourse2021Q3.M10_ReportCreator
{
    public static class XElementExtensions
    {
        public static void SendToConsole(this XElement element)
        {
            Console.WriteLine(element);
        }
    }
}
