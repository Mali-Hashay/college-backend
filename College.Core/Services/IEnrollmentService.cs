using College.Core.DTOs;
using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Services
{
    public interface IEnrollmentService
    {
        public Task<IEnumerable<EnrollmentDTO>> GetList();
        public Task<EnrollmentDTO?> GetEnrollmentById(int id, int? userId = null);
        public Task AddEnrollmentAsync(Enrollment enrollment);
        public Task UpdateEnrollment(EnrollmentDTO enrollment);
        public Task DeleteEnrollment(int id);
    }
}
