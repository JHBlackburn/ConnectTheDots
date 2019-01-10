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
    public class ConnectionsController : ControllerBase
    {
        private readonly ConnectTheDotsContext _context;

        public ConnectionsController(ConnectTheDotsContext context)
        {
            _context = context;
        }

        // GET: api/Connections
        [HttpGet]
        public IEnumerable<Connection> GetConnection()
        {
            return _context.Connection;
        }

        // GET: api/Connections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConnection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var connection = await _context.Connection.FindAsync(id);

            if (connection == null)
            {
                return NotFound();
            }

            return Ok(connection);
        }

        // PUT: api/Connections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConnection([FromRoute] int id, [FromBody] Connection connection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != connection.ConnectionId)
            {
                return BadRequest();
            }

            _context.Entry(connection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectionExists(id))
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

        // POST: api/Connections
        [HttpPost]
        public async Task<IActionResult> PostConnection([FromBody] Connection connection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Connection.Add(connection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConnection", new { id = connection.ConnectionId }, connection);
        }

        // DELETE: api/Connections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var connection = await _context.Connection.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }

            _context.Connection.Remove(connection);
            await _context.SaveChangesAsync();

            return Ok(connection);
        }

        private bool ConnectionExists(int id)
        {
            return _context.Connection.Any(e => e.ConnectionId == id);
        }
    }
}