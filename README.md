# AZURE_TSI_API
Azure TSI Model and Query APIs with Swagger UI


The project code which has been modified to use the Azure TSI APIs as custom APIs along with Swagger UI. 

Consumers who are working on TSI Model Hierarchy would be able to retrieve the existing Hierarchies / Types / Instances , 
would be able to create a new or update an existing Hierarchies / Types / Instances and Delete a specific Hierarchies / Types
/ Instances

Consumers who are working on TSI Query would be able to get series, aggregate series and events for a particular time series ID. 



TO DO: 

1.In the Testcontroller.cs file, you need to replace the value of the client id, secret and tenant id of your organiation. 

2. Build the solution and run it. 


For basic understanding about the Azure TSI APIs, go to : https://docs.microsoft.com/en-us/azure/time-series-insights/

Modifications have been made to the code from github: https://github.com/Azure-Samples/Azure-Time-Series-Insights/tree/master/csharp-tsi-preview-sample
