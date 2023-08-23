using Register.Application.Domain.Entities;
using Register.Application.NotificationPattern;
using FluentValidation;
using FluentValidation.Results;
using Core.Common;

namespace Register.Application.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string mensagem)
        {
            _notifier.AddNotication(new Notification(mensagem));
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
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
