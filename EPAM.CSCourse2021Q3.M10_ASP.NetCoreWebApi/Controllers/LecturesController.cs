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
using Microsoft.Extensions.Logging;

namespace EPAM.CSCourse2021Q3.M10_ASP.NetCoreWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ILogger<LecturesController> _logger;
        private IRepository _dbAccess;

        public LecturesController(ILogger<LecturesController> logger)
        {
            _logger = logger;
            _dbAccess = new LectionsRepository();
        }

        // GET: api/Lectures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecture>>> GetLectures()
        {
            //List<string> lectures = new List<string>();
            //_dbAccess.GetLectures().ForEach(l => lectures.Add(l.ToXML().ToString()));
            //var tsk = Task.Factory.StartNew(() => lectures.Aggregate((a, b) => a + b));
            //return await tsk;
            var tsk = Task.Factory.StartNew(() => _dbAccess.GetLectures());
            return await tsk;
        }

        // GET: api/Lectures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecture>> GetLecture(long id)
        {
            var lecture = _dbAccess.GetEntityAsync<Lecture>(id);

            if (await lecture == null)
            {
                return NotFound();
            }

            return await lecture;
        }

        // PUT: api/Lectures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecture(long id, Lecture lecture)
        {
            if (id != lecture.LectureID)
            {
                return BadRequest();
            }

            try
            {
                bool success = await _dbAccess.PutEntityAsync<Lecture>(lecture.LectureID, lecture);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbAccess.EntityExists<Lecture>(id))
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

        // POST: api/Lectures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lecture>> PostLecture(Lecture lecture)
        {
            bool success = await _dbAccess.AddEntityAsync(lecture);

            if(success)
                return CreatedAtAction("GetLecture", new { id = lecture.LectureID }, lecture);
            else
                return BadRequest();
        }

        // DELETE: api/Lectures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lecture>> DeleteLecture(long id)
        {
            var exists = _dbAccess.EntityExists<Lecture>(id);
            if (!exists)
            {
                return NotFound();
            }

            _dbAccess.RemoveEntity<Lecture>(id);

            return await _dbAccess.GetEntityAsync<Lecture>(id);
        }
    }
}
