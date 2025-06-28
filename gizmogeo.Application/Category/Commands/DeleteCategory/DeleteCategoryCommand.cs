using MediatR;

namespace gizmogeo.Application.Category.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public int Id { get; set; }  
}
