using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Repositories
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAllAsync();
        public Task<Course> GetCourseAsync(int id);
        public Task PostAsync(Course course);
        public Task PutAsync(Course course);
        public Task DeleteAsync(int id);
        public Task<List<Student>> StudentsOfCourseAsync(int id);


    }
}
