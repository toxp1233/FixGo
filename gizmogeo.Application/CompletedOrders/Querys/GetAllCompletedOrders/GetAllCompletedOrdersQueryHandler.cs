using AutoMapper;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetAllCompletedOrders;

public class GetAllCompletedOrdersQueryHandler(ICompletedOrderRepository completedOrderRepository, IMapper mapper) : IRequestHandler<GetAllCompletedOrdersQuery, IEnumerable<CompletedOrderDto>>
{
    public async Task<IEnumerable<CompletedOrderDto>> Handle(GetAllCompletedOrdersQuery request, CancellationToken cancellationToken)
    {
        var completedOrders = await completedOrderRepository.GetAllAsync();
        return mapper.Map<IEnumerable<CompletedOrderDto>>(completedOrders);
    }
}
