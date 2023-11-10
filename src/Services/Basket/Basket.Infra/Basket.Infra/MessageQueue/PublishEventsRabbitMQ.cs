using Basket.Domain.Interfaces.MessageQueue;
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

        public async Task<bool> PublishCheckoutEvent()
        {
            await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);
            return true;
        }
    }
}
