using MediatR;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.Contracts.Subscriptions;
using DomainSubscription = GymManagement.Domain.Subscriptions.SubscriptionType;
namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender  _mediator;

    public SubscriptionsController(ISender mediator) =>
        _mediator = mediator;
    
    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);
        var result = await _mediator.Send(query);

        return result.MatchFirst(
            subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
            error => Problem());
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!DomainSubscription.TryFromName(
                request.SubscriptionType.ToString(),
                out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid subscription type.");
        }
        
        var command = new CreateSubscriptionCommand(
            subscriptionType, request.AdminId);

        var result = await _mediator.Send(command);

        return result.MatchFirst(
            subscription => Ok(
                new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            error => Problem());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            error => NoContent());
    }
}