using System;
using System.Collections.Generic;
using Core.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public abstract class GenericController<T, Z> : ControllerBase
        where T : class
        where Z : BaseComponent<T>
    {
        protected readonly ILogger<GenericController<T, Z>> _logger;
        protected readonly BaseComponent<T> _component;

        public GenericController(ILogger<GenericController<T, Z>> logger, BaseComponent<T> component)
        {
            _logger = logger;
            _component = component;
        }
    }
}
