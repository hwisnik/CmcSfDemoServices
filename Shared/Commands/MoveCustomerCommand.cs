using Shared.Entities;

namespace Shared.Commands
{
    public class MoveCustomerCommand
    {
        public int CustomerId { get; set; }

        public TestCustomer.CustomerAddress NewAddress { get; set; }
    }
}