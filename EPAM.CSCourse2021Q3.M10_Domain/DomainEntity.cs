using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public abstract class DomainEntity
    {
        public abstract long GetID();
        public bool IsNotNull()
        {
            return !(this is null);
        }
        public virtual bool MapTo<T>(T target) where T : IMapable
        {
            return MapTo(target);
        }
    }
}