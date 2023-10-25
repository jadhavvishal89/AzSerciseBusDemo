using AzSerciseBusDemo.Models;
using Azure.Messaging.ServiceBus;

namespace AzSerciseBusDemo.Repositories
{
    public interface IserviceBus
    {
        Task SendMessageAsync(CarDetails carDetails);
        Task GetAllMessageAsync();

        Task<string> GetCarDetailsMessageAsync();

    }
}
