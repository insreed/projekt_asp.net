using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StatisticsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            var applicationDbContext = _context.Exercise
                .Include(e => e.ExerciseType)
                .Include(e => e.Session)
                .Where(e => e.Session.User.Id == user.Id && e.Session.User.UserName == user.UserName)
                .Where(e => e.Session.Start > DateTime.Now.AddDays(-28))
                .GroupBy(x => new { x.ExerciseType.Name, x.Session.Start.Date })
                .Select(g => new Statistics
                {
                    ExcerciseType = g.Key.Name,
                    BestStat = g.Max(e => e.Weight),
                    SessionCount = g.Count(),
                    StartDate = g.Key.Date
                })
                .OrderBy(z => z.ExcerciseType)
                .ToListAsync();

            return View(await applicationDbContext);

        }
    }

}


