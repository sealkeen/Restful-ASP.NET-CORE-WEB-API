using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Exceptions
{
    public class HomeworkNotFoundException : DatabaseException
    {
        public HomeworkNotFoundException(string message) : base(message)
        {
            
        }
    }
}
