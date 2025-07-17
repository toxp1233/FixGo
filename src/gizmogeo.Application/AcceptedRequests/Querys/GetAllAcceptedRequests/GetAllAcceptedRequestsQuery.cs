using gizmogeo.Application.AcceptedRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Querys.GetAllUsers;

public record GetAllAcceptedRequestsQuery : IRequest<IEnumerable<AcceptedRequestByUserDto>>;

