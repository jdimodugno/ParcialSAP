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
    [Route("api/withdrawals")]
    public class WithdrawalController : GenericController<Withdrawal, WithdrawalComponent>
    {
        public WithdrawalController(ILogger<WithdrawalController> logger)
            : base(logger, new WithdrawalComponent()) { }

        [HttpGet]
        public IEnumerable<Withdrawal> Read()
        {
            return _component.Read(m => m.Id != null);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Withdrawal entity)
        {
            try
            {
                OperationResult<Withdrawal> result = _component.Create(entity);
                return result.HasError ? StatusCode(412, result.Error) : Ok(result.data);
            }
            catch (Exception ex)
            {
                return StatusCode(412, ex.Message);
            }
        }
    }
}
