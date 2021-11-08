using System;

namespace EPAM.CSCourse2021Q3.M10_Exceptions
{
    //  Exception (+)
    //    • При конфликтах и исключительных ситуациях библиотека должна выбрасывать 
    //      строго типизированные "кастомные" исключения, определенные в ней. (+)
    public class ApplicationCoreException : Exception
    {
        public ApplicationCoreException(string message) : base(message) 
        { 

        }
    }
}
