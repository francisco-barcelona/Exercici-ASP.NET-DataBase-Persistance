using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpleatsSQL.Models;

namespace EmpleatsSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class workersController : ControllerBase
    {
        private readonly workerContext _context;

        public workersController(workerContext context)
        {
            _context = context;
        }

        // GET: api/workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<worker>>> GetworkerContexts()
        {
            return await _context.workerContexts.ToListAsync();
        }

        // GET: api/workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<worker>> Getworker(long id)
        {
            var worker = await _context.workerContexts.FindAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        // PUT: api/workers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putworker(long id, worker worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!workerExists(id))
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

        // POST: api/workers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<worker>> Postworker(worker worker)
        {
            _context.workerContexts.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getworker", new { id = worker.Id }, worker);
        }

        // DELETE: api/workers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<worker>> Deleteworker(long id)
        {
            var worker = await _context.workerContexts.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.workerContexts.Remove(worker);
            await _context.SaveChangesAsync();

            return worker;
        }

        private bool workerExists(long id)
        {
            return _context.workerContexts.Any(e => e.Id == id);
        }
    }
}
