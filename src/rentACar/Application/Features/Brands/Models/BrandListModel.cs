using Application.Features.Brands.Dtos;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Brands.Models;

public class BrandListModel : BasePageableModel, IRequest<Unit>
{
    public IList<BrandListDto> Items { get; set; }
}