using gizmogeo.Application.CompletedOrders.Dtos;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetCompletedOrdersByUsers;

public record GetCompletedOrdersByUsersQuery(Guid UserId) : IRequest<IEnumerable<CompletedOrderDto>>;
