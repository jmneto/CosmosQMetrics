# CosmosQMetrics
Azure Cosmos DB - Utility to review query metrics and diagnostic information

This program uses Azure Cosmos DB SDK 3.x to execute a quuery in Azure Cosmos DB and retrieve diagnostics information

# Usage:

1. Create a configuration JSON document with the required information to connect and execute your query

Example:

```
{
  "EndpointUri": "https://cosmosdb.documents.azure.com:443/",
  "Key": "your-cosmosdb-key",
  "DatabaseId": "MyDatabase",
  "ContainerId": "MyContainer",
  "Query": "select * from c",
  "WriteMetrics": true,
  "WriteDiagnostics": false,
  "WriteStream":  false
}
```


2. Call CosmosQMetrics program providing the path to the configuration JSON file

Example:

```
CosmosQMetrics test.json
```
