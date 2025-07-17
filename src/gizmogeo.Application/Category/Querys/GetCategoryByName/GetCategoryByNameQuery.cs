using gizmogeo.Application.Category.Dtos;
using MediatR;

namespace gizmogeo.Application.Category.Querys.GetCategoryByName;

public record GetCategoryByNameQuery(string Name) : IRequest<CategoryDto>;
