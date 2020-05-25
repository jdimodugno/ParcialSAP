using System.Collections.Generic;
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
        public Withdrawal Create([FromBody] Withdrawal entity)
        {
            return _component.Create(entity);
        }
    }
}
