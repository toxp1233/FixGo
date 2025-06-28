using AutoMapper;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetAllServices
{
    internal class GetAllServicesQueryHandler(IServiceRequestRepository serviceRequestRepository, IMapper mapper) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceRequestDto>>
    {
        public async Task<IEnumerable<ServiceRequestDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var ServiceRequests = await serviceRequestRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ServiceRequestDto>>(ServiceRequests);
        }
    }
}
