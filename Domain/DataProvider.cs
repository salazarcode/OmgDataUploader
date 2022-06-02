using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DataProvider
    {
        public int DataProviderID { get; set; }
        public string DataProviderName { get; set; }
        public string AzureContainerName { get; set; }
        public List<DataFile> ProcessedDataFiles { get; set; }
        public List<string> ActualFiles { get; set; }
        public List<string> NewFiles { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
