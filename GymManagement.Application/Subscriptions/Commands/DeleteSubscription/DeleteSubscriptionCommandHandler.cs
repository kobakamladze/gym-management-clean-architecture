// using ErrorOr;
// using GymManagement.Application.Common.Interfaces;
// using MediatR;
//
// namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
//
// public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
// {
//     private readonly IGymRepository _gymRepository;
//     private readonly ISubscriptionsRepository _subscriptionsRepository;
//     private readonly IUnitOfWork _unitOfWork;
//
//     public DeleteSubscriptionCommandHandler(
//         IGymRepository gymRepository,
//         ISubscriptionsRepository subscriptionsRepository,
//         IUnitOfWork unitOfWork)
//     {
//         _gymRepository = gymRepository;
//         _subscriptionsRepository = subscriptionsRepository;
//         _unitOfWork = unitOfWork;
//     }
//     
//     public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
//     {
//         var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
//         if (subscription is null) return Error.NotFound(description: "Subscription not found.");
//
//     }
// }