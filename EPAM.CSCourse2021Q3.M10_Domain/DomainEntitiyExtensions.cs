using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public static class DomainEntitiyExtensions
    {
        public static void ToConsole(this DomainEntity entity)
        {
            Console.WriteLine(entity.ToString());
        }
    }
}
