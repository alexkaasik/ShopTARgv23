using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Data;
using ShopTARgv23.Models;
using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.Spaceships;
using ShopTARgv23.Core.Domain;

namespace ShopTARgv23.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARgv23Context _context;
        private readonly ISpaceshipServices _spaceshipService;

        public SpaceshipsController
            (
                ShopTARgv23Context context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result =  _context.Spaceships
                .Select(x => new SpaceshipsIndexViewModel
                {
                   Id = x.Id,
                   Name = x.Name,
                   Type = x.Type,
                   BuiltDate = x.BuiltDate,
                   EnginePower = x.EnginePower
                });
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipService.DetailsAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;

            return View("CreateUpdate", vm);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipService.DetailsAsync();


            if (spaceship == null)
            {
                return NotFound();
            }
            var vm = new SpaceshipDetailsViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipService.DetailsAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDetailsViewModel();
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto();

            dto.Id = vm.Id;
            dto.Name = vm.Name;
            dto.Type = vm.Type;
            dto.BuiltDate = vm.BuiltDate;
            dto.CargoWeight = vm.CargoWeight;
            dto.Crew = vm.Crew;
            dto.EnginePower = vm.EnginePower;
            dto.CreateAt = vm.CreatedAt;
            dto.ModifiedAt = vm.ModifiedAt;

            var result = await _spaceshipServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipService.DetailsAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            // Peate tegema Delete view modeli ja siin ära mappima spaceship muutujuga

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceship = await _spaceshipService.Delete(id);

            if (spaceship == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
