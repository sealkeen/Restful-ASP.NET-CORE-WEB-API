using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EPAM.CSCourse2021Q3.M10_Domain;
using Module10Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPAM.CSCourse2021Q3.M10_Logging;
using EPAM.CSCourse2021Q3.M10_Logic;

namespace EPAM.CSCourse2021Q3.M10_ASP.NetCoreWebApi
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentForecastController : ControllerBase
    {
        private readonly ILogger<StudentForecastController> _logger;
        private readonly IRepository _dbAccess;

        public StudentForecastController(ILogger<StudentForecastController> logger)
        {
            _logger = logger;
            _dbAccess = new LectionsRepository();
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            try
            {
                var students = _dbAccess.GetStudents().ToList();
                return students;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return new List<Student>();
            }
        }
    }
}
