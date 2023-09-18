using Azure;
using Azure.Data.Tables;

namespace StorageAccount.Models
{
    public class eployees : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get ; set ; }
        public DateTimeOffset? Timestamp { get ; set ; }
        public ETag ETag { get; set ; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}
