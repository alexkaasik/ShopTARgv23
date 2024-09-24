using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceship> DetailsAsync(Guid id);

        Task<Spaceship> Update(SpaceshipDto dto);
    }
}
