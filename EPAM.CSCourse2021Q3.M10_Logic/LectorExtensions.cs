using EPAM.CSCourse2021Q3.M10_Domain;
using Microsoft.EntityFrameworkCore;
using Module10Core;
using EPAM.CSCourse2021Q3.M10_Exceptions;
using EPAM.CSCourse2021Q3.M10_Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2021Q3.M10_Logic
{
    public static class LectorExtensions
    {
        public static string GetInfo(this Lector lector)
        {
            string result = "";
            if (lector == null || lector.LectorID < 0)
            {
                result += "{ \"Lector's is absent (No such).\" }";
                throw new LectorNotFoundException(result);
            }
            else
            {
                result += $"{{ LectorID: {lector.LectorID}, ";
                result += $"{ PersonExtension.GetName(lector) } }}";
            }
            return result;
        }
        public static IEnumerable<Lector> GetLectors()
        {
            using (IDBContext dBContext = new M10DBContext())
            {
                return dBContext.GetLectors();
            }
        }
        public static async Task<Lector> GetLector(long id)
        {
            using (IDBContext dBContext = new M10DBContext())
            {
                var lector = await dBContext.FindAsync<Lector>(id);

                if (lector == null)
                {
                    throw new LectorNotFoundException("The requested lector wasn't found.");
                }
                return lector;
            }
        }
        public static void CopyProperties(Lector source, Lector target)
        {
            if (source != null && target != null)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
            }
        }
        public static async Task<bool> PutLector(long id, Lector sourceLector)
        {
            using (IDBContext dbContext = new M10DBContext())
            {
                if (id != sourceLector.LectorID)
                {
                    throw new ObjectIDsDoesntMatchException("the Lector's ID doesn't match the given argument ID.");
                }

                var foundLectors = dbContext.GetLectors().Where(l => l.LectorID == id);
                if (foundLectors.Count() > 0)
                {
                    CopyProperties(sourceLector, foundLectors.FirstOrDefault());;
                }

                try
                {
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!LectorExtensions.LectorExists(id))
                    {
                        throw new LectorNotFoundException($"No lector found with id {id}. {ex.Message}");
                    }
                }
            }
            return false;
        }
        public static bool LectorExists(long id)
        {
            return GetLectors().Any(e => e.LectorID == id);
        }

    }
}
