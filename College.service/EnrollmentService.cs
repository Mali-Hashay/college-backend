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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentDTO>> GetList()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            var enrollmentDtos = _mapper.Map<IEnumerable<EnrollmentDTO>>(enrollments);
            return enrollmentDtos;
        }

        public async Task<EnrollmentDTO?> GetEnrollmentById(int id, int? userId = null)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentAsync(id, userId);
            return _mapper.Map<EnrollmentDTO>(enrollment);
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            if (enrollments.Any(e => e.StudentId == enrollment.StudentId
                && e.CourseId == enrollment.CourseId))
            {
                throw new InvalidOperationException("Student already enrolled to this course");
            }

            await _enrollmentRepository.PostAsync(enrollment);
        }

        public async Task DeleteEnrollment(int id)
        {
            await _enrollmentRepository.DeleteAsync(id);
        }

        public async Task UpdateEnrollment(EnrollmentDTO enrollmentDto)
        {
            var existingEnrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentDto.Id);

            if (existingEnrollment != null)
            {
                _mapper.Map(enrollmentDto, existingEnrollment);
                await _enrollmentRepository.PutAsync(existingEnrollment);
            }
        }
    }
}
