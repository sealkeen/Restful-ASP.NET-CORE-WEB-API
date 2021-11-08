using EPAM.CSCourse2021Q3.M10_Domain;
using EPAM.CSCourse2021Q3.M10_Logic;
using EPAM.CSCourse2021Q3.M10_ReportCreator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2021Q3.M10_UnitTests
{
    public class ReportCreatorTester : ConsoleWriteLinable
    {
        [Test]
        public void CreateXMLTest()
        {
            Student student = new Student();
            student.FirstName = "FN";
            student.LastName = "LN";
            Console.WriteLine(student.ToXML().ToString());
        }

        [Test]
        public void HomeWorksToXML()
        {
            LectionsRepository repository = new LectionsRepository();
            repository.GetHomeworks().ForEach(h => h.ToXML().SendToConsole());
        }

        [Test]
        public void LecturesToXML()
        {
            LectionsRepository repository = new LectionsRepository();
            repository.GetLectures().ForEach(h => h.ToXML().SendToConsole());
        }
    }
}
