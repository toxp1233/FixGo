using AutoMapper;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Commands.PatchNotes;

public class PatchNotesCommandHandler(ICompletedOrderRepository completedOrderRepository, IMapper mapper) : IRequestHandler<PatchNotesCommand, CompletedOrderDto>
{
    public async Task<CompletedOrderDto> Handle(PatchNotesCommand request, CancellationToken cancellationToken)
    {
        var completedOrder = await completedOrderRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(CompletedOrder), request.Id.ToString());
        mapper.Map(request, completedOrder);
        await completedOrderRepository.UpdateAsync(completedOrder);
        return mapper.Map<CompletedOrderDto>(completedOrder);
    }
}
