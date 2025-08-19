using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxation.Api.Contracts;
using Taxation.Application.UseCases.CalculateVat;

namespace Taxation.Api.Controllers;

[ApiController]
[Produces("application/problem+json")]
[Route("api/v1/taxation")]
public class Taxation(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Calculates Net, Gross and Vat based on the given amount and VAT rate
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("actions/calculate")]
    [ProducesResponseType(typeof(CalculateVatResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IResult> Calculate([FromBody] CalculateVatRequest request, CancellationToken cancellationToken)
    {
        var cmd = new CalculateVatCommand(request.Net, request.Gross, request.Vat, request.VatRate, "AT");
        var result = await mediator.Send(cmd, cancellationToken);

        // TODO: this could be done by extension method with mapping different errors to different status codes
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Errors);
    }
}