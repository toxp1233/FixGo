using AutoMapper;
using gizmogeo.Application.Category.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper
) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
       
        var category = await categoryRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(gizmogeo.Domain.Entities.Category), request.Id.ToString());
        mapper.Map(request, category);

        var updatedCategory = await categoryRepository.UpdateAsync(category);

        var categoryDto = mapper.Map<CategoryDto>(updatedCategory);
        return categoryDto;
    }
}
