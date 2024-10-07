using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.RealEstates;

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

        public RealEstatesController
            (
                ShopTARgv23Context context
            )
        {
            _context = context;
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
    }
}
