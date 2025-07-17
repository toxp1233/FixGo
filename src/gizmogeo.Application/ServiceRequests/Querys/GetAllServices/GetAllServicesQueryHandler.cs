using AutoMapper;
using AutoMapper.QueryableExtensions;
using gizmogeo.Application.common;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Application.ServiceRequests.Querys.GetAllServices
{
    internal class GetAllServicesQueryHandler(IServiceRequestRepository serviceRequestRepository, IMapper mapper) : IRequestHandler<GetAllServicesQuery, PaginatedList<ServiceRequestDto>>
    {
        public async Task<PaginatedList<ServiceRequestDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var serviceRequestsQuery = serviceRequestRepository.GetQueryable()
                .Include(u => u.User)
                .Include(c => c.Category)
                .Include(a => a.AcceptedRequest);

            if (!string.IsNullOrEmpty(request.PaginationParams.Search))
            {
                var search = request.PaginationParams.Search.ToLower();
                serviceRequestsQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<ServiceRequest, AcceptedRequest?>)serviceRequestsQuery.Where(sr => sr.Category.Name.Contains(search));
            }

            var serviceRequests = serviceRequestsQuery.ProjectTo<ServiceRequestDto>(mapper.ConfigurationProvider);
            return await PaginatedList<ServiceRequestDto>.CreateAsync(serviceRequests, request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
        }
    }
}
