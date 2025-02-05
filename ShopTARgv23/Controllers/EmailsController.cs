using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.Emails;

namespace ShopTARgv23.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailsServices _emailsServuces;

        public EmailsController(IEmailsServices emailsServuces) { _emailsServuces = emailsServuces; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendEmail(EmailsViewModel vm)
        {
            var dto = new EmailDto()
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = vm.Body,
            };

            _emailsServuces.SendEmail(dto);

			return RedirectToAction(nameof(Index));
		}
    }
}
