using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPAM.CSCourse2021Q3.M10_Logic;
using EPAM.CSCourse2021Q3.M10_Domain;
using Module10Core;
using Microsoft.Extensions.Logging;
using EPAM.CSCourse2021Q3.M10_Logging;

namespace EPAM.CSCourse2021Q3.M10_ASP.NetCoreWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly ILogger<HomeworkController> _logger;
        private readonly IRepository _repository;

        public HomeworkController(ILogger<HomeworkController> logger)
        {
            _logger = logger;
            _repository = new LectionsRepository();
        }

        // GET: api/Homework
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworks()
        {
            return await _repository.GetHomeworks().ToListAsync();
        }

        // GET: api/Homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> GetHomework(long id)
        {
            var homework = _repository.GetEntityAsync<Homework>(id);

            if (homework == null)
            {
                return NotFound();
            }

            return await homework;
        }

        // PUT: api/Homework/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomework(long id, Homework homework)
        {
            if (id != homework.GetID())
            {
                return BadRequest();
            }
            try
            {
                await _repository.PutEntityAsync(id, homework);
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

        // POST: api/Homework
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Homework>> PostHomework(Homework homework)
        {
            await _repository.AddEntityAsync(homework);

            return CreatedAtAction(nameof(GetHomework), new { id = homework.HomeworkID }, homework);
        }

        // DELETE: api/Homework/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Homework>> DeleteHomework(long id)
        {
            var homework = await _repository.GetEntityAsync<Homework>(id);
            if (_repository.EntityExists<Student>(id) == false)
            {
                return NotFound();
            }

            _repository.RemoveEntity<Student>(id);

            return homework;
        }
    }
}
