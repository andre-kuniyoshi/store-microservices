using Core.NotifierErrors;

namespace Catalog.API.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void NotifyErrorMessage(string field, string message)
        {
            _notifier.AddNotication(new Notification(field, message));
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
