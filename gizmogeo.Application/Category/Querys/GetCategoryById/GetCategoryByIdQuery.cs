using gizmogeo.Application.Category.Dtos;
using MediatR;

namespace gizmogeo.Application.Category.Querys.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>;
