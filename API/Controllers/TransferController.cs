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
    [Route("api/transfers")]
    public class TransferController : GenericController<Transfer, TransferComponent>
    {
        public TransferController(ILogger<TransferController> logger)
            : base(logger, new TransferComponent()) { }

        [HttpGet]
        public IEnumerable<Transfer> Read()
        {
            return _component.Read(m => m.Id != null);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Transfer entity)
        {
            try
            {
                OperationResult<Transfer> result = _component.Create(entity);
                return result.HasError ? StatusCode(412, result.Error) : Ok(result.data);
            }
            catch (Exception ex)
            {
                return StatusCode(412, ex.Message);
            }
        }
    }
}
