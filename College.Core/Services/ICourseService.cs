using College.Core.DTOs;
using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Services
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDTO>> GetList();
        public Task<CourseDTO?> GetCourseById(int id);
        public Task AddCourseAsync(Course course);
        public Task UpdateCourse(CourseDTO course);
        public Task DeleteCourse(int id);
        public Task<IEnumerable<StudentDTO>> GetStudentsOfCourse(int courseId);
    }
}
