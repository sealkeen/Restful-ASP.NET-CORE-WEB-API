using Module10Core;
using EPAM.CSCourse2021Q3.M10_Logging;
using System;
using System.Collections.Generic;
using System.Text;
using EPAM.CSCourse2021Q3.M10_Domain;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public static class IDBContextExtensions
    {
        public static void ApplyActionToDBContext(this IDBContext dbContext, Action<IDBContext> action)
        {
            try
            {
                if (dbContext == null)
                    dbContext = new M10DBContext();
                action(dbContext);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
