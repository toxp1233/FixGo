using MediatR;

namespace gizmogeo.Application.ServiceRequests.Command.DeleteService;

public record DeleteServiceCommand(Guid Id) : IRequest;

