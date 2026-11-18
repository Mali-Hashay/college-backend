using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using College.Core.DTOs;
using College.Core.Entities;
using College.Core.Repositories;
using College.Core.Services;
namespace College.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetList()
        {
            var studentEntities = await _studentRepository.GetAllAsync();
            var studentDtos = _mapper.Map<IEnumerable<StudentDTO>>(studentEntities);
            return studentDtos;
        }

        public async Task<StudentDTO?> GetStudent(int id, int? userId = null)
        {
            var student = await _studentRepository.GetByIdAsync(id, userId);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<int?> GetStudentIdByUserIdAsync(int userId)
        {
            var student = await _studentRepository.GetByUserIdAsync(userId);
            return student?.Id;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentRepository.PostAsync(student);
        }

        public async Task DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task UpdateStudent(StudentDTO studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(studentDto.Id);

            if (existingStudent != null)
            {
                _mapper.Map(studentDto, existingStudent);
                await _studentRepository.PutAsync(existingStudent);
            }
        }

        public async Task<IEnumerable<CourseDTO>> CoursesOfStudent(int userId)
        {
            var courses = await _studentRepository.GetCoursesOfStudentAsync(userId);
            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

    }
}
