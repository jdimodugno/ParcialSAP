using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.Helpers;
using Core;
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
        public IActionResult Create([FromBody] Deposit entity)
        {
            try
            {
                OperationResult<Deposit> result = _component.Create(entity);
                return result.HasError ? StatusCode(412, result.Error) : Ok(result.data);
            }
            catch (Exception ex)
            {
                return StatusCode(412, ex.Message);
            }
        }
    }
}
