using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class ContactRepository : IContactRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        //private IDbTransaction Transaction { get; set; }
        //IDbTransaction IContactRepository.Transaction { get => Transaction; set => Transaction = value; }



        // Repository table(s) for dapper
        private const string ContactRepositoryTable = " [Contact].[Contact] ";
        private const string ContactTypeRepositoryTable = " [Contact].[LkpContactType] ";
        private const string EmailRepositoryTable = " [Contact].[Email] ";
        private const string PhoneRepositoryTable = " [Contact].[Phone] ";
        private const string PrimaryContactTypeGuid = "BEF01936-5D80-471D-AF77-4CC2AE5161B4";

        //Logging String Helpers
        private const string Repository = "ContactRepository";

        public ContactRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), @"logging commandhandlerDecorator instance is null");
        }


        IDbConnection IContactRepository.GetConnection()
        {
            return _connectionFactory.GetConnection;
        }

        public async Task<Contact> GetPrimaryContactFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetDetailedContactsFromCustomerGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * " +
                " FROM [Contact].[Contact] as _Contact " +
                " join [Contact].[LkpContactType] _ContactType " +
                " on _Contact.ContactTypeGuid = _ContactType.ContactTypeGuid " +
                " where _Contact.CustomerGuid = @CustomerGuid and _Contact.ContactTypeGuid = 'BEF01936-5D80-471D-AF77-4CC2AE5161B4' ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={CustomerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ContactList = await connection.QueryFirstAsync<Contact>(query
                    , new
                    {
                        CustomerGuid
                    });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _ContactList;
            }
        }

        public async Task<Contact> GetContactFromSFContactRecordID(string SFContactRecordID, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetContactFromSFContactRecordID";

            //Query Used by Dapper
            var query =
                " SELECT  * " +
                $" FROM {ContactRepositoryTable} as _Contact " +
                $" join {ContactTypeRepositoryTable} as _ContactType " +
                " on _Contact.ContactTypeGuid = _ContactType.ContactTypeGuid " +
                " where  SFContactID = @SFContactRecordID ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter SFContactRecordID={SFContactRecordID}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ContactList = await connection.QueryFirstAsync<Contact>(query
                    , new
                    {
                        SFContactRecordID
                    });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _ContactList;
            }
        }

        public async Task<PhoneNumbers> GetPhoneFromContactGuid(Guid ContactGuid, LogCommand logCommand)
        {
            try
            {
                //Logging Helper for Method
                const string methodName = "GetPhoneFromContactId";

                //Query Used by Dapper
                var query =
                    " SELECT  * from " + PhoneRepositoryTable + " " +
                    " where ContactGuid = @ContactGuid";

                //Log the input
                logCommand.LogMessage =
                    $"{Repository}.{methodName} Starting input parameter ContactGuid={ContactGuid}" +
                    Environment.NewLine +
                    $"Query: {query}";
                _logHandler.HandleLog(logCommand);


                using (var connection = _connectionFactory.GetConnection)
                {
                    //Await the response
                    var _Phone = await connection.QueryFirstOrDefaultAsync<PhoneNumbers>(query, new { ContactGuid });

                    connection.Close();

                    //Log the output
                    logCommand.LogMessage = $"{Repository}.{methodName} completed";
                    _logHandler.HandleLog(logCommand);

                    return _Phone;
                }
            }
            catch
            {
                return new PhoneNumbers();
            }
        }

        public async Task<PhoneNumbers> GetPhoneFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetPhoneFromContactId";

            //Query Used by Dapper
            var query =
                " SELECT  * from " + PhoneRepositoryTable + " as _phone" +
                " join  Customer.Customer _customer " +
                " on Customer." +
                " where ContactGuid = @CustomerGuid";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={CustomerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Phone = await connection.QueryFirstOrDefaultAsync<PhoneNumbers>(query, new { CustomerGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Phone;
            }
        }


        public async Task<IEnumerable<string>> GetPhoneNumberByContactGuidAsync(Guid contactGuid, LogCommand logCommand, IDbTransaction trans)
        {
            //Logging Helper for Method
            const string methodName = "GetPhoneNumberByContactGuidAsync";
            var query = QueryResources.LeadQueries.ResourceManager.GetString("GetPhoneNumberByContactGuidQuery");
            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter: {contactGuid} " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            //NOTE: NO USING STATEMENT CALLING ROUTINE IN THE SERVICE LAYER NEEDS TO MANAGE THE CONNECTION !!!
            //Await the response
            var phone = await trans.Connection.QueryAsync<string>(query, new { @ContactGuid = contactGuid }, trans, null, CommandType.Text);

            // connection.Close();

            //Log the output
            logCommand.LogMessage = $"{Repository}.{methodName} completed";
            _logHandler.HandleLog(logCommand);

            return phone;

        }
        public async Task<Email> GetEmailFromContactGuid(Guid ContactGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetEmailFromContactId";

            //Query Used by Dapper
            var query =
                " SELECT  * from " + EmailRepositoryTable +
                " where ContactGuid = @ContactGuid";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter ContactGuid={ContactGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Email = await connection.QueryFirstOrDefaultAsync<Email>(query, new { ContactGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Email;
            }
        }

        public async Task<int> UpdatePrimaryContact(Contact contact, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdatePrimaryContact";

            var BaseQuery = $" Update {ContactRepositoryTable} set " +
                            "   [FirstName] = @FirstName " +
                            "   ,[MiddleName] = @MiddleName " +
                            "   ,[LastName] = @LastName " +
                            "   ,[FullName] = @FullName " +
                            "   ,[InformalName] = @InformalName " +
                            "   ,[Salutation] = @Salutation " +
                            "   ,[Suffix] = @Suffix " +
                            "   ,[Title] = @Title " +
                            "   ,[IsAnyContactAllowed] =  @IsAnyContactAllowed " +
                            "   ,[IsVoiceContactAllowed] = @IsVoiceContactAllowed " +
                            "   ,[IsSmsContactAllowed] = @IsSmsContactAllowed  " +
                            "   ,[IsAutoCallAllowed] = @IsAutoCallAllowed  " +
                            "   ,[IsEmailContactAllowed] = @IsEmailContactAllowed " +
                            $" where [CustomerGuid] = @CustomerGuid and  ContactTypeGuid = '{PrimaryContactTypeGuid}' ";

            // Update Primary Contact, ignoring IsPrimary and ContactType. 

            if (contact.CustomerGuid == Guid.Empty)
            {
                throw new Exception($"Missing Fields: contact.CustomerGuid: {contact.CustomerGuid} ");
            }

            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters contact = {JsonConvert.SerializeObject(contact)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, contact);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> InsertContact(Contact contact, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertContact";
            var BaseQuery = $" insert into {ContactRepositoryTable}  " +
                            " ([ContactGuid] " +
                            " ,[CustomerGuid] " +
                            " ,[SFContactId] " +
                            " ,[ContactTypeGuid] " +
                            " ,[FirstName] " +
                            " ,[MiddleName] " +
                            " ,[LastName] " +
                            " ,[FullName] " +
                            " ,[InformalName] " +
                            " ,[Salutation] " +
                            " ,[Suffix] " +
                            " ,[Title] " +
                            " ,[IsAnyContactAllowed] " +
                            " ,[IsVoiceContactAllowed] " +
                            " ,[IsSmsContactAllowed] " +
                            " ,[IsAutoCallAllowed] " +
                            " ,[IsEmailContactAllowed] " +
                            " ) " +
                            " VALUES " +
                            " (newid() " +
                            " ,@CustomerGuid " +
                            " ,@SFContactId " +
                            " ,@ContactTypeGuid " +
                            " ,@FirstName " +
                            " ,@MiddleName " +
                            " ,@LastName " +
                            " ,@FullName " +
                            " ,@InformalName " +
                            " ,@Salutation " +
                            " ,@Suffix " +
                            " ,@Title " +
                            " ,@IsAnyContactAllowed " +
                            " ,@IsVoiceContactAllowed " +
                            " ,@IsSmsContactAllowed " +
                            " ,@IsAutoCallAllowed " +
                            " ,@IsEmailContactAllowed " +
                            " ) ";

            if (contact.ContactTypeGuid == Guid.Empty
                || contact.CustomerGuid == Guid.Empty)
            {
                throw new Exception("Missing Fields");
            }


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters contact = {JsonConvert.SerializeObject(contact)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, contact);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> UpdateContact(Contact contact, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateContactFromLeadPayload";

            var BaseQuery = $" Update {ContactRepositoryTable} set " +
                            "   ,[ContactTypeGuid] = @ContactTypeGuid" +
                            "   ,[FirstName] = @FirstName " +
                            "   ,[MiddleName] = @MiddleName " +
                            "   ,[LastName] = @LastName " +
                            "   ,[FullName] = @Name " +
                            "   ,[InformalName] = @InformalName " +
                            "   ,[Salutation] = @Salutation " +
                            "   ,[Suffix] = @Suffix " +
                            "   ,[Title] = @Title " +
                            "   ,[IsAnyContactAllowed] =  @IsAnyContactAllowed " +
                            "   ,[IsVoiceContactAllowed] = @IsVoiceContactAllowed " +
                            "   ,[IsSmsContactAllowed] = @IsSmsContactAllowed  " +
                            "   ,[IsAutoCallAllowed] = @IsAutoCallAllowed  " +
                            "   ,[IsEmailContactAllowed] = @IsEmailContactAllowed " +
                            " where ([SFContactId] is not null and [SFContactId] = @SFContactId) or ContactGuid = @ContactGuid ";

            if (contact.ContactTypeGuid == Guid.Empty
                || string.IsNullOrEmpty(contact.SFContactId))
            {
                throw new Exception("Missing Fields");
            }


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters contact = {JsonConvert.SerializeObject(contact)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, contact);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> InsertPhone(PhoneNumbers phone, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertPhone";

            var BaseQuery = $" insert into {PhoneRepositoryTable} " +
                            $"([PhoneNumberGuid]" +
                            ", [ContactGuid] " +
                            ", [Phone] " +
                            ", [Mobile] " +
                            ", [Home_Phone__c] " +
                            ", [Work_Phone__c] " +
                            ", [Other_Phone__c] " +
                            ", [Additional_Phone_Data] )" +
                            " VALUES " +
                            $"( newid()" +
                            ", @ContactGuid " +
                            ", @Phone " +
                            ", @Mobile " +
                            ", @Home_Phone__c " +
                            ", @Work_Phone__c " +
                            ", @Other_Phone__c " +
                            ", @Additional_Phone_Data )";

            if (phone.ContactGuid == Guid.Empty)
            {
                throw new Exception("Missing Fields");
            }


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters phone = {JsonConvert.SerializeObject(phone)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, phone);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> UpdatePhone(PhoneNumbers phone, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdatePhone";

            var BaseQuery = $" UPDATE {PhoneRepositoryTable} SET " +
                            " [Phone]= @Phone " +
                            ", [MobilePhone]= @MobilePhone " +
                            ", [Home_Phone__c]= @Home_Phone__c " +
                            ", [Work_Phone__c]= @Work_Phone__c " +
                            ", [Other_Phone__c]= @Other_Phone__c " +
                            ", [Additional_Phone_Data]= @Additional_Phone_Data " +
                            "  WHERE ContactGuid = @ContactGuid ";

            if (phone.ContactGuid == Guid.Empty)
            {
                throw new Exception("Missing Fields");
            }

            
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters phone = {JsonConvert.SerializeObject(phone)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, phone);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> InsertEmail(Email email, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertEmail";

            var BaseQuery = $" insert into {EmailRepositoryTable} " +
                            "( [EmailGuid] " +
                            ", [ContactGuid] " +
                            ", [Email_Address] " +
                            ", [Alternate_Email_1__c] " +
                            ", [Alternate_Email_2__c] " +
                            ", [Alternate_Email_3__c] " +
                            ", [Additional_Email_Data] )" +
                            " VALUES " +
                            "( newid() " +
                            ", @ContactGuid " +
                            ", @Email_Address " +
                            ", @Alternate_Email_1__c " +
                            ", @Alternate_Email_2__c " +
                            ", @Alternate_Email_3__c " +
                            ", @Additional_Email_Data ) ";

            if (email.ContactGuid == Guid.Empty)
            {
                throw new Exception("Missing Fields");
            }


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters contact = {JsonConvert.SerializeObject(email)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, email);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }

        public async Task<int> UpdateEmail(Email email, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateEmail";

            var BaseQuery = $" update {EmailRepositoryTable} set " +
                            " [Email_Address] = @Email_Address" +
                            ", [Alternate_Email_1__c] = @Alternate_Email_1__c" +
                            ", [Alternate_Email_2__c] = @Alternate_Email_2__c" +
                            ", [Alternate_Email_3__c] = @Alternate_Email_3__c" +
                            ", [Additional_Email_Data] = @Additional_Email_Data" +
                            " where ContactGuid = @ContactGuid";

            if (email.ContactGuid == Guid.Empty)
            {
                throw new Exception("Missing Fields");
            }


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters email = {JsonConvert.SerializeObject(email)}" + Environment.NewLine +
                                    $"Query: {BaseQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _ReturnValue = await connection.ExecuteAsync(BaseQuery, email);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _ReturnValue;
            }
        }
    }
}