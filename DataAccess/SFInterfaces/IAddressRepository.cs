﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities.DTO.Customer;
using Shared.Entities.InputObjects.Address;
using Shared.Entities._SearchCriteria.Address;
using Shared.EventPayloads;

namespace DataAccess.SFInterfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetDetailedAddressFromCustomerGuid(Guid customerGuid, LogCommand logCommand);

        Task<LkpAddressType> GetLKPAddressTypeFromAddressTypeName(string AddressTypeName,
            LogCommand logCommand);
        
        Task<LkpCounties> GetLkpCountyFromCountyDescription(string CountyDescription,
            LogCommand logCommand);

        Task<LkpOwnership> GetLkpOwnershipFromOwnershipType(
            string OwnershipType,
            LogCommand logCommand);

        Task<int> UpdateAddress(Address address, LogCommand logCommand);
        Task<int> UpdateProgramServiceAddress(Address address, LogCommand logCommand);
        Task<int> InsertAddress(Address address, LogCommand logCommand);
        Task<IEnumerable<Address>> GetAddressBySfIdAndAddressType(Address address, LogCommand logCommand);



    }
}
