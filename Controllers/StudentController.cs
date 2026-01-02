using internship1.Data;
using internship1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace internship1.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            // redirection vendi logged in allel.
            if (HttpContext.Session.GetString("admin") == null)
                return RedirectToAction("Login", "Admin");

            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(s =>
                    s.Name.Contains(search) ||
                    s.Email.Contains(search) ||
                    s.Course.Contains(search)
                );
            }



            // Total students idkaan
            ViewBag.TotalStudents = _context.Students.Count();

            // course vech groupeyan (MCA, BCA, etc.)
            var courseCounts = _context.Students
                .GroupBy(s => s.Course)
                .Select(g => new { Course = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.CourseCounts = courseCounts;


            return View(students.ToList());
        }


        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student) {
            if (ModelState.IsValid) { 
                _context.Students.Add(student);
                _context.SaveChanges();
                TempData["success"] = "Student added successfully!";
                return RedirectToAction("Index");

}
            TempData["error"] = "Failed to add student.";
            return View();
        
        
        
        
        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student) {
            if (ModelState.IsValid) {
                _context.Students.Update(student);
                _context.SaveChanges();
                TempData["success"] = "Student updated successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Failed to update student.";
            return View(student);
        
        
        
        
        }

        public IActionResult Delete(int Id) {
            var student = _context.Students.Find(Id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            TempData["success"] = "Student deleted successfully!";
            return RedirectToAction("Index");

        }
        public IActionResult Export()
        {
            var students = _context.Students.ToList();

            var csv = "Id,Name,Email,Course\n";

            foreach (var s in students)
            {
                csv += $"{s.Id},{s.Name},{s.Email},{s.Course}\n";
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "students.csv");
        }






    }
}
