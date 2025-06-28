using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository
    ) 
    :IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(gizmogeo.Domain.Entities.Category), request.Id.ToString());

        await categoryRepository.DeleteAsync(category);
    }
}
