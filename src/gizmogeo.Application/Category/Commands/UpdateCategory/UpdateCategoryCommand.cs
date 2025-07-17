using gizmogeo.Application.Category.Dtos;
using MediatR;

namespace gizmogeo.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
