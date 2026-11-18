using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BeginMonth { get; set; }
        public string EndMonth { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
