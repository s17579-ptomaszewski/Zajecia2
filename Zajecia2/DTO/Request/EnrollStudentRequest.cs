using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zajecia2.DTO
{
    public class EnrollStudentRequest
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        public String BirthDate { get; set; }

        [Required]
        public string Studies { get; set; }

    }
}
