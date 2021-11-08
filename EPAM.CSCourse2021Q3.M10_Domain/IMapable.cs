using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public interface IMapable
    {
        bool MapTo<T>(T target) where T : IMapable;
    }
}
