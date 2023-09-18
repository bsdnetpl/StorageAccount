using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAccount.Models;

namespace StorageAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private TableClient _tableClient;
        public TableController()
        {
            var ConnectionString = "DefaultEndpointsProtocol=https;AccountName=azurecurse;AccountKey=U69rgC/BW3vvlnSsNLxMbooNnD7a2npELAxhmKIqckfjrsGGYn26r7SlLTf9x8eISfVv/Rbmkda3+ASthkbhJg==;EndpointSuffix=core.windows.net";
            TableServiceClient tableServiceClient = new TableServiceClient(ConnectionString);

            _tableClient = tableServiceClient.GetTableClient("employees");

        }

        [HttpPost]
        public async Task<IActionResult> Post(eployees employee)
        {

            await _tableClient.CreateIfNotExistsAsync();
            await _tableClient.AddEntityAsync(employee);
            return Accepted();

        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string rowKey, [FromQuery] string partitionKey)
        {

          var employee = await  _tableClient.GetEntityAsync<eployees>(partitionKey, rowKey);
            return Ok(employee);

        }

        [HttpGet("query")]
        public async Task<IActionResult> Query()
        {

            var employee = _tableClient.Query<eployees>(a => a.PartitionKey == "IT");
            return Ok(employee);

        }
    }
}
