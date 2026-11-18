using College.Core.DTOs;
using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Services
{
    public interface IStudentService
    {
        public Task<IEnumerable<StudentDTO>> GetList();
        public Task<StudentDTO?> GetStudent(int id, int? userId = null);
        public Task<int?> GetStudentIdByUserIdAsync(int userId);
        public Task AddStudentAsync(Student student);
        public Task UpdateStudent(StudentDTO student);
        public Task DeleteStudent(int id);
        public Task<IEnumerable<CourseDTO>> CoursesOfStudent(int id);
    }
}
