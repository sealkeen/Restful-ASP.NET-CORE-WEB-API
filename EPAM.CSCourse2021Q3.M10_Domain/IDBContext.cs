using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Threading;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public interface IDBContext : IDisposable
    {
        public Microsoft.Extensions.Logging.ILogger Logger { get; set; }
        void AddEntity<TEntity>(TEntity entity) where TEntity : class;
        void RemoveEntity<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        IQueryable<Lector> GetLectors();
        IQueryable<Student> GetStudents();
        IQueryable<Lecture> GetLectures();
        IQueryable<LectureStudent> GetLectureStudents();
        IQueryable<Homework> GetHomeworks();
        IQueryable<StudentHomework> GetStudentHomeworks();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
    }
}
