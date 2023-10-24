using AzSerciseBusDemo.Models;

namespace AzSerciseBusDemo.Repositories
{
    public interface IserviceBus
    {
        Task SendMessageAsync(CarDetails carDetails);
    }
}
