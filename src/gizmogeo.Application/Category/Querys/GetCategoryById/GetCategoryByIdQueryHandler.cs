using AutoMapper;
using gizmogeo.Application.Category.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Category.Querys.GetCategoryById;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(gizmogeo.Domain.Entities.Category), request.Id.ToString());
        return mapper.Map<CategoryDto>(category);
    }
}
