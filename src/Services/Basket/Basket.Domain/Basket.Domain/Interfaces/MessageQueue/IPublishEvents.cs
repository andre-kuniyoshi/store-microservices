using Basket.Domain.Entities;

namespace Basket.Domain.Interfaces.MessageQueue
{
    public interface IPublishEvents
    {
        Task<bool> PublishCheckoutEvent(BasketCheckout basketCheckout);
    }
}
