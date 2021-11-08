using EPAM.CSCourse2021Q3.M10_Domain;
using Microsoft.EntityFrameworkCore;
using EPAM.CSCourse2021Q3.M10_Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Module10Core;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public class DataShower
    {
        public static void ShowEntities()
        {
            IDBContext dbContext = new M10DBContext();
            dbContext.ApplyActionToDBContext(ShowAllEntities);
        }

        private static void ShowAllEntities(IDBContext dbContext)
        {
            Console.WriteLine("_____________Show All Entities");
            ShowCount(dbContext);
            dbContext.GetLectors().ForEach(l => Console.WriteLine(l.GetInfo()));
            dbContext.GetStudents().ForEach(s => Console.WriteLine(s.GetInfo()));
            dbContext.GetLectures().Include(x => x.LectureStudents).ForEach(l => Console.WriteLine(l.GetInfo()));
            dbContext.GetHomeworks().ForEach(h => Console.WriteLine(h.GetInfo()));
        }

        public static void ShowCount(IDBContext dbContext)
        {
            string count = "";
            count += "Count of lectures: " + dbContext.GetLectures().Count();
            count += ", Count of lectors: " + dbContext.GetLectors().Count();
            count += ", Count of students: " + dbContext.GetStudents().Count();
            Console.WriteLine(count);
        }
    }
}
