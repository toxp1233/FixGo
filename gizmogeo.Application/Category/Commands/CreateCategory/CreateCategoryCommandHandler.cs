using AutoMapper;
using gizmogeo.Application.Category.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {       
        var category = mapper.Map<gizmogeo.Domain.Entities.Category>(request);
        var newCategory = await categoryRepository.CreateAsync(category);
        return mapper.Map<CategoryDto>(newCategory);
    }
}
