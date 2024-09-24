using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTARgv23.Data;
using ShopTARgv23.Core.Dto;

namespace ShopTARgv23.ApplicationService.Services
{
    internal class SpaceshipsService
    {
        private readonly SpaceshipsService _context;

        public SpaceshipsService(SpaceshipsService context)
        {
            _context = context;
        }

        public async Task<Spaceship>DetailsAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Spaceship> Update(SpaceshipDto dto)
        {
            Spaceship domain = new();

            domain.Id = dto.Id;
            domain.Name = dto.Name;
            domain.Type = dto.Type;
            domain.BuiltDate = dto.BuiltDate;
            domain.CargoWeight = dto.CargoWeight;
            domain.Crew = dto.Crew;
            domain.EnginePower = dto.EnginePower;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            _context.Spaceships.Update(domain);
            await _context.SaveChangeAsync();

            return domain;
        }
        public async Task<Spaceship> Delete(Grud id)
        {
            var spaceship = await _context.Spaceships
                .FirstOrDefaultAsync(x =>x.Id == id);

            _context.Spaceships.Remove(spaceship);
            await _context.SaveChangeAsync();

            return spaceship;
        }
    }
}
