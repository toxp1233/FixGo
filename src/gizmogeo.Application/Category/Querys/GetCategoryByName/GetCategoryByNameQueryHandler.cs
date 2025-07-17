using AutoMapper;
using gizmogeo.Application.Category.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Category.Querys.GetCategoryByName;

public class GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByNameQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByNameAsync(request.Name) ?? throw new NotFoundException(nameof(gizmogeo.Domain.Entities.Category), request.Name);
        return mapper.Map<CategoryDto>(category);
    }
}
