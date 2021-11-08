using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EPAM.CSCourse2021Q3.M10_Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Module10Core;
using EPAM.CSCourse2021Q3.M10_Logging;
using Microsoft.EntityFrameworkCore;
using EPAM.CSCourse2021Q3.M10_Exceptions;
using System.Collections.Generic;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    //  BLL: (+)
    //    • Сущности: Лектор, Студент, Лекция, Домашняя работа. (+)
    //    • CRUD операции для работы с лекциями  (+|-)
    //    • CRUD операции для работы с домашними работами (+|-)
    //    • CRUD операции для работы со списком студентов \ лекторов (+|-)
    public class LectionsRepository : IRepository, ILogable
    {
        private IDBContext _dbContext;
        public Microsoft.Extensions.Logging.ILogger Logger { get; set; }
        public LectionsRepository(ILogger<LectionsRepository> logger = null)
        {
            Logger = logger ?? NLogInjector.CreateLogger(EPAM.CSCourse2021Q3.M10_IO.Extension.PathResolver.CurrentDirectory());
        }
        ~LectionsRepository()
        {
            _dbContext?.Dispose();
        }

        public bool RemoveEntity<T>(long id) where T : DomainEntity
        {
            if (!EntityExists<T>(id))
            {
                throw new EntityNotFoundException($"Entity with ID:{id} was not found");
            }

            using (IDBContext dbContext = new M10DBContext())
            {
                var entity = dbContext.FindAsync<T>(id);
                dbContext.RemoveEntity<T>(entity.Result);
                dbContext.SaveChanges();
                return true;
            }
        }

        public bool EntityExists<T>(long id) where T : DomainEntity
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                var e = dbContext.FindAsync<T>(id);
                return e.Result != null;
            }
        }

        public async Task<bool> EntityExists<T>(T entity) where T : DomainEntity
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                var e = await dbContext.FindAsync<T>(entity.GetID());
                return (e != null);
            }
        }

        public async Task<bool> AddEntityAsync<T>(T entity) where T : DomainEntity
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                try
                {
                    dbContext.AddEntity(entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex.Message);
                    var e = dbContext.FindAsync<T>(entity.GetID());
                    if (e == null)
                    {
                        Logger.LogError($"{typeof(T)} not found.");
                        throw new EntityNotFoundException($"Entity have not been added. Reason: {ex.Message}");
                    }
                    throw new DbUpdateException($"Cannot add specified Entity. {ex.Message}");
                }
            }
        }
        public async Task<T> GetEntityAsync<T>(long id) where T : DomainEntity
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return await dbContext.FindAsync<T>(id);
            }
        }

        public async Task<bool> PutEntityAsync<T>(long id, T entity) where T : DomainEntity, IMapable
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                if (id != entity.GetID())
                {
                    Logger?.LogError($"{nameof(PutEntityAsync)}: the Entity's ID doesn't match the given argument ID.");
                    throw new ObjectIDsDoesntMatchException();
                }

                var foundLectors = dbContext.GetStudents().Where(l => l.StudentID == id);
                if (foundLectors.Count() > 0)
                {
                    entity.MapTo(foundLectors.FirstOrDefault());
                }

                try
                {
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Logger?.LogError(ex.Message);
                    if (!LectorExtensions.LectorExists(id))
                    {
                        throw new LectorNotFoundException($"No lector found with id {id}. {ex.Message}");
                    }
                }
            }
            return false;
        }
        public List<Homework> GetHomeworks()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return dbContext.GetHomeworks().Include(h=>h.StudentHomework.Student).ToList();
            }
        }
        public List<Lecture> GetLectures()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return dbContext.GetLectures().Include(l=>l.LectureStudents).Include(l=>l.Lector).ToList();
            }
        }
        public void DeleteLectures()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                dbContext.GetLectures().ForEach(dbContext.RemoveEntity);
                dbContext.SaveChanges();
            }
        }
        public List<Student> GetStudents()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return dbContext.GetStudents().ToList();
            }
        }
        public void DeleteStudents()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                dbContext.GetStudents().ForEach(dbContext.RemoveEntity);
                dbContext.SaveChanges();
            }
        }
        public List<Lector> GetLectors()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return dbContext.GetLectors().ToList();
            }
        }
        public void DeleteLectors()
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                dbContext.GetLectors().ForEach(dbContext.RemoveEntity);
                dbContext.SaveChanges();
            }
        }
    }
}
