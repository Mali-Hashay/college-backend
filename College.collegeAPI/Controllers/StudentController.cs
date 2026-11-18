using Microsoft.AspNetCore.Mvc;
using College.Core.Services;
using College.Core.Entities;
using College.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace College.collegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        // GET: api/<StudentController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> Get()
        {
            return Ok(await _studentService.GetList());
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<StudentDTO>> Get(int id)
        {
            int? userId = null;
            if (!User.IsInRole("Admin"))
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var parsedUserId))
                    return Unauthorized("משתמש לא מזוהה.");
                userId = parsedUserId;
            }

            var studentDto = await _studentService.GetStudent(id, userId);
            if (studentDto == null)
                return NotFound();
            return Ok(studentDto);
        }

        // POST api/<StudentController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] StudentDTO studentDto)
        {
            if (studentDto == null)
                return BadRequest();

            var student = _mapper.Map<Student>(studentDto);
            await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(Get), new { id = studentDto.Id }, studentDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] StudentDTO studentDto)
        {
            if (studentDto == null)
                return BadRequest();

            var existingStudent = await _studentService.GetStudent(id);
            if (existingStudent == null)
                return NotFound();

            studentDto.Id = id;

            await _studentService.UpdateStudent(studentDto);
            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var Student = await _studentService.GetStudent(id);
            if(Student == null)
                return NotFound();
            await _studentService.DeleteStudent(id);
            return NoContent();
        }

        [HttpGet("my-courses")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCoursesForStudent()
        {
            
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("משתמש לא מזוהה.");
            }

            return Ok(await _studentService.CoursesOfStudent(userId));
        }

    }
}
