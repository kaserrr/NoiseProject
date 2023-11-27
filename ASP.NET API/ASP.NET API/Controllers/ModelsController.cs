using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET_API.Models;

namespace ASP.NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ContextModels _context;

        public ModelsController(ContextModels context)
        {
            _context = context;
        }

        // GET: api/Models
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Models>>> GetSensorData()
        {
          if (_context.SensorData == null)
          {
              return NotFound();
          }
            return await _context.SensorData.ToListAsync();
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Models    >> GetModels(long id)
        {
          if (_context.SensorData == null)
          {
              return NotFound();
          }
            var models = await _context.SensorData.FindAsync(id);

            if (models == null)
            {
                return NotFound();
            }

            return models;
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModels(long id, Models.Models models)
        {
            if (id != models.Id)
            {
                return BadRequest();
            }

            _context.Entry(models).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelsExists(id))
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

        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Models>> PostModels(Models.Models models)
        {
          if (_context.SensorData == null)
          {
              return Problem("Entity set 'ContextModels.SensorData'  is null.");
          }
            _context.SensorData.Add(models);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof("GetModels", new { id = models.Id }, models));
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModels(long id)
        {
            if (_context.SensorData == null)
            {
                return NotFound();
            }
            var models = await _context.SensorData.FindAsync(id);
            if (models == null)
            {
                return NotFound();
            }

            _context.SensorData.Remove(models);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelsExists(long id)
        {
            return (_context.SensorData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
