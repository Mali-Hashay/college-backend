using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Repositories
{
    public interface IEnrollmentRepository
    {
        public Task<List<Enrollment>> GetAllAsync(); 
        public Task<Enrollment> GetEnrollmentAsync(int id, int? userId = null);
        public Task PostAsync(Enrollment enrollment);
        public Task PutAsync(Enrollment enrollment);
        public Task DeleteAsync(int id);

    }
}
