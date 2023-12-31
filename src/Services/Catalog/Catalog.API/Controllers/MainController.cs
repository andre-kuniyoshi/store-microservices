﻿using Core.NotifierErrors;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    public abstract class MainController<T> : ControllerBase where T : class
    {
        private readonly INotifier _notifier;
        protected readonly ILogger<T> Logger;
        protected MainController(INotifier notifier, ILogger<T> logger)
        {
            _notifier = notifier;
            Logger = logger;
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications()
            });
        }

        protected void NotifyErrorMessage(string field, string message)
        {
            _notifier.AddNotication(new Notification(field, message));
        }
    }
}
