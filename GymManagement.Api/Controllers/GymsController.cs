using ErrorOr;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ControllerBase
{
    private readonly ISender _mediator;

    public GymsController(ISender mediator) =>
        _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateGym(
        [FromBody] CreateGymRequest request,
        [FromRoute] Guid subscriptionId)
    {
        var createGymCommand = new CreateGymCommand(request.Name, subscriptionId);
        var createGymResult = await _mediator.Send(createGymCommand);

        return createGymResult.MatchFirst(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
                error => Problem(error.ToString()));
    }

    [HttpDelete("{gymId:guid}")]
    public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
    {
        var command = new DeleteGymCommand(subscriptionId, gymId);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            // todo
            error => NoContent());
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var query = new ListGymsQuery(subscriptionId);
        var result = await _mediator.Send(query);

        return result.MatchFirst(
            gyms => Ok(
                gyms.ConvertAll(g => 
                new GymResponse(g.Id, g.Name))),
            error => Problem());
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        var query = new GetGymQuery(subscriptionId, gymId);
        var result = await _mediator.Send(query);

        return result.MatchFirst(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            error => Problem());
    }
    
    
}