using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.AddTrainer;

public record AddTrainerCommand(
    Guid SubscriptionId,
    Guid GymId,
    Guid TrainerId) : IRequest<ErrorOr<Success>>;