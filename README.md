# CosmosQMetrics
Azure Cosmos DB - Utility to show query metrics and diagnostic information

This program uses Azure Cosmos DB SDK 3.x to execute a query in Azure Cosmos DB and retrieve diagnostics information

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

# Sample Output

```
CosmosQMetrics
Inpecting Query:
select * from c
Retrieved Document Count                 :               2
Retrieved Document Size                  :             543 bytes
Output Document Count                    :               2
Output Document Size                     :             593 bytes
Index Utilization                        :          100.00 %
Total Query Execution Time               :            0.37 milliseconds
  Query Preparation Time                 :            0.11 milliseconds
  Index Lookup Time                      :            0.00 milliseconds
  Document Load Time                     :            0.02 milliseconds
  Runtime Execution Times                :            0.00 milliseconds
  Document Write Time                    :            0.00 milliseconds

Index Utilization Information
  Utilized Single Indexes
  Potential Single Indexes
  Utilized Composite Indexes
  Potential Composite Indexes
  
Done
```
