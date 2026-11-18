using AutoMapper;
using College.Core.DTOs;
using College.Core.Entities;
using College.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace College.collegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public EnrollmentController(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _mapper = mapper;
        }
        // GET: api/<EnrollmentController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<EnrollmentDTO>>> Get()
        {
            return Ok(await _enrollmentService.GetList());
        }

        // GET api/<EnrollmentController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EnrollmentDTO>> Get(int id)
        {
            int? userId = null;
            if (!User.IsInRole("Admin"))
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var parsedUserId))
                    return Unauthorized("משתמש לא מזוהה.");
                userId = parsedUserId;
            }

            var enrollment = await _enrollmentService.GetEnrollmentById(id, userId);
            if (enrollment == null)
                return NotFound();
            return Ok(enrollment);
        }

        // POST api/<EnrollmentController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] EnrollmentDTO enrollmentDto)
        {
            if (enrollmentDto == null)
                return BadRequest();

            if (User.IsInRole("Admin"))
            {
                if (!enrollmentDto.StudentId.HasValue)
                    return BadRequest("מנהל חייב לציין StudentId.");
            }
            else
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                    return Unauthorized("משתמש לא מזוהה.");

                var studentId = await _studentService.GetStudentIdByUserIdAsync(userId);
                if (!studentId.HasValue)
                    return Unauthorized("לא נמצא סטודנט עבור המשתמש.");

                enrollmentDto.StudentId = studentId.Value;
            }

            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
                await _enrollmentService.AddEnrollmentAsync(enrollment);
                return CreatedAtAction(nameof(Get), new { id = enrollmentDto.Id }, enrollmentDto);
            }
            catch (InvalidOperationException)
            {
                return Conflict("לא ניתן ליצור את ההרשמה.");
            }
            
        }

        // PUT api/<EnrollmentController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] EnrollmentDTO enrollment)
        {
            var existingEnrollment = await _enrollmentService.GetEnrollmentById(id);
            if (existingEnrollment == null)
                return NotFound();
            await _enrollmentService.UpdateEnrollment(enrollment);
            return NoContent();
        }

        // DELETE api/<EnrollmentController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingEnrollment = await _enrollmentService.GetEnrollmentById(id);
            if (existingEnrollment == null)
                return NotFound();
            await _enrollmentService.DeleteEnrollment(id);
            return NoContent();
        }
    }
}
