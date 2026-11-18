using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace College.Core.DTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }

        // StudentId is supplied by the server for student self-enrollment and by the client for administrators.
        [Range(1, int.MaxValue)]
        public int? StudentId { get; set; }

        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }
    }
}
