using gizmogeo.Application.Category.Dtos;
using MediatR;

namespace gizmogeo.Application.Category.Querys.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
