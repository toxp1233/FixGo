using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Commands.DeleteAcceptedRequest;

public record DeleteAcceptedRequestCommand(Guid Id) : IRequest;

