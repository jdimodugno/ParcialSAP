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
        public IActionResult Create([FromBody] BankAccount entity)
        {
            try
            {
                OperationResult<BankAccount> result = _component.Create(entity);
                return result.HasError ? StatusCode(412, result.Error) : Ok(result.data);
            }
            catch (Exception ex)
            {
                return StatusCode(412, ex.Message);
            }
        }
    }
}
