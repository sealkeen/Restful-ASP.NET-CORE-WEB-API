using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public class StudentHomework
    {
        public long StudentID { get; set; }
        public long HomeworkID { get; set; }
        public Student Student { get; set; }
        public Homework Homework { get; set; }
    }
}
