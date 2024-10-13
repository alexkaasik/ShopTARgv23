using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Data;
using ShopTARgv23.Models.Kindergartens;

namespace ShopTARgv23.Controllers
{
    public class KindergartensController : Controller
    {

        private readonly ShopTARgv23Context _context;

        public KindergartensController
            (
                ShopTARgv23Context context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartensIndexViewModel
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
    }
}
