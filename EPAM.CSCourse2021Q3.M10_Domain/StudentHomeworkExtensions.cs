using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public static class StudentHomeworkExtensions
    {
        public static StudentHomework Create(Student student, Homework homework)
        {
            StudentHomework studentHomework = new StudentHomework();
            //studentHomework.Student = student;
            studentHomework.StudentID = student.StudentID;
            //studentHomework.Homework = homework;
            studentHomework.HomeworkID = homework.HomeworkID;
            return studentHomework;
        }
    }
}
