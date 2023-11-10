using Basket.Domain.Entities;
using Basket.Domain.Interfaces.MessageQueue;
using EventBus.Messages.Events;
using MassTransit;

namespace Basket.Infra.MessageQueue
{
    public class PublishEventsRabbitMQ : IPublishEvents
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishEventsRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> PublishCheckoutEvent(BasketCheckout basketCheckout)
        {
            // Mapear para BasketCheckoutEvent
            await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);
            return true;
        }
    }
}
