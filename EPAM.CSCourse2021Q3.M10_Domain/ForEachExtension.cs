using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Domain
{
    public static class ForEachExtension
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> sources, Action<TSource> action)
        {
            foreach (var i in sources)
            {
                action(i);
            }
        }
    }
}
