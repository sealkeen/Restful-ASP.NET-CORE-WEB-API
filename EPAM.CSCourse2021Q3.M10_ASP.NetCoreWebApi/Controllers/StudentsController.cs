using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPAM.CSCourse2021Q3.M10_Domain;
using Module10Core;
using EPAM.CSCourse2021Q3.M10_Logic;
using EPAM.CSCourse2021Q3.M10_Logging;
using Microsoft.Extensions.Logging;

namespace EPAM.CSCourse2021Q3.M10_ASP.NetCoreWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IRepository _dbAccess;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
            _dbAccess = new LectionsRepository();
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _dbAccess.GetStudents().ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var student = StudentExtensions.GetStudent(id);
            
            if (student == null)
            {
                return NotFound();
            }

            return await student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student student)
        {
            if (id != student.GetID())
            {
                return BadRequest();
            } 
            try
            {
                await StudentExtensions.PutStudent(id, student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExtensions.StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await _dbAccess.AddEntityAsync(student);

            return CreatedAtAction("GetStudent", new { id = student.StudentID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(long id)
        {
            var student = await StudentExtensions.GetStudent(id);
            if (_dbAccess.EntityExists<Student>(id) == false)
            {
                return NotFound();
            }

            _dbAccess.RemoveEntity<Student>(id);

            return student;
        }
    }
}
