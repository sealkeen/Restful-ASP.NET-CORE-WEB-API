using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public partial class Student : Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.StudentLectures = new HashSet<LectureStudent>();
            this.StudentHomeworks = new HashSet<StudentHomework>();
            this.StudentLectures = new HashSet<LectureStudent>();
        }
        public long StudentID { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LectureStudent> StudentLectures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; }


        public new bool MapTo<T>(T target) where T : IMapable
        {
            return base.MapTo(target);
        }

        public override long GetID()
        {
            return StudentID;
        }
    }
}
