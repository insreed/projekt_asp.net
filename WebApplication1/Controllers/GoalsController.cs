using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GoalsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Akcja wyświetlająca listę celów
        public async Task<IActionResult> Index()
        {
            // Pobierz zalogowanego użytkownika
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Pobierz cele tylko dla aktualnie zalogowanego użytkownika
            var goals = await _context.Goals.Where(g => g.UserId == user.Id).ToListAsync();
            return View(goals);
        }

        // Akcja dodawania celu
        public IActionResult Create()
        {
            return View(new WebApplication1.Models.Goals());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, GoalType, Mass, MassGoal, WeightGoal, UserId")] Goals goal)
        {
            if (ModelState.IsValid)
            {
                // Pobierz zalogowanego użytkownika
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                // Ustaw UserId na podstawie zalogowanego użytkownika
                goal.UserId = user.Id;

                _context.Add(goal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goal);
        }

        // Akcja edytowania celu
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, GoalType, Mass, MassGoal, WeightGoal, UserId")] Goals goal)
        {
            if (id != goal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalsExists(goal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(goal);
        }

        // Reszta akcji pozostaje bez zmian

        private bool GoalsExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }
    }
}
