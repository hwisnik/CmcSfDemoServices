using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string AddressTable = " [Customer].[Address] ";
        //private const string AddressTypeTable = " [Customer].[LkpAddressType] ";
        //private const string AddressCountiesTable = " [Customer].[LkpCounties] ";

        //these values were taken from test, this could cause failure
        //private const string addressTypeGuidService = "32463191-A534-45B6-9EBA-06E53B1E9A7E";
        //private const string addressSource = "Program";


        //Logging String Helpers
        private const string Repository = "AddressRepository";

        public AddressRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }


        public async Task<IEnumerable<Address>> GetDetailedAddressFromCustomerGuid(Guid CustomerGuid,
            LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetDetailedAddressFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT * " + 
                $" from {AddressTable} as _Address" +

                " join  [Customer].LkpAddressType as _AddressType " +
                " on _AddressType.AddressTypeGuid = _Address.AddressTypeGuid " +

                " join  [Customer].LkpCounties as _Counties " +
                " on _Counties.CountyGuid = _Address.CountyGuid " +

                " join [Customer].LkpOwnership as _ownership" +
                " on _Address.OwnershipGuid = _ownership.OwnershipGuid " +
                
                " where _Address.CustomerGuid = @CustomerGuid ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={CustomerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var Address = await connection.QueryAsync<Address>(
                    query, 
                    new {CustomerGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return Address;
            }
        }

        public async Task<IEnumerable<Address>> GetAddressBySfIdAndAddressType (Address address, LogCommand logCommand) //(string sfAddressRecordId, Guid ownershipGuid, LogCommand logCommand)
        {

            //Logging Helper for Method
            const string methodName = "GetAddressBySfIdAndAddressType";

            var query =
                " SELECT [AddressGuid] " +
                "   ,[CustomerGuid]        " +
                "   ,[AddressTypeGuid]     " +
                "   ,[StreetAddress1]      " +
                "   ,[StreetAddress2]      " +
                "   ,[Country]             " +
                "   ,[City]                " +
                "   ,[State]               " +
                "   ,[Zip]                 " +
                "   ,[CountyGuid]          " +
                "   ,[Latitude]            " +
                "   ,[Longitude]           " +
                "   ,[LastServiceDate]     " +
                "   ,[SFAddressRecordID]   " +
                "   ,[AddressSource]       " +
                "   ,[OwnershipGuid]       " +
                $" FROM {AddressTable}      " +
                " WHERE SFAddressRecordID = @SFAddressRecordID " +
                " AND OwnerShipGuid = @OwnershipGuid";
            
            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter SfAddressRecordId={address.SFAddressRecordID}, ownershipGuid={address.OwnershipGuid} "  + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var response = await connection.QueryAsync<Address>(query, address);


                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return response;
            }
        }

        public async Task<LkpAddressType> GetLKPAddressTypeFromAddressTypeName(string AddressTypeName,
            LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLKPAddressTypeFromDescription";

            //Query Used by Dapper
            var query =
                " SELECT * from  [Customer].[LkpAddressType] " +
                " where [AddressTypeName] = @AddressTypeName ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter AddressTypeName={AddressTypeName}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var AddressType = await connection.QueryFirstAsync<LkpAddressType>(
                    query, 
                    new {AddressTypeName});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return AddressType;
            }
        }

        public async Task<LkpCounties> GetLkpCountyFromCountyDescription(string CountyDescription,
            LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLKPAddressTypeFromDescription";

            //Query Used by Dapper
            var query =
                " SELECT * from [Customer].[LkpCounties] " +
                " where [CountyDescription] = @CountyDescription ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CountyDescription={CountyDescription}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var AddressType = await connection.QueryFirstAsync<LkpCounties>(
                    query, 
                    new {CountyDescription});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return AddressType;
            }
        }

        public async Task<LkpOwnership> GetLkpOwnershipFromOwnershipType(string OwnershipType,
            LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpOwnershipFromOwnershipType";

            //Query Used by Dapper
            var query =
                " SELECT * from [Customer].[LkpOwnership] " +
                " where [OwnershipType] = 'Rented' ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter OwnershipType={OwnershipType}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var Ownership = await connection.QueryFirstAsync<LkpOwnership>(
                    query, 
                    new {OwnershipType});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return Ownership;
            }
        }

        public async Task<int> UpdateProgramServiceAddress(Address address, LogCommand logCommand)
        {
            //Logging Helper for Method
            // Probably only should be setting the sfAddressRecordID, source address should never be changed by Salesforce. 
            // Override would be inserted here. 
            var method_name = "UpdateProgramServiceAddress";

            var query = $" Update {AddressTable} set " +
                        $" SFAddressRecordID = @SFAddressRecordID" +
                        $" where CustomerGuid = @CustomerGuid and AddressTypeGuid = @AddressTypeGuid and AddressSource = @AddressSource";
                           

            if ( string.IsNullOrEmpty(address.SFAddressRecordID)
                || address.CustomerGuid == Guid.Empty
                ||address.AddressTypeGuid == Guid.Empty
                || address.CountyGuid == Guid.Empty) 
            {
                throw new Exception("Missing Fields"); 
            }
         
            //Build the Query
            //Log the input
            logCommand.LogMessage = $" {Repository}.{method_name} Starting input parameters: " +
                                    $" address = {JsonConvert.SerializeObject(address)} " + Environment.NewLine +
                                    $" Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(query, address);
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> UpdateAddress(Address address, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateAddress";

            var query = $" Update {AddressTable} set " +
                        " [AddressTypeGuid] = @AddressTypeGuid" +
                        ", [StreetAddress1] = @StreetAddress1" +
                        ", [StreetAddress2] = @StreetAddress2" +
                        ", [Country] = @Country" +
                        ", [City] = @City" +
                        ", [State] = @State" +
                        ", [Zip] = @Zip" +
                        ", [CountyGuid] = @CountyGuid" +
                        ", [Latitude] = @Latitude" +
                        ", [Longitude] = @Longitude" +
                        ", [LastServiceDate] = @LastServiceDate" +
                        ", [AddressSource] = @AddressSource" +
                        " where SFAddressRecordID = @SFAddressRecordID ";
                           

            if ( string.IsNullOrEmpty(address.SFAddressRecordID)
                ||address.AddressTypeGuid == Guid.Empty
                || address.CountyGuid == Guid.Empty) 
            {
                throw new Exception("Missing Fields"); 
            }
         
            //Build the Query
            //Log the input
            logCommand.LogMessage = $" {Repository}.{method_name} Starting input parameters: " +
                                    $" address = {JsonConvert.SerializeObject(address)} " + Environment.NewLine +
                                    $" Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(query, address);
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> InsertAddress(Address address, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertAddress";

            var query = $" insert into  {AddressTable} " +
                        " ([AddressGuid] " +
                        ", [CustomerGuid] " +
                        ", [SFAddressRecordID] " +
                        ", [AddressTypeGuid] " +
                        ", [StreetAddress1] " +
                        ", [StreetAddress2] " +
                        ", [Country] " +
                        ", [City] " +
                        ", [State] " +
                        ", [Zip] " +
                        ", [CountyGuid] " +
                        " ,[Latitude] " +
                        " ,[Longitude] " +
                        " ,[LastServiceDate]" +
                        " ,[AddressSource] " +
                        " ,[OwnershipGuid]) " +

                        " VALUES " +

                        " (NewID() " +
                        ", @CustomerGuid " +
                        ", @SFAddressRecordID " +
                        ", @AddressTypeGuid " +
                        ", @StreetAddress1 " +
                        ", @StreetAddress2 " +
                        ", @Country " +
                        ", @City " +
                        ", @State " +
                        ", @Zip " +
                        ", @CountyGuid " +
                        " ,@Latitude " +
                        " ,@Longitude " +
                        " ,@LastServiceDate" +
                        " ,@AddressSource" +
                        ", @OwnershipGuid ) ";
                           

            if ( string.IsNullOrEmpty(address.SFAddressRecordID)
                || address.CustomerGuid == Guid.Empty
                ||address.AddressTypeGuid == Guid.Empty
                || address.CountyGuid == Guid.Empty) 
            {
                throw new Exception("Missing Fields"); 
            }
         
            //Build the Query
            //Log the input
            logCommand.LogMessage = $" {Repository}.{method_name} Starting input parameters: " +
                                    $" address = {JsonConvert.SerializeObject(address)} " + Environment.NewLine +
                                    $" Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(query, address);
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }
 
    }
}
