using Microsoft.Azure.TimeSeriesInsights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestAppTSI.Model;
using Microsoft.Azure.TimeSeriesInsights;
using System.Collections;
using Microsoft.Azure;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.WindowsAzure;

namespace TestAppTSI.Controllers
{
    
    public class TestController : ApiController
    {
        [HttpPut]
        [System.Web.Http.Route("Api/PutHierarchy")]
        public  async Task<HierarchiesBatchResponse> CreateNewHierarchy(string EnvironmentFqdn,IList<TimeSeriesHierarchy> value)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            HierarchiesBatchResponse hierarchies =
                   await tsi.ExecuteHierarchiesBatchOperationAsync(new HierarchiesBatchRequest(put:value));
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
        public async Task<HierarchiesBatchResponse> DeleteHierarchy(string EnvironmentFqdn,string id)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            HierarchiesBatchResponse hierarchies =
            await tsi.ExecuteHierarchiesBatchOperationAsync(new HierarchiesBatchRequest(delete: new HierarchiesRequestBatchGetDelete(hierarchyIds: new List<Guid?>() { Guid.Parse(id) })));
            return hierarchies;
        }


        [HttpPut]
        [System.Web.Http.Route("Api/PutTypes")]
        public async Task<TypesBatchResponse> CreateNewType(string EnvironmentFqdn,IList<TimeSeriesType> value)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result; //all correct 
            TypesBatchResponse types = await tsi.ExecuteTypesBatchOperationAsync(new TypesBatchRequest(put: value));
            return types;
        }


        [HttpGet]
        [System.Web.Http.Route("Api/GetTypes")]
        public async Task<TypesBatchResponse> GetType(string EnvironmentFqdn,List<Guid> id)
        {

            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            TypesBatchResponse types = await tsi.ExecuteTypesBatchOperationAsync(
                new TypesBatchRequest(
                    get: new TypesRequestBatchGetOrDelete(typeIds: new List<Guid?>() { Guid.Parse() })));
            return types;
        }

        [HttpDelete]
        [System.Web.Http.Route("Api/DeleteTypes")]
        public async Task<TypesBatchResponse> DeleteType(string EnvironmentFqdn,string id)
        {

            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            TypesBatchResponse types = await tsi.ExecuteTypesBatchOperationAsync(
                new TypesBatchRequest(
                    delete: new TypesRequestBatchGetOrDelete(typeIds: new List<Guid?>() { Guid.Parse(id) })));
            return types;
        }

        [HttpPut]
        [System.Web.Http.Route("Api/PutInstance")]
        public async Task<InstancesBatchResponse> CreateNewInstance(string EnvironmentFqdn,IList<TimeSeriesInstance> Instance)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;
            InstancesBatchResponse instances = await tsi.ExecuteInstancesBatchOperationAsync(
                new InstancesBatchRequest(put: Instance ));
            return instances;
        }



        [HttpGet]
        [System.Web.Http.Route("Api/GetInstance")]
        public async Task<InstancesBatchResponse> GetInstance(string EnvironmentFqdn,IList<IList<object>> TimeSeriesId)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            InstancesBatchResponse instances = await tsi.ExecuteInstancesBatchOperationAsync(
                new InstancesBatchRequest(get:  TimeSeriesId ));

            return instances;
        }


        [HttpDelete]
        [System.Web.Http.Route("Api/DeleteInstance")]
        public async Task<InstancesBatchResponse> DeleteInstance(string EnvironmentFqdn,object[] id)
        {
            TimeSeriesInsightsClient tsi = GetTimeSeriesInsightsClientAsync(EnvironmentFqdn).Result;

            InstancesBatchResponse instances = await tsi.ExecuteInstancesBatchOperationAsync(
                new InstancesBatchRequest(delete: new IList<object>[] { id }));

            return instances;
        }






        private static async Task<TimeSeriesInsightsClient> GetTimeSeriesInsightsClientAsync(string EnvironmentFqdn)
        {
            const string ResourceUri = "https://api.timeseries.azure.com/";
        const string PowerShellAadClientId = "9e7410a9-c382-4325-8e24-85ece431e772";
         const string PowerShellAadClientSecret = "a/IdF9+s9q4+2_p/jXbRzx.-=bV3s]Qs";
         const string AzureActiveDirectoryLoginUrl = "https://login.microsoftonline.com/common";
         const string MicrosoftTenantId = "ea80952e-a476-42d4-aaf4-5457852b0f7e";
         //const string EnvironmentFqdn = "fd1f93bf-fe73-4c67-bcf3-d37837e282b6.env.timeseries.azure.com";
        // object[] TimeSeriesId = new object[] { "CH_MAR - BALLAST WTR TK - 311 Volume(cu ft ^ 3)" };

        ClientCredential ClientCredential = new ClientCredential(PowerShellAadClientId, PowerShellAadClientSecret);
            AuthenticationContext context = new AuthenticationContext($"{AzureActiveDirectoryLoginUrl}/{MicrosoftTenantId}", TokenCache.DefaultShared);
            AuthenticationResult authenticationResult = await context.AcquireTokenAsync(ResourceUri, ClientCredential);

            TokenCloudCredentials tokenCloudCredentials = new TokenCloudCredentials(authenticationResult.AccessToken);
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


