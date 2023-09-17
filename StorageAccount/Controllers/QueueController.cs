using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageAccount.Models;
using System.Text.Json;

namespace StorageAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpPost("publish")]
        public async Task<IActionResult> Publish(ReturnDto returnDto)
        {
            var ConnectionStringQueue = "DefaultEndpointsProtocol=https;AccountName=azurecurse;AccountKey=U69rgC/BW3vvlnSsNLxMbooNnD7a2npELAxhmKIqckfjrsGGYn26r7SlLTf9x8eISfVv/Rbmkda3+ASthkbhJg==;EndpointSuffix=core.windows.net";
            var QueueName = "returns";
            QueueClient queueClient = new QueueClient(ConnectionStringQueue,QueueName);
            await queueClient.CreateIfNotExistsAsync();
            var serialize = JsonSerializer.Serialize(returnDto);
            await queueClient.SendMessageAsync(serialize);
            return Ok();
        }
    }
}
