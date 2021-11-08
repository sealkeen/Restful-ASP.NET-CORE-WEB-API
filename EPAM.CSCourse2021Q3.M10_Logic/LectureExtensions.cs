using EPAM.CSCourse2021Q3.M10_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using EPAM.CSCourse2021Q3.M10_Exceptions;
using System.Text;
using System.Threading.Tasks;
using Module10Core;
using EPAM.CSCourse2021Q3.M10_Logging;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public static class LectureExtensions
    {
        private static Random _rnd = new Random();

        public static bool NotNull(this Lecture lecture)
        {
            return lecture != null;
        }

        public static async Task<Lecture> CreateNewLecture(IQueryable<Student> students, Lector lc)
        {
            var context = M10DBContext.CreateContextAsync();
            using (IDBContext iContext = await context)
            {
                Lecture lecture = new Lecture();
                lecture.LectorID = 0;
                if (lc == null || iContext.GetLectors().Any(l => l.LectorID != lc.LectorID))
                {
                    var firstLector = iContext.GetLectors().First(l => l.LectorID > 0);
                    lecture.LectorID = firstLector.LectorID;
                    lecture.Lector = firstLector;
                }
                else
                {
                    lecture.LectorID = lc.LectorID;
                    lecture.Lector = lc;
                }
                foreach (Student stud in students)
                {
                    LectureStudent lC = new LectureStudent();
                    lC.Student = stud; lC.StudentID = stud.StudentID;
                    lC.Lecture = lecture; lC.LectureID = lecture.LectureID;
                    lecture.LectureStudents.Add(lC);
                }

                iContext.AddEntity(lecture);
                iContext.SaveChanges();
                return lecture;
            }
        }
        //TODO autoincrement
        public static void CreateNewLecture(IDBContext dbContext)
        {
            Lecture lecture = new Lecture();
            var lecturesQuery = dbContext.GetLectures();
            lecture.LectureID = 
                lecturesQuery.Count() > 0 ?
                lecturesQuery.Max(l => l.LectureID+1) : 1;
            lecture.DateTime = DateTime.Now - TimeSpan.FromDays(_rnd.Next(78));

            var query = dbContext.GetLectors();
            if (query.Count() > 0)
            {
                var rndLector = _rnd.Next(1, dbContext.GetLectors().Count());
                List<Lector> lectors = dbContext.GetLectors().ToList();
                var firstLector = lectors[rndLector];
                lecture.LectorID = firstLector.LectorID;
                lecture.Lector = firstLector;
                //firstLector.
                //firstLector.Lectures.Add(lecture);
            }

            foreach (Student stud in dbContext.GetStudents())
            {
                LectureStudent lS = LectureStudentExtensions.Create(lecture, stud);
                lecture.LectureStudents.Add(lS);
            }

            dbContext.AddEntity(lecture);
            dbContext.SaveChanges();
        }

        public static void RemoveLecture(long LectureID)
        {
            using (IDBContext dBContext = new M10DBContext())
            {
                var query = dBContext.GetLectures();
                var firstMatch = query?.First(lctr => lctr.LectureID == LectureID);
                dBContext?.RemoveEntity(firstMatch);
            }
        }
    }
}
