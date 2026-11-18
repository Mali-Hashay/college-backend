using AutoMapper;
using College.Core.DTOs;
using College.Core.Entities;
using College.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace College.collegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }
        // GET: api/<CourseController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> Get()
        {
            return Ok(await _courseService.GetList());
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> Get(int id)
        {
            var courseDto = await _courseService.GetCourseById(id);
            if (courseDto == null)
                return NotFound();
            return Ok(courseDto);
        }

        // POST api/<CourseController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] CourseDTO courseDto)
        {
            if (courseDto == null)
                return BadRequest();
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                await _courseService.AddCourseAsync(course);
                return CreatedAtAction(nameof(Get), new { id = courseDto.Id }, courseDto);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("לא ניתן להוסיף את הקורס.");
            }
           
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] CourseDTO courseDto)
        {
            var existingCourse = await _courseService.GetCourseById(id);
            if (existingCourse == null)
                return NotFound();
            courseDto.Id = id;
            await _courseService.UpdateCourse(courseDto);
            return NoContent();
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var courseDto = await _courseService.GetCourseById(id);
            if (courseDto == null)
                return NotFound();
            await _courseService.DeleteCourse(id);
            return NoContent();
        }

        //GET api/<CourseController>/5/students
        [HttpGet("{id}/students")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsInCourse(int id)
        {
           return Ok(await _courseService.GetStudentsOfCourse(id));
        }

    }
}
