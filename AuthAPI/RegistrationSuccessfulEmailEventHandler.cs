using EventBus.Messages.Events;
using MassTransit;

namespace OrderAPI.Integration;
public class RegistrationSuccessfulEmailEventHandler : IConsumer<NotificationEvent>
{
    Task IConsumer<NotificationEvent>.Consume(ConsumeContext<NotificationEvent> context)
    {
        if (context.Message.Message != null)
        {
            Console.WriteLine(context.Message.Message);
        }

        return Task.CompletedTask;
    }
}
