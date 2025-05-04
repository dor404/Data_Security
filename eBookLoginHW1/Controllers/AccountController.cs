using Microsoft.AspNetCore.Mvc;
using eBookLoginHW1.Data;
using eBookLoginHW1.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eBookLoginHW1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Handle null or empty password (important for injection demo)
            string hash = string.IsNullOrEmpty(password) ? "" : ComputeSha256Hash(password);

            // ⚠️ Deliberately vulnerable query
            string sql = $"SELECT * FROM Users WHERE Username = '{username}' AND PasswordHash = '{hash}'";

            var user = _context.Users.FromSqlRaw(sql).FirstOrDefault();

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            if (user.Role == "admin")
                return RedirectToAction("AdminDashboard");
            else
                return RedirectToAction("UserDog");
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ViewBag.Message = "User not found.";
                return View();
            }

            return RedirectToAction("ResetPassword", new { username });
        }

        [HttpGet]
        public IActionResult ResetPassword(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string username, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.PasswordHash = ComputeSha256Hash(newPassword);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.Message = "User not found.";
            return View();
        }

        [HttpGet]
        public IActionResult UserDog()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            var allUsers = _context.Users.ToList();
            return View(allUsers);
        }

        private string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return string.Concat(bytes.Select(b => b.ToString("x2")));
        }
    }
}
