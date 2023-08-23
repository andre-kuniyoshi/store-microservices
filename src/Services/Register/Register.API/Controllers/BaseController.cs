using AutoMapper;
using Register.Application.NotificationPattern;
using Microsoft.AspNetCore.Mvc;

namespace Register.App.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly INotifier _notifier;
        protected readonly ILogger<T> Logger;
        protected readonly IMapper Mapper;

        protected BaseController(IMapper mapper, INotifier notificador, ILogger<T> logger)
        {
            _notifier = notificador;
            Logger = logger;
            Mapper = mapper;
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
