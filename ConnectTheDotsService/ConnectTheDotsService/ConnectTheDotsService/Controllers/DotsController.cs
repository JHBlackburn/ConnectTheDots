using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConnectTheDotsService.Models;

namespace ConnectTheDotsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotsController : ControllerBase
    {
        private readonly ConnectTheDotsContext _context;

        public DotsController(ConnectTheDotsContext context)
        {
            _context = context;
        }

        // GET: api/Dots
        [HttpGet]
        public IEnumerable<Dot> GetDot()
        {
            return _context.Dot;
        }

        // GET: api/Dots/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDot([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dot = await _context.Dot.FindAsync(id);

            if (dot == null)
            {
                return NotFound();
            }

            return Ok(dot);
        }

        // PUT: api/Dots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDot([FromRoute] int id, [FromBody] Dot dot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dot.DotId)
            {
                return BadRequest();
            }

            _context.Entry(dot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DotExists(id))
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

        // POST: api/Dots
        [HttpPost]
        public async Task<IActionResult> PostDot([FromBody] Dot dot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dot.Add(dot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDot", new { id = dot.DotId }, dot);
        }

        // DELETE: api/Dots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDot([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dot = await _context.Dot.FindAsync(id);
            if (dot == null)
            {
                return NotFound();
            }

            _context.Dot.Remove(dot);
            await _context.SaveChangesAsync();

            return Ok(dot);
        }

        private bool DotExists(int id)
        {
            return _context.Dot.Any(e => e.DotId == id);
        }
    }
}