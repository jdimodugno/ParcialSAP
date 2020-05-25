using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.Helpers;
using Core.Business;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/deposits")]
    public class DepositController : GenericController<Deposit, DepositComponent>
    {
        public DepositController(ILogger<DepositController> logger)
            : base(logger, new DepositComponent()) { }


        [HttpGet]
        public IEnumerable<Deposit> Read()
        {
            return _component.Read(m => m.Id != null);
        }

        [HttpPost]
        public Deposit Create([FromBody] Deposit entity)
        {
            return _component.Create(entity);
        }
    }
}
