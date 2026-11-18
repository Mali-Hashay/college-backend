using College.Core.Entities;
using College.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace College.Data.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly DataContext _context;

        public EnrollmentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments.ToListAsync();
        }

        public async Task<Enrollment?> GetEnrollmentAsync(int id, int? userId = null)
        {
            var query = _context.Enrollments.AsQueryable();
            if (userId.HasValue)
                query = query.Where(enrollment => enrollment.Student.UserId == userId.Value);
            return await query.FirstOrDefaultAsync(enrollment => enrollment.Id == id);
        }

        public async Task PostAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(Enrollment enrollment)
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var existingEnrollment = await _context.Enrollments
                .FirstOrDefaultAsync(enrollment => enrollment.Id == id);
            if (existingEnrollment != null)
            {
                _context.Enrollments.Remove(existingEnrollment);
            }

            await _context.SaveChangesAsync();
        }
    }
}
