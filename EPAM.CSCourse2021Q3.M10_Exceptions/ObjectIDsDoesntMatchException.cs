using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Exceptions
{
    public class ObjectIDsDoesntMatchException : WebAPIException
    {
        public ObjectIDsDoesntMatchException(string message = "the Entity's ID doesn't match the given argument ID.") : base(message)
        {
            
        }
    }
}
