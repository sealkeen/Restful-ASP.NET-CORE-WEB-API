using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public abstract class Person : DomainEntity, IMapable
    {
        public abstract string FirstName { get; set; }
        public abstract string LastName { get; set; }
        public bool Map(Person source, Person target)
        {
            if (source != null && target != null)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                return true;
            }
            return false;
        }
        public override bool MapTo<T>(T target)
        {
            if (this != null && target != null)
            {
                (target as Person).FirstName = FirstName;
                (target as Person).LastName = LastName;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return this.GetInfo();
        }
    }
}
