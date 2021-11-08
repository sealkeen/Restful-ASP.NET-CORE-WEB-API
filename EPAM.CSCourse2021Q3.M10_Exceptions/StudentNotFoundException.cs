using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Exceptions
{
    public class StudentNotFoundException : DatabaseException
    {
        public StudentNotFoundException(string message) : base(message)
        { 

        }
    }
}
