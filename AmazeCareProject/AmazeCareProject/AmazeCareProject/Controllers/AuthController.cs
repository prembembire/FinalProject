using AmazeCareProject.Data;
using AmazeCareProject.Models;
using CodeFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazeCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AmazeCareDBContext _dbContext;
     

        public AuthController(IConfiguration config, AmazeCareDBContext dBContext)
        {
            _config = config;
            _dbContext = dBContext;
        }


        [AllowAnonymous] // Anyone can use
        [HttpPost]


        public IActionResult Auth([FromBody] User user)
        {
            IActionResult response = Unauthorized();
            if (user != null)
            {



                if (user.SelectedRole == "Patient")
                {
                    var dbUser = _dbContext.Patient.FirstOrDefault(p => p.UserName == user.UserName);
                    if ((user.UserName == dbUser.UserName) && (user.Password == dbUser.Password))
                    {
                        var issuer = _config["Jwt:Issuer"];
                        var audience = _config["Jwt:Audience"];
                        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                        var subject = new ClaimsIdentity(new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.SelectedRole),
                    });
                        var expires = DateTime.UtcNow.AddMinutes(100);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = subject,
                            SigningCredentials = signingCredentials,
                            Expires = expires,
                            Issuer = issuer,
                            Audience = audience 
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                        
                        return Ok(new AuthenticatedResponse { token = jwtToken, id = dbUser.PatientId });
                    }


                }
                if (user.SelectedRole == "Doctor")
                {
                    var dbUser = _dbContext.Doctors.FirstOrDefault(p => p.UserName == user.UserName);
                    if ((user.UserName == dbUser.UserName) && (user.Password == dbUser.Password))
                    {
                        var issuer = _config["Jwt:Issuer"];
                        var audience = _config["Jwt:Audience"];
                        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                        var subject = new ClaimsIdentity(new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.SelectedRole),
                    });
                        var expires = DateTime.UtcNow.AddMinutes(10);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = subject,
                            SigningCredentials = signingCredentials,
                            Expires = expires,
                            Issuer = issuer,
                            Audience = audience
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                        
                        return Ok(new AuthenticatedResponse { token = jwtToken, id = dbUser.DoctorId });
                    }


                }


                if (user.SelectedRole == "Admin")
                {
                    var dbUser = _dbContext.Admin.FirstOrDefault(p => p.UserName == user.UserName);
                    if ((user.UserName == dbUser.UserName) && (user.Password == dbUser.Password))
                    {
                        var issuer = _config["Jwt:Issuer"];
                        var audience = _config["Jwt:Audience"];
                        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                        var subject = new ClaimsIdentity(new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                       
                        new Claim(ClaimTypes.Role,user.SelectedRole)
                    });
                        var expires = DateTime.UtcNow.AddMinutes(10);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = subject,
                            SigningCredentials = signingCredentials,
                            Expires = expires,
                            Issuer = issuer,
                            Audience = audience 
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                       
                        return Ok(new AuthenticatedResponse { token = jwtToken, id = dbUser.AdminId });
                    }
                }

            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { error = "Invalid user data provided." });
            }

            // Determine the user role
            switch (user.SelectedRole)
            {
                case "Patient":
                    var patient = _dbContext.Patient.FirstOrDefault(p => p.UserName == user.UserName);
                    if (patient == null)
                    {
                        return NotFound(new { error = "No patient found with the provided username." });
                    }
                    patient.Password = user.Password;
                    break;

                case "Doctor":
                    var doctor = _dbContext.Doctors.FirstOrDefault(d => d.UserName == user.UserName);
                    if (doctor == null)
                    {
                        return NotFound(new { error = "No doctor found with the provided username." });
                    }
                    doctor.Password = user.Password;
                    break;

                case "Admin":
                    var admin = _dbContext.Admin.FirstOrDefault(a => a.UserName == user.UserName);
                    if (admin == null)
                    {
                        return NotFound(new { error = "No admin found with the provided username." });
                    }
                    admin.Password = user.Password;
                    break;

                default:
                    return BadRequest(new { error = "Invalid role provided." });
            }

            try
            {
                _dbContext.SaveChanges(); // Save changes to the database
                return Ok(new { message = "Password reset successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error occurred while resetting the password: {ex.Message}" });
            }
        }









    }
    }


