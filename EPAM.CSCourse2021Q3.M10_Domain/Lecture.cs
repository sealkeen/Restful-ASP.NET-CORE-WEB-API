using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public partial class Lecture : DomainEntity, IMapable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lecture()
        {
            this.LectureStudents = new HashSet<LectureStudent>();
        }
        public long LectureID { get; set; }
        public Nullable<long> LectorID { get; set; }
        public Lector Lector { get; set; }
        public DateTime DateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LectureStudent> LectureStudents { get; set; }

        public new bool MapTo<T>(T target) where T : IMapable
        {
            var dT = (target as Lecture)?.DateTime;
            if (dT != null)
                DateTime = dT.Value;
            return true;
        }
        public override long GetID()
        {
            return LectureID;
        }
        public override string ToString()
        {
            return this.GetInfo();
        }
    }
}
