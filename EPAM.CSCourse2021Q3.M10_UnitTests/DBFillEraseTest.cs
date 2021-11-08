using EPAM.CSCourse2021Q3.M10_Domain;
using Module10Core;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using EPAM.CSCourse2021Q3.M10_Logic;
using EPAM.CSCourse2021Q3.M10_Logging;
using Microsoft.EntityFrameworkCore;

namespace EPAM.CSCourse2021Q3.M10_UnitTests
{
    //  Intergration / Unit Tests.  (+)
    //    • Unit тесты должны уметь проверять зависимости и их корректность, бизнес логику, вызовы методов.
    //    • Интеграционные тесты должны уметь создавать тестовые данные в базе и работать с ними, (+|-)
    //      после тестов данные должны быть очищены. (InMemoryDatabase) (+)
    public class DBFillEraseTest : ConsoleWriteLinable
    {
        private Random rnd = new Random();
        public IEnumerable<StudentHomework> GetNewHomeworks()
        {
            IDBContext r = new M10DBContext();
            for (int i = 0; i < r.GetStudents().Count(); i++) {
                var rndStud = rnd.Next(1, r.GetStudents().Count());
                var rndLecture = rnd.Next(1, r.GetLectures().Count());
                Student student = r.GetStudents().ToList().First(s => s.StudentID == rndStud);
                Homework homework = HomeworkExtensions.Create(
                    r.GetLectures().ToList().First(l => l.LectureID == rndLecture),
                    student,
                    rnd.Next(5)
                );
                r.AddEntity(homework);
                yield return StudentHomeworkExtensions.Create(student, homework);
            }
        }
        public IEnumerable<Lector> GetNewLectors() 
        {
            yield return new Lector { LectorID = 1L, FirstName = "Oleg", LastName = "Tarusov" };
            yield return new Lector { FirstName = "Kickolay", LastName = "Dollezhal" };
            yield return new Lector { FirstName = "Michael", LastName = "Till" };
            yield return new Lector { FirstName = "Michail", LastName = "Simonov" };
            yield return new Lector { FirstName = "Joel", LastName = "Spolky" };
            yield return new Lector { FirstName = "Jeff", LastName = "Etwood" };
            yield return new Lector { FirstName = "Chris", LastName = "Vantras" };
        }
        public IEnumerable<Student> GetNewStudents() 
        {
            yield return new Student { StudentID = 1L, FirstName = "Ivan", LastName = "Silkin" };
            yield return new Student { FirstName = "Aleksandr", LastName = "Pushkin" };
            yield return new Student { FirstName = "Mikhail", LastName = "Lermontov" };
            yield return new Student { FirstName = "Mikhail", LastName = "Glinka" };
            yield return new Student { FirstName = "Petr", LastName = "Chaikovsky" };
            yield return new Student { FirstName = "Nikolay", LastName = "Rimsky-Korsakov" };
            yield return new Student { FirstName = "Dmitry", LastName = "Shostakovich" };
            yield return new Student { FirstName = "Lev", LastName = "Tolstoy" };
            yield return new Student { FirstName = "Nickolay", LastName = "Gogol" };
            yield return new Student { FirstName = "Fedor", LastName = "Dostoevsky" };
            yield return new Student { FirstName = "Ivan", LastName = "Turgenev" };
            yield return new Student { FirstName = "Anton", LastName = "Chehov" };
            yield return new Student { FirstName = "Aleksandr", LastName = "Ostrovsky" };
            yield return new Student { FirstName = "Aleksandr", LastName = "Griboedov" };
            yield return new Student { FirstName = "Nickolay", LastName = "Neckrasov" };
            yield return new Student { FirstName = "Mikhail", LastName = "Saltykov-Schedrin" };
        }
        //public IEnumerable<Homework> GetHomeWorks(Student student, Lecture lecture)
        //{
        //    yield return new Homework {
        //        HomeworkID = 1L, 
        //        Student = student,
        //        StudentID = student.StudentID,
        //        LectureID = lecture.LectureID,
        //        Lecture = lecture
        //        };
        //}
        public void AddLector(Lector lector, IDBContext dbContext)
        {
            dbContext.AddEntity(lector); dbContext.SaveChanges();
            Assert.AreEqual(dbContext.GetLectors()
                .First(l => l.LectorID == lector.LectorID).LastName, lector.LastName);
            ShowData(dbContext);
        }
        public void AddStudent(Student student, IDBContext dbContext)
        {
            var std = dbContext?.GetStudents();
            student.StudentID = std.Count() == 0 ? 1L : std.Max(s=>s.StudentID)+1;
            dbContext.AddEntity(student); 
            dbContext.SaveChanges();
            Assert.AreEqual(dbContext.GetStudents()
                .First(s => s.StudentID == student.StudentID).LastName, student.LastName);
            ShowData(dbContext);
        }
        public void RemoveLector(Lector lector, IDBContext dbContext)
        {
            if (dbContext.GetLectors().Any(s => s.LectorID == lector.LectorID))
            {
                dbContext.RemoveEntity(lector);
                dbContext.SaveChanges();
            }
            ShowData(dbContext);
        }
        public void RemoveStudentIfExists(Student student, IDBContext dbContext)
        {
            if (dbContext.GetStudents().Any(s => s.StudentID == student.StudentID))
            {
                dbContext.RemoveEntity(student);
                dbContext.SaveChanges();
            }
            ShowData(dbContext);
        }
        [Test]
        public void AddLectors()
        {
            using (IDBContext dbContext = new M10DBContext()) 
            {
                foreach (var lector in GetNewLectors())
                {
                    AddLector(lector, dbContext);
                }
            }
        }
        [Test]
        public void AddStudents()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                foreach (var student in GetNewStudents())
                {
                    AddStudent(student, dbContext);
                }
            }
        }
        
        [Test]
        public void ShowHomeworks()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                dbContext.GetHomeworks()
                    .Include(h => h.StudentHomework)
                    .ThenInclude(sh=>sh.Student)
                    .ForEach(x => x.ToConsole());
            }
        }

        [Test]
        public void AddHomeworks()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                foreach (var homework in GetNewHomeworks())
                {
                    dbContext.AddEntity(homework);
                    dbContext.SaveChanges();
                }
            }
        }

        [Test]
        public void DeleteHomeworks()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                foreach (var homework in dbContext.GetHomeworks())
                {
                    dbContext.RemoveEntity(homework);
                }
                foreach (var sh in dbContext.GetStudentHomeworks())
                {
                    dbContext.RemoveEntity(sh);
                }
                dbContext.SaveChanges();
            }
        }

        public void ShowData(IDBContext dbContext)
        {
            Debug.WriteLine($"Students: {dbContext.GetStudents().Count()}" +
                $" Lectors: {dbContext.GetLectors().Count()}" +
                $" Lectures: {dbContext.GetLectures().Count()}");
        }
    }
}