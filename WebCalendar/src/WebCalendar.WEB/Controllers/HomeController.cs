using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCalendar.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}