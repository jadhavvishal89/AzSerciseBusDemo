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
        ServiceBusClient client;
        ServiceBusSender sender;
        ServiceBusReceiver receiver;
        public ServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;

            // the client that owns the connection and can be used to create senders and receivers
             client = new ServiceBusClient(_configuration["AzServiceBusConnectionStrings"]);

            // the sender used to publish messages to the queue
             sender = client.CreateSender(_configuration["QueueName"]);
             receiver = client.CreateReceiver(_configuration["QueueName"]);
        }
        public async Task SendMessageAsync(CarDetails carDetails)
        {
            //Add Meesage to Queue with client
            //IQueueClient client = new QueueClient(_configuration["AzServiceBusConnectionStrings"], _configuration["QueueName"]);

            ////Serialize car details object
            //var messageBody = JsonSerializer.Serialize(carDetails);

            //var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            //{
            //    MessageId = Guid.NewGuid().ToString(),
            //    ContentType = "application/json"
            //};
            //await client.SendAsync(message);

            // Option 2 Add message to queue with sender in queue

            //Serialize car details object
            var messageBody = JsonSerializer.Serialize(carDetails);
            
            ServiceBusMessage message = new ServiceBusMessage(messageBody);

            await sender.SendMessageAsync(message);

        }
        public async Task<string> GetCarDetailsMessageAsync()
        {
            var receivedmessage=  await receiver.ReceiveMessageAsync();

            var body = receivedmessage.Body.ToString();
            return body;
        }
        public async Task GetAllMessageAsync()

        {
            ServiceBusProcessor processor = client.CreateProcessor(_configuration["QueueName"], new ServiceBusProcessorOptions());
                        
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var carDetails = args.Message.Body.ToString();
            
            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
           // return carDetails;

        }
        // handle any errors when receiving messages
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        
    }
}
