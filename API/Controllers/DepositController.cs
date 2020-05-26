using System;
using System.Collections.Generic;
using Core;
using Core.Business;
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

        [HttpGet("{id}")]
        public List<Deposit> ReadByAccountNumber(string Id)
        {
            return ((DepositComponent)_component).ReadByAccountNumber(Id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Deposit entity)
        {
            try
            {
                OperationResult<Deposit> result = _component.Create(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
