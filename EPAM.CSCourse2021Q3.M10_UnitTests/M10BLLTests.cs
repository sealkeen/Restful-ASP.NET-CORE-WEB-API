using EPAM.CSCourse2021Q3.M10_Logic;
using EPAM.CSCourse2021Q3.M10_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module10Core;
using EPAM.CSCourse2021Q3.M10_Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2021Q3.M10_UnitTests
{
    class M10BLLTests : ConsoleWriteLinable
    {
        public void ShowCount(IDBContext dbContext)
        {
            string count = "";
            count += "Count of lectures: " + dbContext.GetLectures().Count();
            count += ", \nCount of lectors: " + dbContext.GetLectors().Count();
            count += ", \nCount of students: " + dbContext.GetStudents().Count();
            count += ", \nCount of student homeworks: " + dbContext.GetStudentHomeworks().Count();
            count += ", \nCount of lecture students: " + dbContext.GetLectureStudents().Count();
            Console.WriteLine(count);
        }

        [Test]
        public void TestAddLecture()
        {
            LectionsRepository lW = NLogInjector.InjectNLog<LectionsRepository>();

            //Console.WriteLine(lecture.GetInfo());
            Console.WriteLine("_____________Start");
            using (IDBContext dbContext = new M10DBContext())
            {
                ShowAllEntities(dbContext);

                Console.WriteLine("_____________Add Lecture");
                LectureExtensions.CreateNewLecture(dbContext);
                dbContext.SaveChanges();

                ShowAllEntities(dbContext);
            }
        }

        [Test]
        public void TestDeleteLectures()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                DeleteLectures(dbContext);
            }
        }

        private void DeleteLectures(IDBContext dbContext)
        {
            dbContext.GetLectures().ForEach(dbContext.RemoveEntity);
            dbContext.SaveChanges();
        }

        [Test]
        public static void DeleteData()
        {
            using (var context = new M10DBContext())
            {
                DeleteData(context);
            }
        }   

        private static void DeleteData(IDBContext dbContext)
        {
            Console.WriteLine("_____________Delete Data");
            dbContext.GetStudents().ForEach(dbContext.RemoveEntity);
            dbContext.GetStudentHomeworks().ForEach(dbContext.RemoveEntity);
            dbContext.GetLectors().ForEach(dbContext.RemoveEntity);
            dbContext.GetLectures().ForEach(dbContext.RemoveEntity);
            dbContext.GetLectureStudents().ForEach(dbContext.RemoveEntity);
            dbContext.GetHomeworks().ForEach(dbContext.RemoveEntity);
            dbContext.SaveChanges();
        }

        [Test]
        public void ShowEntities()
        {
            //DoSomethingWithDataBase(DeleteLectures);
            //DoSomethingWithDataBase(ShowAllEntities);
            //DoSomethingWithDataBase(LectionsWorker.CreateNewLecture);
            DoSomethingWithDataBase(ShowAllEntities);
        }

        public void DoSomethingWithDataBase(Action<IDBContext> action)
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                action(dbContext);
            }
        }

        private void ShowAllEntities(IDBContext dbContext)
        {
            Console.WriteLine("_____________Show All Entities");
            ShowCount(dbContext);
            dbContext.GetLectors().ForEach(l => Console.WriteLine(l.GetInfo()));
            dbContext.GetStudents().ForEach(s => Console.WriteLine(s.GetInfo()));
            dbContext.GetLectures().Include(x => x.LectureStudents).ForEach(l => Console.WriteLine(l.GetInfo()));
            dbContext.GetHomeworks().ForEach(h => Console.WriteLine(h.GetInfo()));
        }
    }
}
