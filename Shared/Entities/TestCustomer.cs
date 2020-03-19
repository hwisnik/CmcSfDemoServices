using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Entities
{
	public class TestCustomer
	{
		public string Name { get; set; }
		public CustomerAddress Address { get; set; }
		public string Telephone { get; set; }

        public class CustomerAddress
        {
            public string Street { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
        }
    }
}
