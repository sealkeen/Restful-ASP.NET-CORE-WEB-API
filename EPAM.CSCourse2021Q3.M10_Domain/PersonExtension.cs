using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.CSCourse2021Q3.M10_Domain;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public static class PersonExtension
    {
        public static Person Create(string firstName, string LastName, bool isStudent = true)
        {
            Person person;
            if (isStudent) { person = new Student(); } 
            else { person =  new Lector(); }
            person.FirstName = firstName;
            person.LastName = LastName;
            return person;
        }

        public static string GetName(this Person person) 
        {
            return $"Name: \"{person.FirstName} {person.LastName}\"";
        }
        public static string GetInfo(this Person person)
        {
            return GetName(person);
        }

        public static bool MapTo(this Person source, Person target)
        {
            if (source != null && target != null)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                return true;
            }
            return false;
        }
    }
}