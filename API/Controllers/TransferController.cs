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
        public Transfer Create([FromBody] Transfer entity)
        {
            return _component.Create(entity);
        }
    }
}
