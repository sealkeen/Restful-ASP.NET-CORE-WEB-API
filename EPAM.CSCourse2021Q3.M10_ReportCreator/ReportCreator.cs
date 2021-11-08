using EPAM.CSCourse2016.SilkinIvan.JSONParser;
using Microsoft.Extensions.Logging;
using EPAM.CSCourse2021Q3.M10_Domain;
using System;
using EPAM.CSCourse2021Q3.M10_Logic;

namespace EPAM.CSCourse2021Q3.M10_ReportCreator
{
    public class ReportCreator
    {
        private ILogger Logger { get; set; }
        public ReportCreator(ILogger logger)
        {
            Logger = logger;
        }
        //  Functional: 
        //    • Генерация репорта о посещаемости, по названию лекции или по имени студента.
        //      Отчет поддерживает 2 формата - Xml / Json (формат свободный).  (+)
        //      Реализация должна поддерживать возможность легкой расширяемости под разные форматы репортов. (+|-)
        //    • При пропуске студентом более 3х лекций необходимо отсылать Email на лектора курса и на студента. (-)
        //    • При достижении текущей средней оценки за курс ниже 4х баллов необходимо отсылать SMS сообщение студенту. (-)
        public string CreateJSONReport(IDBContext dbContext)
        {
            try
            {
                JRoot jRoot = new JRoot();
                foreach (var lecture in dbContext.GetLectures())
                {
                    JObject jLecture = new JObject(jRoot);
                    foreach (var ls in lecture.LectureStudents)
                    {
                        jLecture.Add(new JObject(new JSingleValue(ls.Student.GetInfo())));
                    }
                }

                return jRoot.ToString();
            }
            catch (NotImplementedException ex)
            {
                Logger.LogError(ex.Message);
                return "{ null }";
            }
        }

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}
