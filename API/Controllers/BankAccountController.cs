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
    [Route("api/accounts")]
    public class BankAccountController : GenericController<BankAccount, BankAccountComponent>
    {

        public BankAccountController(ILogger<BankAccountController> logger)
            : base(logger, new BankAccountComponent()) { }

        [HttpGet]
        [Route("types")]
        public JsonResult GetAccountTypes() => new JsonResult(EnumHelper<AccountTypes>.ToJson());

        [HttpGet]
        public IEnumerable<BankAccount> Read()
        {
            return _component.Read(m => m.Number != null);
        }

        [HttpGet("{id}")]
        public BankAccount ReadByNumber(string Id)
        {
            return ((BankAccountComponent)_component).ReadByNumber(Id);
        }

        [HttpPost]
        public BankAccount Create([FromBody] BankAccount entity)
        {
            return _component.Create(entity);
        }
    }
}
