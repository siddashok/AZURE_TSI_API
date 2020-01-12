using Microsoft.Azure.TimeSeriesInsights;
using Microsoft.Azure.TimeSeriesInsights.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.WindowsAzure;
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace TestAppTSI.Controllers
{

    public class TestController : ApiController


    {
        [HttpGet]
        [System.Web.Http.Route("Api/GetSeries")]
        public async Task<QueryResultPage> GetSeries(string EnvironmentFqdn, string TimeSeriesId)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            string continuationToken;
            do
            {
                DateTimeRange SearchSpan = new DateTimeRange(new DateTime(2019, 12, 26).ToUniversalTime(), new DateTime(2019, 12, 27).ToUniversalTime());
                //object[] TimeSeriesIdValue = new object[] { "AND-ARU-XI82121_F" };
                QueryResultPage queryResponse = await tsi.ExecuteQueryPagedAsync(
                        new QueryRequest(
                            getSeries: new GetSeries(
                                timeSeriesId: new object[] { TimeSeriesId },
                                searchSpan: SearchSpan,
                                filter: null,
                                 projectedVariables: new[] { "Float" },
                            inlineVariables: new Dictionary<string, NumericVariable>()
                            {
                                ["Float"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("avg($value)"))
                            })));
                return queryResponse;
                //continuationToken = queryResponse.ContinuationToken;
            }
            while (continuationToken != null);
           

        }



        [HttpGet]
        [System.Web.Http.Route("Api/GetAggregateSeries")]
        public async Task<QueryResultPage> GetAggregateSeries(string EnvironmentFqdn, string TimeSeriesId)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            string continuationToken;
            do
            {
                DateTimeRange SearchSpan = new DateTimeRange(new DateTime(2019, 12, 26).ToUniversalTime(), new DateTime(2019, 12, 27).ToUniversalTime());
                //object[] TimeSeriesIdValue = new object[] { "AND-ARU-XI82121_F" };
                QueryResultPage queryResponse = await tsi.ExecuteQueryPagedAsync(
                        new QueryRequest(
                        aggregateSeries: new AggregateSeries(
                            timeSeriesId: new object[] { TimeSeriesId },
                            searchSpan: SearchSpan,
                            filter: null,
                            interval: TimeSpan.FromHours(5),
                            projectedVariables: new[] { "Min_Numeric", "Max_Numeric", "Sum_Numeric", "Avg_Numeric", "First_Numeric", "Last_Numeric"},
                            inlineVariables: new Dictionary<string, Variable>()
                            {
                                ["Min_Numeric"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("min($value)")),
                                ["Max_Numeric"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("max($value)")),
                                ["Sum_Numeric"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("sum($value)")),
                                ["Avg_Numeric"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("avg($value)")),
                                ["First_Numeric"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("first($value)")),
                                ["Last_Numeric"] = new NumericVariable(
                                    value: new Tsx("$event.series_value"),
                                    aggregation: new Tsx("last($value)"))
                            })));
                return queryResponse;
                //continuationToken = queryResponse.ContinuationToken;
            }
            while (continuationToken != null);


        }



        [HttpGet]
        [System.Web.Http.Route("Api/GetEvents")]
        public async Task<QueryResultPage> GetEvents(string EnvironmentFqdn, string TimeSeriesId)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            string continuationToken;
            do
            {
                DateTimeRange SearchSpan = new DateTimeRange(new DateTime(2019, 12, 26).ToUniversalTime(), new DateTime(2019, 12, 27).ToUniversalTime());
                //object[] TimeSeriesIdValue = new object[] { "AND-ARU-XI82121_F" };
                QueryResultPage queryResponse = await tsi.ExecuteQueryPagedAsync(
                        new QueryRequest(
                            getSeries: new GetSeries(
                                timeSeriesId: new object[] { TimeSeriesId },
                                searchSpan: SearchSpan,
                                filter: null
                                 )));
                return queryResponse;
                //continuationToken = queryResponse.ContinuationToken;
            }
            while (continuationToken != null);


        }





        [HttpPut]
        [System.Web.Http.Route("Api/PutHierarchy")]
        public async Task<HierarchiesBatchResponse> CreateNewHierarchy(string EnvironmentFqdn, IList<TimeSeriesHierarchy> value)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            HierarchiesBatchResponse hierarchies =
                   await tsi.ExecuteHierarchiesBatchOperationAsync(new HierarchiesBatchRequest(put: value));
            return hierarchies;
        }


        [HttpGet]
        [System.Web.Http.Route("Api/GetHierarchy")]
        public async Task<HierarchiesBatchResponse> GetHierarchy(string EnvironmentFqdn,string id)
        { 
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            HierarchiesBatchResponse hierarchies =
            await tsi.ExecuteHierarchiesBatchOperationAsync(new HierarchiesBatchRequest(get: new HierarchiesRequestBatchGetDelete(hierarchyIds: new List<Guid?>() { Guid.Parse(id) })));
            return hierarchies;
        }



        [HttpDelete]
        [System.Web.Http.Route("Api/DeleteHierarchy")]
        public async Task<HierarchiesBatchResponse> DeleteHierarchy(string EnvironmentFqdn, string id)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            HierarchiesBatchResponse hierarchies =
            await tsi.ExecuteHierarchiesBatchOperationAsync(new HierarchiesBatchRequest(delete: new HierarchiesRequestBatchGetDelete(hierarchyIds: new List<Guid?>() { Guid.Parse(id) })));
            return hierarchies;
        }


        [HttpPut]
        [System.Web.Http.Route("Api/PutTypes")]
        public async Task<TypesBatchResponse> CreateNewType(string EnvironmentFqdn, IList<TimeSeriesType> value)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result; //all correct 
            TypesBatchResponse types = await tsi.ExecuteTypesBatchOperationAsync(new TypesBatchRequest(put: value));
            return types;
        }


        [HttpGet]
        [System.Web.Http.Route("Api/GetTypes")]
        public async Task<TypesBatchResponse> GetType(string EnvironmentFqdn, string id)
        {

            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            TypesBatchResponse types = await tsi.ExecuteTypesBatchOperationAsync(
                new TypesBatchRequest(
                    get: new TypesRequestBatchGetOrDelete(typeIds: new List<Guid?>() { Guid.Parse(id) })));
            return types;
        }

        [HttpDelete]
        [System.Web.Http.Route("Api/DeleteTypes")]
        public async Task<TypesBatchResponse> DeleteType(string EnvironmentFqdn, string id)
        {

            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            TypesBatchResponse types = await tsi.ExecuteTypesBatchOperationAsync(
                new TypesBatchRequest(
                    delete: new TypesRequestBatchGetOrDelete(typeIds: new List<Guid?>() { Guid.Parse(id) })));
            return types;
        }

        [HttpPut]
        [System.Web.Http.Route("Api/PutInstance")]
        public async Task<InstancesBatchResponse> CreateNewInstance(string EnvironmentFqdn, IList<TimeSeriesInstance> Instance)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            InstancesBatchResponse instances = await tsi.ExecuteInstancesBatchOperationAsync(
                new InstancesBatchRequest(put: Instance));
            return instances;
        }



        [HttpGet]
        [System.Web.Http.Route("Api/GetInstance")]
        public async Task<InstancesBatchResponse> GetInstance(string EnvironmentFqdn, string TimeSeriesId)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            object[] TimeSeriesIdValue = new object[] { TimeSeriesId };
            InstancesBatchResponse instances = await tsi.ExecuteInstancesBatchOperationAsync(
                new InstancesBatchRequest(get: new IList<object>[] { TimeSeriesIdValue }));

            return instances;
        }


        [HttpDelete]
        [System.Web.Http.Route("Api/DeleteInstance")]
        public async Task<InstancesBatchResponse> DeleteInstance(string EnvironmentFqdn, object[] id)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            InstancesBatchResponse instances = await tsi.ExecuteInstancesBatchOperationAsync(
                new InstancesBatchRequest(delete: new IList<object>[] { id }));

            return instances;
        }






        private static async Task<TimeSeriesInsightsClient> GetTimeSeriesInsightsClientAsync(string EnvironmentFqdn)
        {
            
            const string ResourceUri = "https://api.timeseries.azure.com/";
            const string PowerShellAadClientId = "<clientid>";
            const string PowerShellAadClientSecret = "<clientsecret>";
            const string AzureActiveDirectoryLoginUrl = "https://login.microsoftonline.com/common";
            const string MicrosoftTenantId = "<tenanatid>";
            

            //string hostName = Dns.GetHostName();
            //string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            System.Web.HttpContext context1 = System.Web.HttpContext.Current;
            string ipAddress = context1.Request.ServerVariables["REMOTE_ADDR"];

            ClientCredential ClientCredential = new ClientCredential(PowerShellAadClientId, PowerShellAadClientSecret);
            AuthenticationContext context = new AuthenticationContext($"{AzureActiveDirectoryLoginUrl}/{MicrosoftTenantId}", TokenCache.DefaultShared);
            AuthenticationResult authenticationResult = await context.AcquireTokenAsync(ResourceUri, ClientCredential);

            TokenCloudCredentials tokenCloudCredentials = new TokenCloudCredentials(authenticationResult.AccessToken);
            //TokenCloudCredentials tokenCloudCredentials = new TokenCloudCredentials(AccessToken);
            var tokenvalue = tokenCloudCredentials.Token;
            ServiceClientCredentials serviceClientCredentials = new TokenCredentials(tokenCloudCredentials.Token);

            TimeSeriesInsightsClient timeSeriesInsightsClient = new TimeSeriesInsightsClient(credentials: serviceClientCredentials)
            {
                EnvironmentFqdn = EnvironmentFqdn
            };
            return timeSeriesInsightsClient;
        }



        





    }
}


