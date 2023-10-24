using AzSerciseBusDemo.Models;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;
using System.Text;
using Azure.Messaging.ServiceBus;

namespace AzSerciseBusDemo.Repositories
{
    public class ServiceBus : IserviceBus
    {
        private readonly IConfiguration _configuration;
        public ServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMessageAsync(CarDetails carDetails)
        {
            IQueueClient client = new QueueClient(_configuration["AzServiceBusConnectionStrings"], _configuration["QueueName"]);

            //Serialize car details object
            var messageBody = JsonSerializer.Serialize(carDetails);

            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                MessageId = Guid.NewGuid().ToString(),
                ContentType = "application/json"
            };
            await client.SendAsync(message);

        }

        //public async Task GetMessageAsync()

        //{
        //    // IQueueClient client = new QueueClient(_configuration["AzServiceBusConnectionStrings"], _configuration["QueueName"]);

        //    ServiceBusClient client = new ServiceBusClient(_configuration["AzServiceBusConnectionStrings"]);
        //    var receiver= client.CreateReceiver(_configuration["QueueName"]);

        //    //var message =  await receiver.ReceiveMessagesAsync();

           
        //}
    }
}
