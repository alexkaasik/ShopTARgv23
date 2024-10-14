using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.ApplicationService.Services;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.RealEstates;
using ShopTARgv23.Models.Spaceships;

namespace ShopTARgv23.Controllers
{
    public class RealEstatesController : Controller
    {
        /*
         * Tehe Index vaade
         * Rakendus peab naitema index vaadet koos andmetega
         * vreate meetotit ei tee valmis
         * Andmed sisestate andmebaasi käsitsi
        */

        private readonly ShopTARgv23Context _context;
        private readonly IRealEstateServices _RealEstateServices;

        public RealEstatesController
            (
                ShopTARgv23Context context,
                IRealEstateServices RealEstateServices
            )
        {
            _context = context;
            _RealEstateServices = RealEstateServices;
        }

        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstatesIndexViewModel
                {
                    Id = x.Id,
                    Location = x.Location,
                    Size = x.Size,
                    RoomNumber = x.RoomNumber,
                    CreatedAt = x.CreatedAt
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            RealEstatesCreateUpdateViewModel RealEstate = new();

            return View("CreateUpdate", RealEstate);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RealEstatesCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Location = vm.Location,
                Size = vm.Size,
                RoomNumber = vm.RoomNumber,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _RealEstateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _RealEstateServices.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstatesDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Location = realEstate.Location;
            vm.Size = realEstate.Size;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;

            return View(vm);
        }
    }
}
