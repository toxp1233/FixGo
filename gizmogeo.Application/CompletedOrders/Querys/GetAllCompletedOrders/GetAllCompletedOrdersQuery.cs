using gizmogeo.Application.CompletedOrders.Dtos;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetAllCompletedOrders;

public record GetAllCompletedOrdersQuery : IRequest<IEnumerable<CompletedOrderDto>>;