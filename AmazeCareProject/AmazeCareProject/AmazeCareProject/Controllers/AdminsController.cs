

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazeCareProject.Data;
using CodeFirst.Models;

namespace AmazeCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly AmazeCareDBContext _context;

        public AdminsController(AmazeCareDBContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            try
            {
                var admins = await _context.Admin.ToListAsync();
                if (admins == null || admins.Count == 0)
                    return NotFound();

                return admins;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            try
            {
                var admin = await _context.Admin.FindAsync(id);
                if (admin == null)
                    return NotFound();

                return admin;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/Admins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            try
            {
                if (id != admin.AdminId)
                    return BadRequest("Admin ID mismatch");

                _context.Entry(admin).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
                    return NotFound();

                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Admins
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            try
            {
                if (_context.Admin == null)
                    return Problem("Entity set 'AmazeCareDBContext.Admin' is null.");

                _context.Admin.Add(admin);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                var admin = await _context.Admin.FindAsync(id);
                if (admin == null)
                    return NotFound();

                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private bool AdminExists(int id)
        {
            return (_context.Admin?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}





