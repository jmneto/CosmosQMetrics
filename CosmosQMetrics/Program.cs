using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CosmosQMetrics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("CosmosQMetrics");

                // Get the Json configuration file location from Argument 0 and read its contents
                var file = args[0];
                var jsonString = System.IO.File.ReadAllText(file);

                // Deserialize the Json document
                var inspectQuery = JsonSerializer.Deserialize<InspectQuery>(jsonString);

                // Perform sanity check on the arguments
                if (string.IsNullOrEmpty(inspectQuery.EndpointUri))
                {
                    throw new ArgumentNullException("Please specify an endpoint in the appSettings.json");
                }

                if (string.IsNullOrEmpty(inspectQuery.Key))
                {
                    throw new ArgumentException("Please specify an AuthorizationKey in the appSettings.json");
                }

                if (string.IsNullOrEmpty(inspectQuery.DatabaseId))
                {
                    throw new ArgumentException("Please specify Database ID in the appSettings.json");
                }

                if (string.IsNullOrEmpty(inspectQuery.ContainerId))
                {
                    throw new ArgumentException("Please specify Container ID in the appSettings.json");
                }

                if (string.IsNullOrEmpty(inspectQuery.Query))
                {
                    throw new ArgumentException("Please specify the Query in the appSettings.json");
                }

                // Inform the query we are Inspecting
                Console.WriteLine("Inpecting Query:");
                Console.WriteLine(inspectQuery.Query);

                // Intantiate the cosmos Helper
                CosmosDBhelper c = new CosmosDBhelper(inspectQuery.EndpointUri, inspectQuery.Key, inspectQuery.DatabaseId, inspectQuery.ContainerId);

                // Test our query
                await c.TestQuery(inspectQuery.Query, inspectQuery.WriteMetrics, inspectQuery.WriteDiagnostics, inspectQuery.WriteStream);

            }
            catch (CosmosException cre)
            {
                Console.WriteLine(cre.ToString());
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("Done");
                //Console.ReadKey();
            }
        }
    }
}
