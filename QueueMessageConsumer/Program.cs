
using Azure.Storage.Queues;
using QueueMessageConsumer;

var ConnectionStringQueue = "DefaultEndpointsProtocol=https;AccountName=azurecurse;AccountKey=U69rgC/BW3vvlnSsNLxMbooNnD7a2npELAxhmKIqckfjrsGGYn26r7SlLTf9x8eISfVv/Rbmkda3+ASthkbhJg==;EndpointSuffix=core.windows.net";
var QueueName = "returns";
QueueClient queueClient = new QueueClient(ConnectionStringQueue, QueueName);
while(true)
{
    var message = queueClient.ReceiveMessage();
    if(message.Value != null )
    {
        var Dto = message.Value.Body.ToObjectFromJson<ReturnDto>();
        Processor(Dto);
        await queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);

    }
    await Task.Delay(1000);
}

void Processor(ReturnDto dto)
{
    Console.WriteLine($"Procesing return width id {dto.Id}, " +
        $"for user : {dto.User}" +
        $"From Address: {dto.Address}");
}