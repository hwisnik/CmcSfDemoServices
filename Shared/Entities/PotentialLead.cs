using System;
using Shared.Entities.DTO.Customer;

namespace Shared.Entities
{
    public class PotentialLead
    {
        public Guid LeadId { get; set; }
        public double Distance { get; set; }
        public Address Address { get; set; }
    }
}
