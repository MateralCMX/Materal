using Materal.TFMS.EventBus;

namespace Materal.BaseCore.EventBus.TestClient
{
    public class NewRegistrationFormEventHandler : IIntegrationEventHandler<NewRegistrationFormEvent>
    {
        public Task HandleAsync(NewRegistrationFormEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
