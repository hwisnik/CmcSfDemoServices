using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public class Technician
    {
        [Required]
        public Guid TechnicianGuid { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public Guid StartAddressGuid { get; set; }

        public Guid EndAddressGuid { get; set; }

    }
}
