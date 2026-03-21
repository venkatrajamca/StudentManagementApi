
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;
using StudentManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _repo;
        public StudentController(StudentRepository repo)
        {
            _repo = repo;
        }   

        // GET: api/<StudentController>
        //[httpget]
        //public iactionresult getalluser() => ok(_repo.getusers());


        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await Task.Run(() => _repo.GetUsers());
            return Ok(users);
        }   

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var oUser=_repo.GetUserById(id);
            return oUser != null ? Ok(oUser) : NotFound();  
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Insert(User user)
        {
            _repo.InserUser(user);
            return Ok("User Added");
        } 

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Update(User user)
        {
            _repo.UpdateUser(user);
            return Ok("User Updated");                
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.DeleteUser(id);
            return Ok("User Deleted");
        }

        //******************************GENERATE JWT TOKEN*************************************
        //[HttpPost("token")]
        //public IActionResult GenerateToken()
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here_123!"));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, "student"),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: "yourIssuer",
        //        audience: "yourAudience",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: credentials);

        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        //    return Ok(new { token = tokenString });
        //}
        //[Authorize]
        //[HttpGet]
        //public IEnumerable<string> GetAuthorize()
        //{
        //    return new string[] { "value1", "value2" };
        //}
    }

}
