using gizmogeo.Application.common;
using gizmogeo.Application.CompletedOrders.Dtos;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetAllCompletedOrders;

public record GetAllCompletedOrdersQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<CompletedOrderDto>>;