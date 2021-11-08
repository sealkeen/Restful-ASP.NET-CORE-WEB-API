using Microsoft.EntityFrameworkCore;
using System;
using EPAM.CSCourse2021Q3.M10_Domain;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EPAM.CSCourse2021Q3.M10_Logging;

namespace Module10Core
{
    public class M10DBContext : DbContext, IDBContext, ILogable
    {
        public M10DBContext(ILogger<M10DBContext> logger = null)
        {
            Logger = logger ?? NLogInjector.CreateLogger(EPAM.CSCourse2021Q3.M10_IO.Extension.PathResolver.CurrentDirectory());

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public static M10DBContext CreateContext()
        {
            M10DBContext result = new M10DBContext();
            return result;
        }

        public static async Task<M10DBContext> CreateContextAsync()
        {
            var result = Task.Factory.StartNew(CreateContext);
            return await result;
        }

        public Microsoft.Extensions.Logging.ILogger Logger { get; set; }

        public DbSet<Lector> Lectors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentHomework> StudentHomeworks { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureStudent> LectureStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LectureStudent>()
                .HasKey(pc => new { pc.StudentID, pc.LectureID });

            modelBuilder.Entity<LectureStudent>()
                .HasOne(pc => pc.Student)
                .WithMany(p => p.StudentLectures)
                .HasForeignKey(pc => pc.StudentID);

            modelBuilder.Entity<LectureStudent>()
                .HasOne(pc => pc.Lecture)
                .WithMany(c => c.LectureStudents)
                .HasForeignKey(pc => pc.LectureID);

            modelBuilder.Entity<StudentHomework>()
                .HasKey(sh => new { sh.StudentID, sh.HomeworkID });

            modelBuilder.Entity<StudentHomework>()
                .HasOne(s => s.Student)
                .WithMany(sh => sh.StudentHomeworks)
                .HasForeignKey(sh => sh.StudentID);

            modelBuilder.Entity<StudentHomework>()
                .HasOne(h => h.Homework);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=m10_testDB_0;username=postgres;password=p4ssw0rds4mpl3");
            }
            catch (Exception ex) { Logger?.LogError(ex.Message); }
        }
        public void AddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                this.Add(entity);
                SaveChanges();
            } 
            catch (Exception ex) { Logger?.LogError(ex.Message); }
        }
        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                this.Remove(entity);
            } 
            catch (Exception ex) { Logger?.LogError( ex.Message); }
        }
        public IQueryable<Lector> GetLectors() => Lectors;
        public IQueryable<Student> GetStudents() => Students;
        public IQueryable<StudentHomework> GetStudentHomeworks() => StudentHomeworks;
        public IQueryable<Lecture> GetLectures() => Lectures;
        public IQueryable<LectureStudent> GetLectureStudents() => LectureStudents;
        public IQueryable<Homework> GetHomeworks() => Homeworks;
    }
}
