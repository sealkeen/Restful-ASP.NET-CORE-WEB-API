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
    public class LectorsController : ControllerBase
    {
        private readonly ILogger<LectorsController> _logger;
        private readonly IRepository _dbAccess;

        public LectorsController(ILogger<LectorsController> logger)
        {
            _logger = logger;
            _dbAccess = new LectionsRepository();
        }

        // GET: api/Lectors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lector>>> GetLectors()
        {
            return await LectorExtensions.GetLectors().ToListAsync();
        }

        // GET: api/Lectors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lector>> GetLector(long id)
        {
            var lector = LectorExtensions.GetLector(id);

            if (lector == null)
            {
                return NotFound();
            }

            return await lector;
        }

        // PUT: api/Lectors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLector(long id, Lector lector)
        {
            if (id != lector.GetID())
            {
                return BadRequest();
            }
            try
            {
                await LectorExtensions.PutLector(id, lector);
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

        // POST: api/Lectors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lector>> PostLector(Lector lector)
        {
            await _dbAccess?.AddEntityAsync(lector);

            return CreatedAtAction("GetLector", new { id = lector.LectorID }, lector);
        }

        // DELETE: api/Lectors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lector>> DeleteLector(long id)
        {
            var lector = await LectorExtensions.GetLector(id);
            if (_dbAccess.EntityExists<Student>(id) == false)
            {
                return NotFound();
            }

            _dbAccess?.RemoveEntity<Student>(id);

            return lector;
        }

    }
}
