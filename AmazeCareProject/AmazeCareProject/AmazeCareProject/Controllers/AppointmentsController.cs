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
using AmazeCareProject.Exceptions;

namespace AmazeCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AmazeCareDBContext _context;

        public AppointmentsController(AmazeCareDBContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet,Authorize(Roles ="Admin,Doctor,Patient")]
        
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAppointments()
        {
          if (_context.Appointments == null)
          {
              return NotFound();
          }
            return await _context.Appointments.ToListAsync();
        }
        

        // GET: api/Appointments/5
        [HttpGet("{id}"), Authorize(Roles = "Admin,Doctor,Patient")]
        

        public async Task<ActionResult<Appointments>> GetAppointments(int id)
        {
          if (_context.Appointments == null)
          {
              return NotFound();
          }
            var appointments = await _context.Appointments.FindAsync(id);

            if (appointments == null)
            {
                return NotFound();
            }

            return appointments;
        }


        [HttpPut("{id}"), Authorize(Roles = "Admin,Doctor,Patient")]
        
        public async Task<IActionResult> PutAppointments(int id, Appointments appointments)
        {
            if (id != appointments.AppointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsExists(id))
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

        [HttpPut("DoctorId"), Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointments appointments)
        {
            try
            {
                if (id != appointments.DoctorId)
                    return BadRequest("Invalid Doctor ID.");

                _context.Entry(appointments).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsExists(id))
                    return NotFound("Appointment not found.");

                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: " + ex.Message);
            }
        }



        [HttpPost("BookAppointment"), Authorize(Roles = "Admin,Patient")]
        public async Task<Appointments> AddAppointment(Appointments appointments)
        {
            
            if (appointments.AppointmentDate <= DateTime.Now)
            {
                throw new InvalidAppointmentDateTimeException();
            }
            
            var existingAppointments = await _context.Appointments.ToListAsync();
            
            var conflictingAppointments = existingAppointments
                .Where(a => a.DoctorId == appointments.DoctorId &&
                            Math.Abs((a.AppointmentDate - appointments.AppointmentDate).TotalMinutes) < 15)
                .ToList();
            
            if (conflictingAppointments.Any())
            {
                throw new();
            }
           
            _context.Appointments.Add(appointments);
            await _context.SaveChangesAsync();
            return appointments;
        }



        [HttpPost, Authorize(Roles = "Admin,Patient")]
        public async Task<ActionResult<Appointments>> PostAppointments(Appointments appointments)
        {
          if (_context.Appointments == null)
          {
              return Problem("Entity set 'AmazeCareDBContext.Appointments'  is null.");
          }
            _context.Appointments.Add(appointments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointments", new { id = appointments.AppointmentId }, appointments);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> DeleteAppointments(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentsExists(int id)
        {
            return (_context.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
