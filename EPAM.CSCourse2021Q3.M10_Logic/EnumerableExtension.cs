using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public static class EnumerableExtension
    {
        public async static Task<List<T>> ToListAsync<T>(this IEnumerable<T> queryable)
        {
            return await Task.Factory.StartNew(queryable.ToList);
        }
    }
}
