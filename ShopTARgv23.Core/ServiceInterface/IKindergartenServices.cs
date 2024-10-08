using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;

namespace ShopTARgv23.Core.ServiceInterface
{
    public class IKindergartenServices
    { 
        Task<Kindergarten> Create(KindergartenDto dto);
    }
}
