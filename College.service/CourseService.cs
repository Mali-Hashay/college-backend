using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using College.Core.DTOs;
using College.Core.Entities;
using College.Core.Repositories;
using College.Core.Services;
namespace College.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDTO>> GetList()
        {
            var courses = await _courseRepository.GetAllAsync();
            var courseDtos = _mapper.Map<IEnumerable<CourseDTO>>(courses);
            return courseDtos;
        }

        public async Task<CourseDTO?> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourseAsync(id);
            return _mapper.Map<CourseDTO>(course);
        }
        public async Task AddCourseAsync(Course course)
        {
            var courses = await _courseRepository.GetAllAsync();
            if (courses.Count > 40)
                throw new InvalidOperationException("There are too many courses for this semester");

            await _courseRepository.PostAsync(course);
        }
        public async Task UpdateCourse(CourseDTO courseDto)
        {
            var existingCourse = await _courseRepository.GetCourseAsync(courseDto.Id);

            if (existingCourse != null)
            {
                _mapper.Map(courseDto, existingCourse);
                await _courseRepository.PutAsync(existingCourse);
            }
        }
        public async Task DeleteCourse(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<StudentDTO>> GetStudentsOfCourse(int courseId)
        {
            var students = await _courseRepository.StudentsOfCourseAsync(courseId);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

    }
}
