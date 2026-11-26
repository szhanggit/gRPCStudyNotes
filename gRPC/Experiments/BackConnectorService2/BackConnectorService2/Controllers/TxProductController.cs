using Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servcies.Command;
using Servcies.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackConnectorService2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxProductController : ControllerBase
    {
        protected readonly IMediator mediator;
        public TxProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut("Status")]
        public async Task<IActionResult> Put([FromForm] UpdateTxProductStatusCommand update, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(update, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> Get([FromQuery] GetTxProductByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
