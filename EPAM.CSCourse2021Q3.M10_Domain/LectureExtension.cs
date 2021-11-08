using EPAM.CSCourse2021Q3.M10_Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public static class LectureExtension
    {
        public static string GetInfo(this Lecture lecture)
        {
            string result = "";
            if (lecture == null)
                throw new LectureNotFoundException("{ \"The requested Lecture was null.\" }");
            result += $"\"Lecture_{lecture.LectureID}\" : ";
            result += lecture.Lector.GetInfo();

            foreach (var stud in lecture.LectureStudents)
            {
                result += ", " + stud.Student.GetInfo();
            }
            return result;
        }
    }
}
