using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> Create(RealEstateDto dto);

        Task<RealEstate> DetailsAsync(Guid id);

        Task<RealEstate> Update(RealEstateDto dto);

        Task<RealEstate> Delete(Guid dto);
    }
}
