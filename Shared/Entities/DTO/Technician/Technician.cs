using System;

namespace Shared.Entities.DTO.Technician
{
    public class Technician
    {
        public Guid TechnicianGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? StartAddressGuid { get; set; }
        public Guid? EndAddressGuid { get; set; }
    }
}
