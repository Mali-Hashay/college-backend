using AutoMapper;
using College.Core.DTOs;
using College.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Mapping
{
    public class MappingProfile: Profile
    {
       
        public MappingProfile() 
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
        }
       
    }
}
