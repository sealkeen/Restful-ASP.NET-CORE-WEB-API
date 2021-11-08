using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Exceptions
{
    public class LectureStudentException : ApplicationCoreException
    {
        public LectureStudentException(string message = "Objects currupted: Either student or lecture's fields were missing.") : base(message)
        { 

        }
    }
}
