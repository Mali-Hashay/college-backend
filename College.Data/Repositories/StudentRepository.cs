using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College.Core.Entities;
using College.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace College.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, int? userId = null)
        {
            var query = _context.Students.AsQueryable();
            if (userId.HasValue)
                query = query.Where(student => student.UserId == userId.Value);
            return await query.FirstOrDefaultAsync(student => student.Id == id);
        }

        public async Task<Student> GetByUserIdAsync(int userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(student => student.UserId == userId);
        }

        public async Task<List<Course>> GetCoursesOfStudentAsync(int userId)
        {
            var courseIds = _context.Enrollments
                .Where(enrollment => enrollment.Student.UserId == userId)
                .Select(enrollment => enrollment.CourseId);

            return await _context.Courses
                .Where(course => courseIds.Contains(course.Id))
                .ToListAsync();
        }

        public async Task PostAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(Student student)
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(student => student.Id == id);
            if (existingStudent != null)
            {
                _context.Students.Remove(existingStudent);
            }

            await _context.SaveChangesAsync();
        }

    }
}
