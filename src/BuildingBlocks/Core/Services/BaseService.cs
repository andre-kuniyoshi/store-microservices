using Core.Common;
using FluentValidation;
using FluentValidation.Results;
using Core.NotifierErrors;

namespace Core.Services
{
    public abstract class BaseService
    {
        protected readonly INotifier Notifier;

        protected BaseService(INotifier notifier)
        {
            Notifier = notifier;
        }

        protected void Notify(string field, string message)
        {
            Notifier.AddNotication(new Notification(field, message));
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.PropertyName, error.ErrorMessage);
            }
        }

        protected bool ValidateEntity<TV, TE>(TV validator, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity
        {
            var xalidationResult = validator.Validate(entity);

            if (xalidationResult.IsValid) return true;

            Notify(xalidationResult);

            return false;
        }
    }
}
