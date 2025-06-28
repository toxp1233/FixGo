using gizmogeo.Application.AcceptedRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Querys.GetAcceptedRequestById;

public record GetAcceptedRequestByIdQuery(Guid Id) : IRequest<AcceptedRequestByUserDto>;

