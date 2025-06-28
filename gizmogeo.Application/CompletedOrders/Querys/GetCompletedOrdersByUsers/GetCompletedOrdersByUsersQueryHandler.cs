using AutoMapper;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Querys.GetCompletedOrdersByUsers;

public class GetCompletedOrdersByUsersQueryHandler(ICompletedOrderRepository completedOrderRepository, IMapper mapper) : IRequestHandler<GetCompletedOrdersByUsersQuery, IEnumerable<CompletedOrderDto>>
{
    public async Task<IEnumerable<CompletedOrderDto>> Handle(GetCompletedOrdersByUsersQuery request, CancellationToken cancellationToken)
    {
        var completedOrders = await completedOrderRepository.GetByAdmin(request.UserId) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());
        return mapper.Map<IEnumerable<CompletedOrderDto>>(completedOrders);
    }
}
