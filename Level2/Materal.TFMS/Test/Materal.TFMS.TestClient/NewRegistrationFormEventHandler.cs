using Materal.TFMS.EventBus;

namespace Materal.TFMS.TestClient
{
    public class NewRegistrationFormEventHandler : IIntegrationEventHandler<NewRegistrationFormEvent>
    {
        public Task HandleAsync(NewRegistrationFormEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
