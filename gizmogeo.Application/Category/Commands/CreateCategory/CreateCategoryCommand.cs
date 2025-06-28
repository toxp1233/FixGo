using gizmogeo.Application.Category.Dtos;
using MediatR;

namespace gizmogeo.Application.Category.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;

