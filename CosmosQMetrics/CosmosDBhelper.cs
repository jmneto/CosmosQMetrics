using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CosmosQMetrics
{
    class CosmosDBhelper
    {
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        private string endpointUri;
        private string primaryKey;
        private string databaseId;
        private string containerId;

        public CosmosDBhelper(string uri, string pk, string db, string ct)
        {
            endpointUri = uri;
            primaryKey = pk;
            databaseId = db;
            containerId = ct;

            // Create a new instance of the Cosmos Client
            cosmosClient = new CosmosClient(endpointUri, primaryKey);
            database = cosmosClient.GetDatabase(databaseId);
            container = database.GetContainer(containerId);
        }

        public async Task TestQuery(string q, bool wm, bool wd, bool ws)
        {
            try
            {
                using (FeedIterator documentQuery = container.GetItemQueryStreamIterator(q))
                {
                    while (documentQuery.HasMoreResults)
                    {
                        ResponseMessage response = await documentQuery.ReadNextAsync();

                        JToken parsedJson = JToken.Parse(response.Diagnostics.ToString());

                        //object►children►1►children►1►children►0►children►0►data►Query Metrics

                        // Write out the Metrics
                        if (wm)
                        {
                            IList<JToken> t = parsedJson.SelectTokens("$.....['Query Metrics']").ToList();
                            foreach (string s in t)
                            {
                                Console.WriteLine(s);
                            }
                        }

                        // Write out Full Diagnostic Info
                        if (wd)
                            Console.WriteLine(parsedJson.ToString(Formatting.Indented));

                        // Write out Stream
                        if (ws)
                            Console.WriteLine(JToken.Parse(new StreamReader(response.Content).ReadToEnd()).ToString(Formatting.Indented));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
