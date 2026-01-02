using Microsoft.AspNetCore.Mvc;

namespace internship1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // admin credentials are hadrcoded, maatanota idh.
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("admin", "logged");
                return RedirectToAction("Index", "Student");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin");
            return RedirectToAction("Login");
        }
    }
}
