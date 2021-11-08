using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public partial class Homework : DomainEntity, IMapable
    {
        public long HomeworkID { get; set; }
        public int Mark { get; set; }
        public long LectureID { get; set; }
        public long StudentID { get; set; }
        public Lecture Lecture { get; set; }
        public StudentHomework StudentHomework { get; set; }

        public new bool MapTo<T>(T target) where T : IMapable
        {
            MapTo(target as Homework);
            return true;
        }

        public override long GetID()
        {
            return HomeworkID;
        }

        public override string ToString()
        {
            return this.GetInfo();
        }
    }
}
