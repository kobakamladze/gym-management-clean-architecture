using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public record class CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;