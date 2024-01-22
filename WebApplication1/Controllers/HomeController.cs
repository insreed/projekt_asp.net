using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Jeśli użytkownik jest zalogowany, pobierz dane związane z sesjami i ćwiczeniami
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var sessions = _context.Session.Where(s => s.UserId == userId).ToList();
                var exercises = _context.Exercise
                    .Include(e => e.ExerciseType)
                    .Where(e => sessions.Select(s => s.Id).Contains(e.SessionId))
                    .ToList();

                var viewModel = new HomeIndexViewModel
                {
                    Sessions = sessions,
                    Exercises = exercises
                };

                return View(viewModel);
            }
            else
            {
                // Jeśli użytkownik nie jest zalogowany, zwróć widok bez danych treningowych
                return View();
            }
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
