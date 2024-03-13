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
    public class DoctorsController : ControllerBase
    {
        private readonly AmazeCareDBContext _context;

        public DoctorsController(AmazeCareDBContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet,Authorize(Roles ="Admin,Doctor,Patient")]
        public async Task<ActionResult<IEnumerable<Doctors>>> GetDoctors()
        {
          if (_context.Doctors == null)
          {
              return NotFound();
          }
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}"), Authorize(Roles = "Patient,Admin,Doctor")]
        public async Task<ActionResult<Doctors>> GetDoctors(int id)
        {
          if (_context.Doctors == null)
          {
              return NotFound();
          }
            var doctors = await _context.Doctors.FindAsync(id);

            if (doctors == null)
            {
                return NotFound();
            }

            return doctors;
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> PutDoctors(int id, Doctors doctors)
        {
            if (id != doctors.DoctorId)
            {
                return BadRequest();
            }

            _context.Entry(doctors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorsExists(id))
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
        public async Task<ActionResult<Doctors>> PostDoctors(Doctors doctors)
        {
          if (_context.Doctors == null)
          {
              return Problem("Entity set 'AmazeCareDBContext.Doctors'  is null.");
          }
            _context.Doctors.Add(doctors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctors", new { id = doctors.DoctorId }, doctors);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}"),Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteDoctors(int id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpGet("View Appointments"),Authorize(Roles ="Doctor")]
        public IActionResult GetAppointments(int id)
        {
            var getAppointment = _context.Doctors.Where(p => p.DoctorId == id).Join(
                _context.Appointments,
                p => p.DoctorId,
                a => a.DoctorId,
                (p, a) => new
                {
                    AppointmentId = a.AppointmentId,
                    PatientName = a.Patient.FullName,
                    ContactNumber = a.Patient.ContactNumber,
                    Date = a.AppointmentDate

                });
            return Ok(getAppointment);
        }



        private bool DoctorsExists(int id)
        {
            return (_context.Doctors?.Any(e => e.DoctorId == id)).GetValueOrDefault();
        }
    }
}
