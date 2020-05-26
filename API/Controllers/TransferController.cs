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


        [HttpGet("source/{id}")]
        public List<Transfer> ReadBySourceAccountNumber(string Id)
        {
            return ((TransferComponent)_component).ReadBySourceAccountNumber(Id);
        }


        [HttpGet("target/{id}")]
        public List<Transfer> ReadByTargetAccountNumber(string Id)
        {
            return ((TransferComponent)_component).ReadByTargetAccountNumber(Id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Transfer entity)
        {
            try
            {
                OperationResult<Transfer> result = _component.Create(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
