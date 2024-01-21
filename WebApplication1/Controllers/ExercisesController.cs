using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExercisesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exercise.Include(e => e.ExerciseType).Include(e => e.Session);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .Include(e => e.ExerciseType)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            // Pobierz aktualnie zalogowanego użytkownika synchronicznie
            var user = Task.Run(() => _userManager.FindByNameAsync(User.Identity.Name)).Result;

            // Pobierz sesje dla danego użytkownika
            var userSessions = _context.Session
                .Where(s => s.UserId == user.Id)
                .ToList();

            // Ustaw listę rozwijaną SessionId tylko na sesje aktualnie zalogowanego użytkownika
            ViewData["SessionId"] = new SelectList(userSessions, "Id", "Start");

            // Ustaw listę rozwijaną ExerciseTypeId
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Name");

            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,Reps,Series,SessionId,ExerciseTypeId")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id", exercise.ExerciseTypeId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", exercise.SessionId);
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id", exercise.ExerciseTypeId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", exercise.SessionId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Reps,Series,SessionId,ExerciseTypeId")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id", exercise.ExerciseTypeId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", exercise.SessionId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .Include(e => e.ExerciseType)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exercise == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Exercise'  is null.");
            }
            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercise.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return (_context.Exercise?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
