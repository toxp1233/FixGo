using AutoMapper;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetOrderById;

public class GetOrderByIdQueryHandler(ICompletedOrderRepository completedOrderRepository, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, CompletedOrderDto>
{
    public async Task<CompletedOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var completedOrder = await completedOrderRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(CompletedOrder), request.Id.ToString());
        return mapper.Map<CompletedOrderDto>(completedOrder);
    }
}
