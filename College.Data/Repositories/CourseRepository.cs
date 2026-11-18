using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College.Core.Entities;
using College.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace College.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseAsync(int id)
        {
           return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task PostAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existingCourse = await _context.Courses
                .FirstOrDefaultAsync(course => course.Id == id);
            if (existingCourse != null)
            {
                _context.Courses.Remove(existingCourse);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> StudentsOfCourseAsync(int id)
        {
            var studentIds = _context.Enrollments
                .Where(enrollment => enrollment.CourseId == id)
                .Select(enrollment => enrollment.StudentId);

            return await _context.Students
                .Where(student => studentIds.Contains(student.Id))
                .ToListAsync();
        }
    }
}
