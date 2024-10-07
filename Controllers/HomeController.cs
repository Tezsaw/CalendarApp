using CalendarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CalendarApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? y, m;

            try
            {
                y = Convert.ToInt32(Request.Query["y"]);
            } catch (FormatException) { y = null; }
            catch (OverflowException) { y = null; }

            try
            {
                m = Convert.ToInt32(Request.Query["m"]);
            }
            catch (FormatException) { m = null; }
            catch (OverflowException) { m = null; }

            return View(new CalendarViewInfo {
                Activities = _context.Activities.ToList(),
                Year = y,
                Month = m
            });
        }

        public IActionResult FullList()
        {
            return View(_context.Activities.ToList());
        }

        public IActionResult ErrorDelete()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Activities.Find(id);
            if (item == null)
            {
                return RedirectToAction("ErrorDelete");
            }
            return View(item);
        }

        [HttpPost(Name = "Delete")]
        public IActionResult Delete(int id, CalendarActivity item)
        {
            if (id == item.Id)
            {
                _context.Activities.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("FullList");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost(Name = "Create")]
        public IActionResult Create(string? Name, DateTime Date)
        {
            _context.Activities.Add(new CalendarActivity { Name = Name, Date = Date});
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}