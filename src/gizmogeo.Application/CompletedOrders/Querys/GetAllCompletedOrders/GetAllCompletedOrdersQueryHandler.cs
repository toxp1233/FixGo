using AutoMapper;
using AutoMapper.QueryableExtensions;
using gizmogeo.Application.common;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Application.CompletedOrders.Querys.GetAllCompletedOrders;

public class GetAllCompletedOrdersQueryHandler(ICompletedOrderRepository completedOrderRepository, IMapper mapper) : IRequestHandler<GetAllCompletedOrdersQuery, PaginatedList<CompletedOrderDto>>
{
    public async Task<PaginatedList<CompletedOrderDto>> Handle(GetAllCompletedOrdersQuery request, CancellationToken cancellationToken)
    {
        var completedOrders = completedOrderRepository.GetQueryable().Include(u => u.CompletedByUser);
        if (!string.IsNullOrEmpty(request.PaginationParams.Search))
        {
            var search = request.PaginationParams.Search.ToLower();
            completedOrders = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<CompletedOrder, User>)completedOrders.Where(sr => sr.CompletedByUser.Name.Contains(search));
        }
        var completedOrdersDto = completedOrders.ProjectTo<CompletedOrderDto>(mapper.ConfigurationProvider);
        return await PaginatedList<CompletedOrderDto>.CreateAsync(completedOrdersDto, request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
    }
}
