using Microsoft.AspNetCore.Mvc;
using eBookLoginHW1.Data;

namespace eBookLoginHW1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult TestDb()
        {
            try
            {
                // מנסה לקרוא את רשימת המשתמשים
                var usersCount = _context.Users.Count();

                return Content($"✅ Database is connected! Users count: {usersCount}");
            }
            catch (Exception ex)
            {
                return Content($"❌ Error: {ex.Message}");
            }
        }
    }
}
