using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateMania.Models
{
    public class ClassModel
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string? Region { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? SchoolName { get; set; }
        public int Grade { get; set; }
    }
}
