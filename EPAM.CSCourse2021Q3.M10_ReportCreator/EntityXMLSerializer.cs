using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using EPAM.CSCourse2021Q3.M10_Domain;

namespace EPAM.CSCourse2021Q3.M10_ReportCreator
{
    public static class EntityXMLSerializer
    {
        public static XElement ToXML(this Person person)
        {
            XElement student = new XElement(person is Student ? "Student" : "Lector");
            XAttribute id = new XAttribute("ID", person.GetID().ToString());
            student.Add(id);
            XAttribute name = new XAttribute("Name", $"{ person.FirstName} { person.LastName}");
            student.Add(name);
            return student;
        }

        public static XElement ToXML(this Homework homework)
        {
            XElement xhomework = new XElement("Homework");
            XAttribute id = new XAttribute("HomeworkID", homework.GetID().ToString());
            xhomework.Add(id);
            //XElement lecID = new XElement("LectureID") { Value = homework.LectureID.ToString() };
            //xhomework.Add(lecID);
            XElement mark = new XElement("Mark") { Value = homework.Mark.ToString() };
            xhomework.Add(mark);
            //XElement xstudentid = new XElement("StudentID") { Value = homework.StudentID.ToString() };
            //xhomework.Add(xstudentid);
            XElement xstudent = homework.StudentHomework.Student.ToXML();
            xhomework.Add(xstudent);
            return xhomework;
        }
        public static XElement ToXML(this Lecture lecture)
        {
            XElement xlecture = new XElement("Lecture");
            XAttribute id = new XAttribute("LectureID", lecture.LectureID.ToString());
            xlecture.Add(id);
            XElement xlector = lecture.Lector.ToXML();
            xlecture.Add(xlector);
            lecture.LectureStudents.ForEach(ls=> xlecture.Add(ls.Student.ToXML()));
            return xlecture;
        }
    }
}