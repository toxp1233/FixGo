using AutoMapper;
using gizmogeo.Application.Category.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Category.Querys.GetAllCategories;

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var allCategories = await categoryRepository.GetAllAsync();
        var categoryDtos = mapper.Map<IEnumerable<CategoryDto>>(allCategories);
        return categoryDtos;
    }
}
