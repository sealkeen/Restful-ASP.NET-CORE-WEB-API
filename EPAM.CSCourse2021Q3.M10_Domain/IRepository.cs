using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public interface IRepository
    {
        Microsoft.Extensions.Logging.ILogger Logger { get; set; }

        List<Lecture> GetLectures();
        //public void DeleteLectures();
        List<Student> GetStudents();
        //public void DeleteStudents();
        List<Lector> GetLectors();
        //public void DeleteLectors();
        List<Homework> GetHomeworks();
        public Task<bool> AddEntityAsync<T>(T entity) where T : DomainEntity;
        public Task<bool> PutEntityAsync<T>(long id, T entity) where T : DomainEntity, IMapable;
        public Task<T> GetEntityAsync<T>(long id) where T : DomainEntity;
        public bool EntityExists<T>(long id) where T : DomainEntity;
        public bool RemoveEntity<T>(long id) where T : DomainEntity;
    }
}