using BusinessLogic.Services;
using CmcSfRestServices.Controllers;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Caching;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Unit_Tests.Helpers
{

    [ExcludeFromCodeCoverage]
    public static class UnitTestHelpers
    {
        private static string _bearerToken;
        private static string _tokenExpiration;

        //public static string BearerToken { get => _bearerToken; set => _bearerToken = value; }
        //public static string TokenExpiration { get => _tokenExpiration; set => _tokenExpiration = value; }

        public static void WriteTestResultsToTestContext(TestContext testContext)
        {
            foreach (string key in testContext.Properties.Keys)
            {
                if (!key.StartsWith("TestRunDirectory") & !key.StartsWith("DeploymentDirectory") & !key.StartsWith("ResultsDirectory") & !key.StartsWith("TestRunResultsDirectory")
                    & !key.StartsWith("TestResultsDirectory") & !key.StartsWith("TestDir") & !key.StartsWith("TestDeploymentDir") & !key.StartsWith("TestLogsDir"))
                {
                    if (key.StartsWith("TestName"))
                    {
                        testContext.WriteLine("{0}TestName: {1}", Environment.NewLine, testContext.TestName);
                        testContext.WriteLine("TestStatus: {0} {1}", testContext.CurrentTestOutcome, Environment.NewLine);
                    }
                    else
                    {
                        testContext.WriteLine("{0}:  {1}", key, testContext.Properties[key]);
                    }
                }

            }

            //See https://stackoverflow.com/questions/23661372/log4net-logicalthreadcontext-and-unit-test-cases
            //Error: Type is not resolved for member 'log4net.Util.PropertiesDictionary,log4net, Version=1.2.13.0, Culture=neutral, PublicKeyToken=669e0ddf0bb1aa2a'.
            //Resolves issue of unit tests failing to run due to using logicalThreadContext in log4net

            CallContext.FreeNamedDataSlot("log4net.Util.LogicalThreadContextProperties");
        }

        public static void AddTestContextPropertiesFromHttpResultsList<TEntityList>(List<TEntityList> expectedEntityList, OkNegotiatedContentResult<GenericServiceResponse> contentResult,
                                    List<TEntityList> actualEntityResponseList, TestContext testContext)
        {
            testContext.Properties.Add("ServiceResponse.Success", $"Expected to be: true, Actual Value = {contentResult.Content.Success.ToString()}");
            var actualRespEntity = actualEntityResponseList == null ? "null" : JsonConvert.SerializeObject(actualEntityResponseList);
            testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                JsonConvert.SerializeObject(expectedEntityList), Environment.NewLine, actualRespEntity));
            testContext.Properties.Add(Environment.NewLine + "Exception", string.Format("{0}Expected to be: null{0} Actual Value = {1}", Environment.NewLine,
                contentResult.Content.OperationException));
        }


        public static void AddTestContextPropertiesFromOkHttpResult<TEntity>(TEntity expectedEntity, OkNegotiatedContentResult<GenericServiceResponse> contentResult,
                            TEntity entityResponse, TestContext testContext)
        {
            testContext.Properties.Add("ServiceResponse.Success", $"Expected to be: true, Actual Value = {contentResult.Content.Success.ToString()}");
            var respEntity = entityResponse == null ? "null" : JsonConvert.SerializeObject(entityResponse);
            testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                JsonConvert.SerializeObject(expectedEntity), Environment.NewLine, respEntity));
            testContext.Properties.Add(Environment.NewLine + "Exception", string.Format("{0}Expected to be: null{0} Actual Value = {1}", Environment.NewLine,
                contentResult.Content.OperationException));
        }

        public static void AddTestContextPropertiesFromHttpActionResult<TEntity>(TEntity expectedEntity, HttpActionResult actionResult,
                    TEntity actualEntityResponse, TestContext testContext, string expectedException)
        {

            testContext.Properties.Add("ServiceResponse.Success", $"Expected to be:{HttpStatusCode.InternalServerError}, Actual Value = {actionResult.StatusCode.ToString()}");
            var respEntity = actualEntityResponse == null ? "null" : JsonConvert.SerializeObject(actualEntityResponse);
            testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                JsonConvert.SerializeObject(expectedEntity), Environment.NewLine, respEntity));
            testContext.Properties.Add(Environment.NewLine + "Exception", string.Format("{0}Expected to be: {1}{0}Actual Value = {2}", Environment.NewLine,
                expectedException, actionResult.CommandResult.OperationException));
        }


        public static void AddTestContextPropertiesFromHttpActionResult<TEntity>(TEntity expectedEntity, HttpActionResult actionResult,
            TEntity actualEntityResponse, TestContext testContext, HttpStatusCode httpStatusCode, string expectedException)
        {

            testContext.Properties.Add("ServiceResponse.Success", $"HttpStatusCode Expected to be:{httpStatusCode}, Actual Value = {actionResult.StatusCode.ToString()}");
            var respEntity = actualEntityResponse == null ? "null" : JsonConvert.SerializeObject(actualEntityResponse);
            testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                JsonConvert.SerializeObject(expectedEntity), Environment.NewLine, respEntity));
            testContext.Properties.Add(Environment.NewLine + "Exception", string.Format("{0}Expected to be: {1}{0}Actual Value = {2}", Environment.NewLine,
                expectedException, actionResult.CommandResult.OperationException));
        }


        public static void AddTestContextFromServiceResponse<TEntity>(TEntity expectedEntity, GenericServiceResponse actualServiceResponse, TestContext testContext,
            bool expectedSuccess = true, string expectedException = "no Exception")
        {
            testContext.Properties.Add("ServiceResponse.Success", $"Expected to be: {expectedSuccess}, Actual Value = {actualServiceResponse.Success.ToString()}");

            var respEntity = actualServiceResponse.Entity == null ? "null" : JsonConvert.SerializeObject(actualServiceResponse.Entity);
            if (expectedSuccess  && expectedException == "no Exception")
            {
                testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                    JsonConvert.SerializeObject(expectedEntity), Environment.NewLine, respEntity));
            }
            else
            {
                testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                    "null", Environment.NewLine, respEntity));
            }

            testContext.Properties.Add(Environment.NewLine + "Exception", string.Format("{0}Expected to be: {1}{0}Actual Value = {2}", Environment.NewLine, expectedException,
                actualServiceResponse.OperationException));
        }

        // ReSharper disable once UnusedMember.Global
        public static void AddTestContextFromControllerResponseList<TEntityList>(List<TEntityList> expectedEntityList, CommandResult commandResult, double elapsedTimeInSeconds, TestContext testContext,
                        bool expectedSuccess = true, string expectedException = "no Exception")
        {
            testContext.Properties.Add("Elapsed Time", elapsedTimeInSeconds);
            testContext.Properties.Add("ServiceResponse.Success", $"Expected to be: {expectedSuccess}, Actual Value = {commandResult.Success.ToString()}");
            var respEntity = commandResult.Entity == null ? "null" : JsonConvert.SerializeObject(commandResult.Entity);
            testContext.Properties.Add(Environment.NewLine + "Results", string.Format("{1}Expected Results: {1}{0}{1}{1}Actual Value:{1}{2}",
                JsonConvert.SerializeObject(expectedEntityList), Environment.NewLine, respEntity));

            Exception operationException = null;
            if (commandResult.OperationException != null)
            {
                operationException = (Exception)((JValue)commandResult.OperationException).Value;
            }
            var exceptionMessage = operationException == null ? "null" : operationException.Message;
            testContext.Properties.Add(Environment.NewLine + "Exception", $"{Environment.NewLine}Expected to be: {expectedException}, Actual Value = {exceptionMessage}");
        }

        public static void AddTestContextFromCommandResult(TestContext testContext, Stopwatch stopWatch, CommandResult resp, JObject result)
        {
            testContext.Properties.Add("Elapsed Time", stopWatch.Elapsed.TotalSeconds);
            testContext.Properties.Add("Exception", resp.OperationException);
            testContext.Properties.Add("Results", result.SelectToken("Entity"));
            testContext.Properties.Add("Success", resp.Success);
        }

        public static void InitializeTestData()
        {
            SetBearerTokenAndDuration();
        }

        public static HttpResponseMessage SetBearerTokenAndDuration()
        {
            if (TokenCacher.GetToken("testadmin") != null) return new HttpResponseMessage(HttpStatusCode.OK);

            using (var client = new HttpClient())
            {
                var login = new Dictionary<string, string>
                   {
                       {"grant_type", "password"},
                       {"username", "testadmin"},
                       {"password", "gnpla55!"},
                   };

                //Act
                var response = client.PostAsync(new Uri(ConfigurationManager.AppSettings["IntegrationTestBaseUrl"] + "token"), new FormUrlEncodedContent(login)).Result;
                if (response.ReasonPhrase != HttpStatusCode.OK.ToString() || !response.IsSuccessStatusCode) return response;

                //Guid corrId;
                var corrId = response.Headers.GetValues("http-correlation-id").FirstOrDefault();
                if (string.IsNullOrEmpty(corrId))
                {
                    var failedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        ReasonPhrase = "Missing CorrelationId",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                    return failedResponse;

                }
                var correlationId = Guid.Parse(corrId);
                var tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                if (tokenDetails != null && tokenDetails.Any())
                {
                    //var tokenNo = tokenDetails.FirstOrDefault().Value;
                    tokenDetails.TryGetValue("access_token", out _bearerToken);
                    tokenDetails.TryGetValue("expires_in", out _tokenExpiration);
                    tokenDetails.Add("ermsSecurityToken", correlationId.ToString());
                }

                var expirationDate = DateTimeOffset.UtcNow.AddSeconds(Convert.ToDouble(_tokenExpiration));

                TokenCacher.Delete("testadmin");
                TokenCacher.Add("testadmin", _bearerToken, expirationDate);
                TokenCacher.Add("CorrelationId", correlationId, expirationDate);


                return response;
            }

        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            dataTable.TableName = type.Name;
            return dataTable;
        }


        public static Guid GetOpenSessionGuid()
        {
            var connFactory = new UnitTestConnectionFactory();
            using (var sqlConnection = connFactory.GetConnection)
            {

                const string userName = "testadmin";
                string sessionGuidQuery = "Select SessionGuid FROM [PCASecurity].[tblSessions] WHERE userName = @userName and SignOutDateTime is null";
                var sessionGuid = sqlConnection.QueryFirstOrDefault<Guid>(sessionGuidQuery, new { userName });
                return sessionGuid;
            }
        }

        public static void PopulateEntityProperties(object tEntity)
        {
            var assertList = new List<string>();
            var entityType = tEntity.GetType();
            var properties = entityType.GetProperties();
            const string path = @"c:\temp\UnitTestClasses\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var assertStatementFileName = path + entityType + ".AssertStatements.txt";
            var propFileName = path + entityType + ".Properties.txt";

            File.Delete(propFileName);

            foreach (var property in properties)
            {
                var targetPropertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                if (targetPropertyType.FullName == null) continue;
                var propTypeName = Type.GetType(targetPropertyType.FullName);
                if (propTypeName == null) continue;
                //var quoteCloseParen = $"\")";
                const string doubleQuoteCloseParen = @""")";
                const string doubleQuote = @"""";
                switch (propTypeName.FullName)
                {
                    case "System.Boolean":
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, true, null);
                        var propBoolString = property.Name + " = true,";

                        File.AppendAllText(propFileName, propBoolString + Environment.NewLine);
                        assertList.Add($"Assert.IsTrue((bool)receivedEntity.{property.Name});");
                        break;
                    case "System.DateTime":
                        var now = DateTime.Now;
                        var formattedDateString = "Convert.ToDateTime(" + doubleQuote + now + doubleQuoteCloseParen;
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, now, null);
                        //var propDateString = property.Name + " = " + formattedDateString + ",";
                        var propDateString = $"{property.Name} = {formattedDateString},";

                        File.AppendAllText(propFileName, propDateString + Environment.NewLine);
                        assertList.Add($"Assert.AreEqual({formattedDateString},receivedEntity.{property.Name});");
                        break;
                    case "System.Decimal":
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, 1.23M, null);
                        var propDecString = property.Name + " = 1.23M,";

                        File.AppendAllText(propFileName, propDecString + Environment.NewLine);
                        assertList.Add($"Assert.AreEqual(1.23M,receivedEntity.{property.Name});");
                        break;
                    case "System.Guid":
                        var testGuid = Guid.NewGuid();
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, testGuid, null);
                        var formattedGuidString = @"Guid.Parse(" + doubleQuote + testGuid + doubleQuoteCloseParen;
                        var propGuidString = $"{property.Name} = {formattedGuidString},";

                        File.AppendAllText(propFileName, propGuidString + Environment.NewLine);
                        assertList.Add($"Assert.AreEqual({formattedGuidString},receivedEntity.{property.Name});");
                        break;
                    case "System.Int16":
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, Convert.ToInt16(123), null);
                        var propInt16String = property.Name + " = 1,";

                        File.AppendAllText(propFileName, propInt16String + Environment.NewLine);
                        assertList.Add($"Assert.AreEqual({1},receivedEntity.{property.Name});");
                        break;
                    case "System.Int32":
                    case "System.Int64":
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, 123, null);
                        var propIntString = property.Name + " = 123,";

                        File.AppendAllText(propFileName, propIntString + Environment.NewLine);
                        assertList.Add($"Assert.AreEqual({123},receivedEntity.{property.Name});");
                        break;
                    case "System.String":
                        entityType.GetProperty(property.Name)?.SetValue(tEntity, property.Name, null);
                        var formattedStringString = doubleQuote + property.Name + doubleQuote;
                        var propStringString = property.Name + " = " + formattedStringString + ",";

                        File.AppendAllText(propFileName, propStringString + Environment.NewLine);
                        assertList.Add($"Assert.AreEqual({formattedStringString},receivedEntity.{property.Name});");
                        break;
                }
            }

            File.WriteAllLines(assertStatementFileName, assertList);

        }

        public static GenericPrincipal CreateGenericPrincipalForTesting()
        {
            var genericIdentity = new GenericIdentity("testIdentity");
            genericIdentity.AddClaim(new Claim("ERMSUSERGUID", "4DA4C148-288E-4199-8636-45A96D9A8342"));  //UserGuid for TestAdmin
            genericIdentity.AddClaim(new Claim("ERMSDEFAULTORGID", "1"));

            string[] roles = { "ErmsServicesUsers", "ErmsExternalWorkflowUsers" };
            var genericPrincipal = new GenericPrincipal(genericIdentity, roles);
            return genericPrincipal;
        }

        public static void UpdateAppConfigAppSettingsValue(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }

    [ExcludeFromCodeCoverage]
    public class RequestHandler
    {
        public static async Task<CommandResult> PostHttpRequestAsync(string uriSuffix, object objectToSerialize)
        {
            var cmdResult = new CommandResult();
            var bearerToken = TokenCacher.GetToken("testadmin");
            if (bearerToken == null) Assert.Fail("Token is not cached");

            string authToken = $"bearer {bearerToken}";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IntegrationTestBaseUrl"]);

                //HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), ConfigurationManager.AppSettings["IntegrationTestBaseUrl"] + uriSuffix);
                //    requestMessage.Headers.Accept.Clear();
                //    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    requestMessage.Headers.TryAddWithoutValidation("Authorization", authToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                //var response = client.SendAsync(requestMessage<DataCommand> dataCommand).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                var apiResponse = await client.PostAsJsonAsync(uriSuffix, objectToSerialize).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                var apiResponseResult = apiResponse.Content.ReadAsStringAsync().Result;
                try
                {
                    var jEntity = JObject.Parse(apiResponseResult);
                    cmdResult.OperationException = jEntity.SelectToken("OperationException");
                    cmdResult.Entity = jEntity;
                    cmdResult.Success = Convert.ToBoolean(jEntity.SelectToken("Success"));
                    return cmdResult;

                }
                catch (Exception ex)
                {
                    cmdResult.OperationException = ex;
                    cmdResult.Success = false;
                    return cmdResult;
                }
            }
        }

        public static async Task<CommandResult> PostHttpRequestKeyValuePairsAsync(string uri, Dictionary<string, string> keyValuePairs = null)
        {
            var cmdResult = new CommandResult();
            var bearerToken = TokenCacher.GetToken("testadmin");
            if (bearerToken == null) Assert.Fail("Token is not cached");

            var authToken = $"bearer {bearerToken}";
            using (var client = new HttpClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), ConfigurationManager.AppSettings["IntegrationTestBaseUrl"] + uri);
                requestMessage.Headers.Accept.Clear();
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                requestMessage.Headers.TryAddWithoutValidation("Authorization", authToken);

                if (keyValuePairs != null)
                {
                    requestMessage.Content = new FormUrlEncodedContent(keyValuePairs);
                }


                using (HttpResponseMessage apiResponse = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead))
                {

                    //var apiResponseResult = apiResponse.Content.ReadAsAsync<CommandResult>();
                    //return apiResponseResult.Result;
                    var apiResponseResult = apiResponse.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var jEntity = JObject.Parse(apiResponseResult);
                        cmdResult.OperationException = jEntity.SelectToken("OperationException");
                        cmdResult.Entity = jEntity;
                        cmdResult.Success = Convert.ToBoolean(jEntity.SelectToken("Success"));
                        return cmdResult;

                    }
                    catch (Exception ex)
                    {
                        cmdResult.OperationException = ex;
                        cmdResult.Success = false;
                        return cmdResult;
                    }
                }
            }
        }

        public static async Task<CommandResult> PostDataCommandHttpRequestAsync(string uriSuffix, List<DataCommand> dataCommands)
        {
            var cmdResult = new CommandResult();
            var bearerToken = TokenCacher.GetToken("testadmin");
            if (bearerToken == null) Assert.Fail("Token is not cached");

            var authToken = $"bearer {bearerToken}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IntegrationTestBaseUrl"]);

                //HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), ConfigurationManager.AppSettings["IntegrationTestBaseUrl"] + uriSuffix);
                //    requestMessage.Headers.Accept.Clear();
                //    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    requestMessage.Headers.TryAddWithoutValidation("Authorization", authToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                //var response = client.SendAsync(requestMessage<DataCommand> dataCommand).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                var apiResponse = await client.PostAsJsonAsync(uriSuffix, dataCommands).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                string apiResponseResult = apiResponse.Content.ReadAsStringAsync().Result;
                try
                {
                    JObject jEntity = JObject.Parse(apiResponseResult);
                    cmdResult.OperationException = jEntity.SelectToken("OperationException");
                    cmdResult.Entity = jEntity;
                    cmdResult.Success = Convert.ToBoolean(jEntity.SelectToken("Success"));
                    return cmdResult;

                }
                catch (Exception ex)
                {
                    cmdResult.OperationException = ex;
                    cmdResult.Success = false;
                    return cmdResult;
                }
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static T MakeRequest<T>(string httpMethod, string route, Dictionary<string, string> postParams = null)
        {
            using (var client = new HttpClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), $"{ConfigurationManager.AppSettings["IntegrationTestBaseUrl"]}/{route}");

                if (postParams != null)
                    requestMessage.Content = new FormUrlEncodedContent(postParams);   // This is where your content gets added to the request body


                var response = client.SendAsync(requestMessage).Result;

                var apiResponse = response.Content.ReadAsStringAsync().Result;
                try
                {
                    // Attempt to deserialize the response to the desired type, otherwise throw an exception with the response from the api.
                    if (apiResponse != "")
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}", ex);
                }
            }
        }

        public static async Task<CommandResult> GetEntity(string query, string uri)
        {
            var cmdResult = new CommandResult();
            var bearerToken = TokenCacher.GetToken("testadmin");
            if (bearerToken == null) Assert.Fail("Token is not cached");
            string authToken = $"bearer {bearerToken}";

            using (HttpClient client = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(uri)
                {
                    Query = query
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);

                using (var response = await client.GetAsync(builder.Uri))
                {
                    if (response.ReasonPhrase == HttpStatusCode.OK.ToString() && response.IsSuccessStatusCode)
                    {

                        var asyncReadresult = await response.Content.ReadAsStringAsync();
                        try
                        {
                            JObject jEntity = JObject.Parse(asyncReadresult);

                            cmdResult.OperationException = jEntity.SelectToken("OperationException");
                            cmdResult.Entity = jEntity;
                            cmdResult.Success = Convert.ToBoolean(jEntity.SelectToken("Success"));
                            return cmdResult;
                        }
                        catch (Exception ex)
                        {

                            cmdResult.OperationException = ex;
                            cmdResult.Success = false;
                            cmdResult.Entity = asyncReadresult;
                            return cmdResult;
                        }

                    }
                    else
                    {
                        cmdResult.Entity = response.ReasonPhrase;
                        cmdResult.Success = false;
                        return cmdResult;
                    }
                }
            }
        }

    }


    [ExcludeFromCodeCoverage]
    public class CommandResult
    {
        public object Entity { get; set; }
        public bool Success { get; set; }
        public object OperationException { get; set; }
    }


    [ExcludeFromCodeCoverage]
    public static class TokenCacher
    {

        public static object GetToken(string tokenKey)
        {
            var memoryCache = MemoryCache.Default;
            return memoryCache.Get(tokenKey);
        }
        public static bool Add(string key, object token, DateTimeOffset absExpiration)
        {
            var memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key)) { memoryCache.Remove(key); }
            return memoryCache.Add(key, token, absExpiration);
        }
        public static void Delete(string userName)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(userName))
            {
                memoryCache.Remove(userName);
            }
        }


    }
}





