using Microsoft.AspNetCore.Mvc;
using CoursesRelaxBack.Data; // Importer AppDbContext
using CoursesRelaxBack.Models; // Importer le modèle User
using Microsoft.Data.SqlClient;

namespace CoursesRelaxBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey] // Vérification de la clé API activée
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            // Retourne l'utilisateur créé, y compris son ID, dans la réponse
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.Id)
            {
                return BadRequest("User data is invalid or ID does not match.");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Mise à jour des champs
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Address = updatedUser.Address;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;

            _context.SaveChanges();
            return Ok(existingUser);
        }
    }
}
