using EPAM.CSCourse2021Q3.M10_Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public class LectureStudentExtensions
    {
        public static LectureStudent Create(Lecture lecture, Student student)
        {
            try
            {
                LectureStudent lC = new LectureStudent();
                lC.Lecture = lecture;
                lC.LectureID = lecture.LectureID;
                lC.Student = student;
                lC.StudentID = student.StudentID;
                return lC;
            } catch (NullReferenceException nLE) {
                throw new LectureStudentException();
            }
        }
    }
}
