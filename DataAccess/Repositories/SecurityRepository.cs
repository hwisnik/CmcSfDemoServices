using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        private IDbTransaction Transaction { get; set; }
        IDbTransaction ISecurityRepository.Transaction { get => Transaction; set => Transaction = value; }

        public SecurityRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler));

        }

        IDbConnection ISecurityRepository.GetConnection()
        {
            return _connectionFactory.GetConnection;
        }


        public async Task<Guid> GetUserGuidByUserNameAsPartOfTransaction(string userName, IDbConnection connection, LogCommand logCommand)
        {
            string query = "Select UserGuid FROM [PCASecurity].[tblUsers] WHERE userName = @userName;";

            //Log the input
            logCommand.LogMessage = $"SecurityRepository.GetUserGuidByUserNameAsPartOfTransaction Starting input parameter userName={userName}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            //Await the response
            var userGuid = await connection.QueryAsync<Guid>(query, new { userName }, Transaction);

            //Log the output
            logCommand.LogMessage = "SecurityRepository.GetUserGuidByUserNameAsPartOfTransaction completed";
            _logHandler.HandleLog(logCommand);

            return userGuid.FirstOrDefault();
        }

        public async Task<Guid> GetUserSecurityTokenGuidByUserGuid(Guid userGuid, IDbConnection connection, LogCommand logCommand)
        {
            string query = "Select SecurityTokenGuid from [PCASecurity].[tblSecurityTokens] where userGuid = @userGuid order by UpdatedOn desc";

            //Log the input
            logCommand.LogMessage = $"SecurityRepository.GetUserSecurityTokenGuidByUserGuid Starting input parameter userGuid={userGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            //Await the response
            var retrievedSecurityToken = await connection.QueryAsync<Guid>(query, new { userGuid }, Transaction);

            //Log the output
            logCommand.LogMessage = "SecurityRepository.GetUserSecurityTokenGuidByUserGuid completed";
            _logHandler.HandleLog(logCommand);

            return retrievedSecurityToken.FirstOrDefault();
        }

        public async Task<int> UpdateSecurityTokenCreateAndUpdateDatesAsPartOfTransaction(Guid userGuid, IDbConnection connection, LogCommand logCommand)
        {
            string query = string.Format("Update [PCASecurity].[tblSecurityTokens] Set CreatedOn = '{0}', UpdatedOn = '{0}' where userGuid = @userGuid", DateTime.UtcNow);
            //Log the input
            logCommand.LogMessage = $"SecurityRepository.UpdateSecurityTokenCreateAndUpdateDatesAsPartOfTransaction Starting input parameter userGuid={userGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            //Await the response
            var affectedRows = await connection.ExecuteAsync(query, new { userGuid }, Transaction);

            //Log the output
            logCommand.LogMessage = "SecurityRepository.UpdateSecurityTokenCreateAndUpdateDatesAsPartOfTransaction completed";

            return affectedRows;
        }

        public async Task<int> InsertSecurityTokenAsPartOfTransaction(Guid correlationId, Guid userGuid, string userName, IDbConnection connection, LogCommand logCommand)
        {
            //Insert UserGuid and SecurityToken into tblSecurityTokens.  This will be checked and overwritten by spUser_SignIn_Preauthorized
            string query = "INSERT INTO [PCASecurity].[tblSecurityTokens](SecurityTokenGuid, UserGuid, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) " +
            "Values (@SecurityTokenGuid,@UserGuid, @CreatedOn,@CreatedBy,@UpdatedOn,@UpdatedBy)";

            var now = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("SecurityTokenGuid", correlationId.ToString().ToUpper());             //Needs to be uppercase for SP queries to find SecurityTokenGuid
            dParams.Add("UserGuid", userGuid);
            dParams.Add("CreatedOn", now);
            dParams.Add("Createdby", userName);
            dParams.Add("UpdatedOn", now);
            dParams.Add("UpdatedBy", userName);

            //Log the input
            logCommand.LogMessage = $"SecurityRepository.InsertSecurityTokenAsPartOfTransaction Starting input parameter correlationId{correlationId}, userGuid={userGuid}, userName={userName}" + Environment.NewLine +
                                    $"Query: {query}" + Environment.NewLine +
                                    $"Param: {DataAccessHelper.StringifyDapperDynamicParam(dParams)}";
            _logHandler.HandleLog(logCommand);



            //Await the response
            var result = await connection.ExecuteAsync(query, dParams, Transaction);

            //Log the output
            logCommand.LogMessage = "SecurityRepository.InsertSecurityTokenAsPartOfTransaction completed";
            _logHandler.HandleLog(logCommand);

            return result;
        }


 public async Task<Guid> InsertSecurityTokenAsync(string userName, Guid ermsSecurityToken, LogCommand logCommand)
        {
            string query = "Select UserGuid FROM [PCASecurity].[tblUsers] WHERE userName = @userName;";

            string query1 = "INSERT INTO [PCASecurity].[tblSecurityTokens](SecurityTokenGuid, UserGuid, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) " +
                                         "Values (@SecurityTokenGuid,@UserGuid, @CreatedOn,@CreatedBy,@UpdatedOn,@UpdatedBy)";
            var securityTokenGuid = ermsSecurityToken.ToString();
            string createdOn = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            string createdBy = userName;
            string updatedOn = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            string updatedBy = userName;

            //Log the input
            logCommand.LogMessage = $"SecurityRepository.InsertSecurityTokenAsync Starting input parameter userName={userName}, ermsSecurityToken={ermsSecurityToken}" + Environment.NewLine +
                                    $"SecurityTokenGuid: {securityTokenGuid}, CreatedOn: {createdOn}, CreatedBy: {createdBy}, UpdatedOn: {updatedOn}, UpdatedBy: {updatedBy}" +
                                    $"Query: {query}" + Environment.NewLine +
                                    $"Insert: {query1}";

            _logHandler.HandleLog(logCommand);

            using (var sqlConnection = _connectionFactory.GetSqlConnection)
            {


                var userGuid = sqlConnection.QuerySingle<Guid>(query, new { userName });

                //Await the response
                var result = await sqlConnection.ExecuteAsync(query1, new
                {
                    securityTokenGuid,
                    userGuid,
                    createdOn,
                    createdBy,
                    updatedOn,
                    updatedBy
                });
                sqlConnection.Close();

                //Log the output
                logCommand.LogMessage = "SecurityRepository.InsertSecurityTokenAsync completed" +
                                        $"result: {result}";
                _logHandler.HandleLog(logCommand);

                return userGuid;
            }
        }




        //Found an SP to use instead of using my own query
        //public async Task<IEnumerable<string>> GetRolePrivilegeNames(Guid userGuid, int orgId,LogCommand logCommand)
        //{
        //    using (var connection = _connectionFactory.GetConnection)
        //    {
        //        string query = "SELECT distinct RolePrivs.PrivilegeName " +
        //                "from [PCASecurity].[tblRolesPrivilegesV2] RolePrivs " +
        //                "inner join[PCASecurity].[tblUsersOrgsRoles] UserRoles on " +
        //                "UserRoles.RoleGuid = RolePrivs.RoleGuid " +
        //                "where UserRoles.UserGuid = @UserGuid " +
        //                "and UserRoles.OrgId = @OrgId " +
        //                "and RolePrivs.IsVisibleOrViewable = 1";

        //        var privs = await connection.QueryAsync<string>(query, new { UserGuid = userGuid, OrgId = orgId });
        //        return privs;
        //    }

        //}

  
    }
}

