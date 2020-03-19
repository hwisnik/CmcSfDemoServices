using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        //private const string AccountTable = " [Customer].[Account] ";
        private const string CustomerAccountTypeTable = " [Customer].[LkpAccountType] ";
        //private const string ProgramTable = " [Customer].[Program] ";
        //private const string SubProgramTable = " [Customer].[Subprogram] ";
        //Logging String Helpers
        private const string Repository = "CustomerRepository";

        public CustomerRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), "logging commandhandlerDecorator instance is null");
        }


        public async Task<Account> GetAccountFromGuid(Guid AccountGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetAccountFromGuid";

            //Query Used by Dapper
            var query =
                " select * from Customer.Account " +
                " where CustomerAccountId = @AccountGuid  ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter AccountGuid={AccountGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var account = await connection.QueryFirstAsync<Account>(query, new { AccountGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return account;
            }
        }


        //ToDo: Rewrite
        public async Task<Customer> GetDetailedCustomerFromGuid(Guid customerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetDetailedCustomerFromGuid";

            //Query Used by Dapper
            var query =
                " select [CustomerGuid] " +
                ",[CMCCustomerID] " +
                ",_Customer.[ProgramGuid] " +
                ",_Customer.[SubProgramGuid] " +
                ",[BillingAccountNumber] " +
                ",[CustomerAccountTypeGuid] " +
                ",ProgramName " +
                ",SubProgramName " +
                " from Customer.Customer as _Customer " +

                " join [Program].Program _Program " +
                " on _Customer.ProgramGuid = _Program.ProgramGuid " +

                " join [Program].Subprogram _Subprogram " +
                " on _Customer.SubProgramGuid = _Subprogram.SubProgramGuid " +

                " where _Customer.CustomerGuid = @CustomerGuid  ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter AccountGuid={customerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var customer = await connection.QueryFirstAsync<Customer>(query, new { CustomerGuid = customerGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return customer;
            }
        }

        public async Task<IEnumerable<LkpAccountType>> GetCustomerAccountTypes(LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "GetCustomerAccountTypes";

            //Query Used by Dapper
            var query =
                $"SELECT * from {CustomerAccountTypeTable} ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting, No Input Parameter." + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var accountType = await connection.QueryAsync<LkpAccountType>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return accountType;
            }
        }

        public async Task<LkpAccountType> GetCustomerAccountTypeFromName(string AccountTypeName, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "GetCustomerAccountTypeFromName";

            //Query Used by Dapper
            var query =
                $" SELECT * from {CustomerAccountTypeTable} " +
                $" where AccountTypeName = @AccountTypeName ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting, Input Parameter - AccountTypeName: {AccountTypeName}" + 
                                    Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var accountType = await connection.QueryFirstAsync<LkpAccountType>(query, new {AccountTypeName});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return accountType;
            }
        }
        public async Task<int> UpdateAccountSFAccountIDFromLeadGuid(Guid leadGuid, string sfAccountId, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateAccountSFAccountIDFromLeadGuid";

            //Query Used by Dapper
            var query =
                "update [Customer].[Account]  " +
                $"set SFAccountGuid = '{sfAccountId}' from  " +
                "	  [Customer].[Account] as _account  " +
                "	  join [Client].[Lead] as _lead  " +
                "	  on _lead.AccountGuid = _account.CustomerAccountId " +
                $"	  where _lead.Leadid = '{leadGuid}' ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters LeadGuid={leadGuid}; SFAccountID={sfAccountId}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        //TODO Missing Mappings....
        

        public async Task<int> UpdateCustomer(Customer customer, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateCustomer";

            //var test = CDCResponse._PayloadParent.BillAccountNumberC
            var BaseQuery =               
                " UPDATE [Customer].[Customer] " +
                " SET  " +
                "     [ProgramGuid] = @ProgramGuid " +
                "     ,[BillingAccountNumber] = @BillingAccountNumber" +
                "     ,[SubProgramGuid] = @SubProgramGuid " +
                " WHERE CustomerGuid = @CustomerGuid "; 

            //Use Changed Field AS Reference for insert. 
            
            //Log the input
            logCommand.LogMessage =
                $"{Repository}.{method_name} Starting input parameters customer = {JsonConvert.SerializeObject(customer)}" +
                Environment.NewLine +
                $"Query: {BaseQuery}" + Environment.NewLine;
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(BaseQuery, customer);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        public async Task<int> InsertCustomer(Customer customer, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertCustomer";

            //TODO THIS IS DEFAULT>>> 
            customer.CMCCustomerID = -1; 
            //var test = CDCResponse._PayloadParent.BillAccountNumberC
            
            var BaseQuery =
                " insert into [Customer].[Customer] " +
                "([CustomerGuid]" +
                ", [CMCCustomerID]" +
                ", [ProgramGuid] " +
                ", [SubProgramGuid]" +
                ", [BillingAccountNumber] " +
                ", [CustomerAccountTypeGuid] )" +
                "values ( " +
                " @CustomerGuid " +
                ",@CMCCustomerID" +
                ",@ProgramGuid" +
                ",@SubProgramGuid" +
                ",@BillingAccountNumber " +
                ",@CustomerAccountTypeGuid" +
                ")";
            
            //Log the input
            logCommand.LogMessage =
                $"{Repository}.{method_name} Starting input parameters customer = {JsonConvert.SerializeObject(customer)}" +
                Environment.NewLine +
                $"Query: {BaseQuery}" + Environment.NewLine;
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(BaseQuery, customer);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }
        Task<Customer> ICustomerRepository.GetAccountFromGuid(Guid customerGuid, LogCommand logCommand)
        {
            throw new NotImplementedException();
        }

    
    }
}
