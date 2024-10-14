﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.ApplicationService.Services;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.Kindergartens;

namespace ShopTARgv23.Controllers
{
    public class KindergartensController : Controller
    {

        private readonly ShopTARgv23Context _context;
        private readonly IKindergartenServices _KindergartenServices;

        public KindergartensController
            (
                ShopTARgv23Context context,
                IKindergartenServices KindergartenServices
            )
        {
            _context = context;
            _KindergartenServices = KindergartenServices;
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

        [HttpGet]
        public IActionResult Create()
        {
            KindergartensCreateUpdateViewModel kindergarten = new();

            return View("CreateUpdate", kindergarten);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(KindergartensCreateUpdateViewModel vm)
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

            var result = await _KindergartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
        

    }
}
