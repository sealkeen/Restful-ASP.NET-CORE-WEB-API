using System;
using System.Collections.Generic;
using System.Text;
using EPAM.CSCourse2021Q3.M10_Domain;
using EPAM.CSCourse2021Q3.M10_Exceptions;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public static class HomeworkExtensions
    {
        public static Homework Create(Lecture lec, Student student, int mark)
        {
            Homework homework = new Homework();
            //homework.Lecture = lec; 
            homework.LectureID = lec.LectureID;
            homework.Mark = mark;
            homework.StudentID = student.StudentID;
            return homework;
        }
        public static bool MapTo(this Homework source, Homework target)
        {
            if (source != null && target != null)
            {
                target.LectureID = source.LectureID;
                target.StudentID = source.StudentID;
                target.Mark = source.Mark;
                return true;
            }
            return false;
        }
        public static string GetInfo(this Homework homework)
        {
            string result = "";
            if (homework == null || homework.HomeworkID < 0)
            {
                result += "{ \"Homework's is absent (No such).\" }";
                throw new HomeworkNotFoundException(result);
            }
            else
            {
                result += $"{{ \"HomeworkID\": {homework.HomeworkID}, ";
                result += $"\"HomeworkMark\" : {homework.Mark}, ";
                result += $"\"StudentID\" : { homework.StudentID } }}";
            }
            return result;
        }
    }
}