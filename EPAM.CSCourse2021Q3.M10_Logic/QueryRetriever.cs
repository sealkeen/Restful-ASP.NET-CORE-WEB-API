using EPAM.CSCourse2021Q3.M10_Domain;
using Module10Core;
using EPAM.CSCourse2021Q3.M10_Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public static class QueryRetriever
    {
        public static IQueryable<T> RetrieveQuery<T>(IQueryable<T> sourceQuery, Func<IQueryable<T>, IQueryable<T>> inquery) where T : class
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                return inquery(sourceQuery);
            }
        }

        public static void Append(StringBuilder sB, object o)
        {
            sB.Append(o.ToString());
        }

        public static string ToElementString<T>(this IQueryable<T> ts) 
        {
            StringBuilder stringBuilder = new StringBuilder();
            ts.ForEach(x => Append(stringBuilder, x));
            return ts.ToString();
        }
    }
}
