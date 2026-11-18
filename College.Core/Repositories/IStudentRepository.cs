using College.Core.DTOs;
using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student> GetByIdAsync(int id, int? userId = null);
        public Task<Student> GetByUserIdAsync(int userId);
        public Task PostAsync(Student student);
        public Task PutAsync(Student student);
        public Task DeleteAsync(int id);
        public Task<List<Course>> GetCoursesOfStudentAsync(int id);
    }
}
