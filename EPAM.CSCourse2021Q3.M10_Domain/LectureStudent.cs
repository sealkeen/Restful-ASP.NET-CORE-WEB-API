using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public class LectureStudent
    {
        public long StudentID { get; set; }
        public long LectureID { get; set; }
        public Student Student { get; set; }
        public Lecture Lecture { get; set; }
    }
}