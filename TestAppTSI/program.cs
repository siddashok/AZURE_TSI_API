using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.TimeSeriesInsights;
using Microsoft.Azure.TimeSeriesInsights.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using DateTimeRange = Microsoft.Azure.TimeSeriesInsights.Models.DateTimeRange;
using Microsoft.WindowsAzure;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestAppTSI
{
    public class program
    {

        private const string ResourceUri = "https://api.timeseries.azure.com/";
        public const string PowerShellAadClientId = "9e7410a9-c382-4325-8e24-85ece431e772";
        public const string PowerShellAadClientSecret = "a/IdF9+s9q4+2_p/jXbRzx.-=bV3s]Qs";
        private const string AzureActiveDirectoryLoginUrl = "https://login.microsoftonline.com/common";
        private const string MicrosoftTenantId = "ea80952e-a476-42d4-aaf4-5457852b0f7e";
        private const string EnvironmentFqdn = "a832f7fc-adf1-4e67-a0ca-38d366d2a209.env.timeseries.azure.com";
        private static object[] TimeSeriesId = new object[] { "MD_2_AN_LPC_ASC_TRV_CntrlLine.PV" };



        private static TimeSeriesInsightsClient _client;

        static void Main(string[] args)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // provides connection management for HTTP Connections

            _client = GetTimeSeriesInsightsClientAsync().Result;
            
        }

        private static async Task<TimeSeriesInsightsClient> GetTimeSeriesInsightsClientAsync()
        {
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

