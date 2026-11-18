using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace College.Core.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
    }
}
