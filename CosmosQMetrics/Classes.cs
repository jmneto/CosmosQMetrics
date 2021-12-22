using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosQMetrics
{
   public class InspectQuery
    {
        public string EndpointUri { get; set; }
        public string Key { get; set; }
        public string DatabaseId { get; set; }
        public string ContainerId { get; set; }
        public string Query { get; set; }
        public bool WriteMetrics { get; set; }
        public bool WriteDiagnostics { get; set; }
        public bool WriteStream { get; set; }
    }
}
