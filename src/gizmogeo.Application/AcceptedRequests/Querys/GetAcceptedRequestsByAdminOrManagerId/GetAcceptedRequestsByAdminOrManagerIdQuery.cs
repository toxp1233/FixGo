using gizmogeo.Application.AcceptedRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Querys.GetAcceptedRequestsByAdminOrManagerId;

public record GetAcceptedRequestsByAdminOrManagerIdQuery(Guid Id) : IRequest<IEnumerable<AcceptedRequestByUserDto>>;
