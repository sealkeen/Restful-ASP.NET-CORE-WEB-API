using EPAM.CSCourse2021Q3.M10_Domain;
using EPAM.CSCourse2021Q3.M10_Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.CSCourse2021Q3.M10_Logging;
using Module10Core;
using Microsoft.EntityFrameworkCore;
using EPAM.CSCourse2021Q3.M10_Logic;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public static class StudentExtensions
    {
        public static string GetInfo(this Student student)
        {
            string result = "";
            if (student == null || student.StudentID < 0)
            {
                result += " Student's is absent (No such). ";
                throw new StudentNotFoundException(result);
            }
            else
            {
                result += $"{{ StudentID: {student.StudentID}, ";
                result += $"{PersonExtension.GetName(student)} }}";
            }

            return result;
        }

        public static async Task<bool> PutStudent(long id, Student sourceStudent)
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                if (id != sourceStudent.StudentID)
                {
                    throw new ObjectIDsDoesntMatchException("the Student's ID doesn't match the given argument ID.");
                }

                var foundLectors = dbContext.GetStudents().Where(l => l.StudentID == id);
                if (foundLectors.Count() > 0)
                {
                    sourceStudent.MapTo(foundLectors.FirstOrDefault()); ;
                }

                try
                {
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!LectorExtensions.LectorExists(id))
                    {
                        throw new LectorNotFoundException($"No lector found with id {id}. {ex.Message}");
                    }
                }
            }
            return false;
        }

        public static bool StudentExists(long id)
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return dbContext.GetStudents().Any(s => s.StudentID == id);
            }
        }

        public static async Task<Student> GetStudent(long id)
        {
            using (IDBContext dBContext = new M10DBContext())
            {
                var student = await dBContext.FindAsync<Student>(id);

                if (student == null)
                {
                    throw new StudentNotFoundException("The requested student wasn't found.");
                }
                return student;
            }
        }
    }
}
