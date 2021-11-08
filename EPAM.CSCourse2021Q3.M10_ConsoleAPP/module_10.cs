using System;
using System.Linq;
using EPAM.CSCourse2021Q3.M10_Domain;
using Module10Core;
using NLog;
using EPAM.CSCourse2021Q3.M10_Logging;

namespace module_10
{
   //Разработать функционал, архитектурное решение которого будет отвечать принципам SOLID и следующим требованиям:
    //  Общие требования к реализации \ дистрибьюции
    //    • Домашнюю работу именовать module_10. (+)
    //    • Функционал должен быть разделён по слоям и назначению на сборки. (+)
    //    • Одна сборка должна содержать модели и бизнес-логику. (+|-)
    //    • Другая сборка должна содержать функционал по работе с базой данных (Repository \ DAL). (+)
    //    • Приложение должно уметь инициализировать дефолтную базу данных. Если база не создана - создать и заполнить данными, если создана - пересоздать. (+)
    public class module_10
    {
        //  Options
        //    • Библиотека не должна зависеть от собственных настроек, настройки должны подтягиваться из вызывающего ее приложения. (+)
        //    • Библиотека не должна требовать какие-то специфичные ключи из web.config основного приложения. (+)
        //    • Настройки должны быть в следующем виде: (+|-)
        //      • Если настройка обязательна, она должна поступать извне, инициализироваться потребителем (-)
        //      • Если настройка опциональная, она должна быть опциональной с дефолтным значением (+)
        //      • Если это внутренние константы и т.д. - они должны быть внутренними (+)
        public static void Main(string[] args)
        {
            using (IDBContext idbContext = new M10DBContext())
            {
                foreach(var lector in idbContext.GetLectors())
                Console.WriteLine($"Lector #{lector.LectorID}: {lector.FirstName} {lector.LastName}");
                //StaticLog.StaticNLog.Error("Error");
            }
            Console.Read();
        }
    }
}
//  Tech Stack:
//    • C# (+), .NET 5  (+)
//    • MsSql Server (-) or Postgress DB (+)
//    • Moq (-), NUnit (+)
//    • Microsoft ILogger (+), NLog (+)
//    • EF Core (+)
//    • Code standarts должен удовлетворять ms.net guideline (+|-)

//  Setting 
//    • Лектор ведет курс лекций.
//    • Студент пришeл на лекцию с домашней работой - получил оценку от 1 до 5. 
//    • Студент пришел на лекцию без домашней работы - получил 0.
//    • Студент пропустил лекцию - получил 0.
//    • Отсутствие домашней работы студента не эквивалентно отсутствию на лекции (следует вести посещаемость).


//2 Part:

//  Необходимо подключить библиотеку к .NET Core web.api rest сервису

//    • Должны быть реализованы end-points покрывающие функционал библиотеки. (+|-)
//    • В качестве Ui использовать Swagger Ui. (+)
//    • https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.0
//    • https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0
//    • https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.0

