using gizmogeo.Application.CompletedOrders.Dtos;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<CompletedOrderDto>;

