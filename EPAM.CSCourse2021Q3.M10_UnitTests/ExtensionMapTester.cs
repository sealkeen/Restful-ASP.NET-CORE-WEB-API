using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.CSCourse2021Q3.M10_Domain;

namespace EPAM.CSCourse2021Q3.M10_UnitTests
{
    class ExtensionMapTester : ConsoleWriteLinable
    {
        [Test]
        public void MapStudent()
        {
            Student student = new Student();
            student.FirstName = "FirstName";
            student.LastName = "LastName";
            Console.WriteLine(student.GetInfo());
            Student secStudent = new Student();
            student.MapTo(secStudent);
            Console.WriteLine(secStudent.GetInfo());
        }
    }
}
