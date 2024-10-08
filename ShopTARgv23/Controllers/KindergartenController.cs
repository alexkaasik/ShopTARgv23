using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.Kindergarten;
using ShopTARgv23.Models.RealEstates;
using ShopTARgv23.Models.Spaceships;

namespace ShopTARgv23.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARgv23Context _context;
        private readonly IKindergartenServices _kindergartenServices;

        public KindergartenController
            (
                ShopTARgv23Context context,
                IKindergartenServices kindergartenServices
            )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    Teacher = x.Teacher,
                    CreatedAt = x.CreatedAt
                });
            return View(result);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel Kindergarten = new();

            return View("CreateUpdate", Kindergarten);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm, IKindergartenServices _kindergartenServices)
        {
            var dto = new KindergartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                Teacher = vm.Teacher,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt               
            };

            var result = await _kindergartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
        

    }
}
