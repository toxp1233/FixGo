using gizmogeo.Application.common;
using gizmogeo.Application.ServiceRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetAllServices;

public record GetAllServicesQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<ServiceRequestDto>>;
