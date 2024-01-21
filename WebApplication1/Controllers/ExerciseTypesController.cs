using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ExerciseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExerciseTypesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ExerciseTypes
        public async Task<IActionResult> Index()
        {
            if (_context.ExerciseType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExerciseType' is null.");
            }

            // Pobierz zalogowanego użytkownika
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Pobierz ćwiczenia tylko dla bieżącego użytkownika
            var userExerciseTypes = await _context.ExerciseType
                .Where(et => et.UserId == user.Id)
                .ToListAsync();

            return View(userExerciseTypes);
        }

        // GET: ExerciseTypes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExerciseType == null)
            {
                return NotFound();
            }

            var exerciseType = await _context.ExerciseType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            return View(exerciseType);
        }

        // GET: ExerciseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerciseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExerciseType exerciseType)
        {
            if (ModelState.IsValid)
            {
                // Pobierz zalogowanego użytkownika
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                // Ustaw UserId na podstawie zalogowanego użytkownika
                exerciseType.UserId = user.Id;

                _context.Add(exerciseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerciseType);
        }

        // GET: ExerciseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExerciseType == null)
            {
                return NotFound();
            }

            var exerciseType = await _context.ExerciseType.FindAsync(id);
            if (exerciseType == null)
            {
                return NotFound();
            }
            return View(exerciseType);
        }

        // POST: ExerciseTypes/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] ExerciseType exerciseType)
        {
            if (id != exerciseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ustaw UserId na podstawie zalogowanego użytkownika
                    IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    exerciseType.UserId = user.Id;

                    _context.Update(exerciseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseTypeExists(exerciseType.Id))
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
            return View(exerciseType);
        }

        // GET: ExerciseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExerciseType == null)
            {
                return NotFound();
            }

            var exerciseType = await _context.ExerciseType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            return View(exerciseType);
        }

        // POST: ExerciseTypes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExerciseType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExerciseType'  is null.");
            }
            var exerciseType = await _context.ExerciseType.FindAsync(id);
            if (exerciseType != null)
            {
                _context.ExerciseType.Remove(exerciseType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseTypeExists(int id)
        {
          return (_context.ExerciseType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
