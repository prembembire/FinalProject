using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazeCareProject.Data;
using CodeFirst.Models;
using Microsoft.AspNetCore.Authorization;

namespace AmazeCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly AmazeCareDBContext _context;

        public MedicalRecordsController(AmazeCareDBContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecords
        [HttpGet,Authorize(Roles ="Admin,Patient,Doctor")]
        public async Task<ActionResult<IEnumerable<MedicalRecords>>> GetMedicalRecords()
        {
          if (_context.MedicalRecords == null)
          {
              return NotFound();
          }
            return await _context.MedicalRecords.ToListAsync();
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}"), Authorize(Roles = "Patient,Doctor")]
        public async Task<ActionResult<MedicalRecords>> GetMedicalRecords(int id)
        {
          if (_context.MedicalRecords == null)
          {
              return NotFound();
          }
            var medicalRecords = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecords == null)
            {
                return NotFound();
            }

            return medicalRecords;
        }

        [HttpGet("DoctorId"), Authorize(Roles = "Doctor")]
        public async Task<ActionResult<MedicalRecords>> GetRecords(int id)
        {
            if (_context.MedicalRecords == null)
            {
                return NotFound();
            }
            var medicalRecords = await _context.MedicalRecords.FirstOrDefaultAsync(m=>m.DoctorId==id);

            if (medicalRecords == null)
            {
                return NotFound();
            }

            return medicalRecords;
        }

  

        [HttpPut("{id}"), Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutMedicalRecords(int id, MedicalRecords medicalRecords)
        {
            if (id != medicalRecords.RecordId)
            {
                return BadRequest();
            }

            _context.Entry(medicalRecords).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalRecordsExists(id))
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

        [HttpPost,Authorize(Roles ="Doctor")]
        public async Task<ActionResult<MedicalRecords>> PostMedicalRecords(MedicalRecords medicalRecords)
        {
          if (_context.MedicalRecords == null)
          {
              return Problem("Entity set 'AmazeCareDBContext.MedicalRecords'  is null.");
          }
            _context.MedicalRecords.Add(medicalRecords);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalRecords", new { id = medicalRecords.RecordId }, medicalRecords);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecords(int id)
        {
            if (_context.MedicalRecords == null)
            {
                return NotFound();
            }
            var medicalRecords = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecords == null)
            {
                return NotFound();
            }

            _context.MedicalRecords.Remove(medicalRecords);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordsExists(int id)
        {
            return (_context.MedicalRecords?.Any(e => e.RecordId == id)).GetValueOrDefault();
        }
    }
}
