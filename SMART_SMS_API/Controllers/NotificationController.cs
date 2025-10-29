using Microsoft.AspNetCore.Mvc;

namespace SMART_SMS_API.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
