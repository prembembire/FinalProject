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
    public class PatientsController : ControllerBase
    {
        private readonly AmazeCareDBContext _context;

        public PatientsController(AmazeCareDBContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
          if (_context.Patient == null)
          {
              return NotFound();
          }
            return await _context.Patient.ToListAsync();
        }

        // GET: api/Patients/5

    

        [HttpGet("{id}"), Authorize(Roles = "Patient,Admin")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            if (_context.Patient == null)
            {
                return NotFound();
            }
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPut("{id}"), Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        [HttpPost,Authorize(Roles ="Admin,Patient")]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
          if (_context.Patient == null)
          {
              return Problem("Entity set 'AmazeCareDBContext.Patient'  is null.");
          }
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.PatientId }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}"),Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (_context.Patient == null)
            {
                return NotFound();
            }
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        [HttpGet("View MedicalHistory"),Authorize(Roles ="Patient,Admin")]
        public IActionResult GetMedicalHistory(int id)
        {
            var getMedicalHistory = _context.Patient.Where(p => p.PatientId == id).Join(
                _context.MedicalRecords,
                p => p.PatientId,
                m => m.PatientId,
                (p, m) => new
                {
                    PatientName = p.FullName,
                    DoctorName = m.Doctors.Name,
                    Symptoms = m.Symptoms,
                    Treatment = m.TreatmentPlan,
                    TestDone = m.TestsRecommended,
                    Prescription = m.Prescription,



                });
            return Ok(getMedicalHistory);
        }


        [HttpGet("ViewAppointments"),Authorize(Roles ="Patient,Admin")]
        public IActionResult GetAppointments(int id)
        {
            var getAppointment = _context.Patient.Where(p => p.PatientId == id).Join(
                _context.Appointments,
                p => p.PatientId,
                a => a.PatientId,
                (p, a) => new
                {
                    PatientName= p.FullName,
                    AppointmentId=a.AppointmentId,
                    DoctorName=a.Doctors.Name,
                    Date = a.AppointmentDate,
                    Status=a.Status

                });
            return Ok(getAppointment);
        }


        private bool PatientExists(int id)
        {
            return (_context.Patient?.Any(e => e.PatientId == id)).GetValueOrDefault();
        }
    }
}
















































//[HttpGet("View MedicalHistory")]
//public IActionResult GetMedicalHistory(int id)
//{
//    try
//    {
//        var getMedicalHistory = _context.Patient.Where(p => p.PatientId == id).Join(
//            _context.MedicalRecords,
//            p => p.PatientId,
//            m => m.PatientId,
//            (p, m) => new
//            {
//                PatientName = p.FullName,
//                DoctorName = m.Doctors.Name,
//                Symptoms = m.Symptoms,
//                Treatment = m.TreatmentPlan,
//                TestDone = m.TestsRecommended,
//                Prescription = m.Prescription
//            });

//        if (!getMedicalHistory.Any())
//        {
//            throw new PatientNotFoundException($"Medical history not found for patient with ID {id}.");
//        }

//        return Ok(getMedicalHistory);
//    }
//    catch (PatientNotFoundException ex)
//    {
//        // Log the exception for further investigation
//        // logger.LogError(ex, "Medical history not found.");

//        // Return a user-friendly error message
//        return NotFound(ex.Message);
//    }
//    catch (Exception ex)
//    {
//        // Log the exception for further investigation
//        // logger.LogError(ex, "An error occurred while fetching medical history.");

//        // Return a user-friendly error message
//        return StatusCode(500, "An unexpected error occurred while fetching medical history. Please try again later.");
//    }
//}





//[HttpGet("{id}")]
//public async Task<ActionResult<Patient>> GetPatient(int id)
//{
//    try
//    {
//        if (_context.Patient == null)
//        {
//            throw new PatientNotFoundException("Patient database not available.");
//        }

//        var patient = await _context.Patient.FindAsync(id);

//        if (patient == null)
//        {
//            throw new PatientNotFoundException($"Patient with ID {id} not found.");
//        }

//        return patient;
//    }
//    catch (PatientNotFoundException ex)
//    {
//        // Log the exception for further investigation
//        // logger.LogError(ex, "Patient not found.");

//        // Return a user-friendly error message
//        return NotFound(ex.Message);
//    }
//    catch (Exception ex)
//    {
//        // Log the exception for further investigation
//        // logger.LogError(ex, "An error occurred while fetching patient.");

//        // Return a user-friendly error message
//        return StatusCode(500, "An unexpected error occurred while fetching patient. Please try again later.");
//    }
//}