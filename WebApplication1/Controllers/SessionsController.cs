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
using Microsoft.Extensions.Logging;
namespace WebApplication1.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        // Dodanie managerea użytkowników
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SessionsController> _logger;


        //Dodanie managera do kontrolera
        public SessionsController(ApplicationDbContext context, ILogger<SessionsController> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger; // Poprawna nazwa zmiennej
        }


        //GET: Sessions
        //public async Task<IActionResult> Index()
        //{
        //    IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
        //    return _context.Session != null ?
        //                  //Include(m => m.User)
        //                  //Where(m=>m.User == user)
        //                  View(await _context.Session.Where(m => m.User == user).Include(m => m.User).ToListAsync()) :
        //                  Problem("Entity set 'ApplicationDbContext.Session'  is null.");
        //}

        public async Task<IActionResult> Index()
        {
            // Pobierz zalogowanego użytkownika
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                // Pobierz sesje dla danego użytkownika
                var userSessions = await _context.Session
                    .Where(m => m.UserId == user.Id) // Zakładam, że istnieje kolumna UserId w tabeli Session
                    .Include(m => m.User)
                    .ToListAsync();

                return View(userSessions);   
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Session == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Start,End,SessionName")] Session session)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            session.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // GET: Sessions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Session == null)
        //    {
        //        return NotFound();
        //    }

        //    var session = await _context.Session.FindAsync(id);
        //    if (session == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(session);
        //}

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            // Odśwież sesję przed przekazaniem do widoku
            _context.Entry(session).Reload();

            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,SessionName,UserId")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ustaw UserId przed zapisaniem zmian w bazie danych
                    session.UserId = _userManager.GetUserId(User);

                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
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

            return View(session);
        }

        //public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,SessionName")] Session session)
        //{
        //    if (id != session.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Aktualizuj sesję
        //            _context.Update(session);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException ex)
        //        {
        //            // Obsługa błędów związanych z konkurencją
        //            // Tutaj możesz dodatkowo zalogować błąd, aby zrozumieć, co się dzieje
        //            _logger.LogError(ex, "Concurrency error occurred during session update.");

        //            if (!SessionExists(session.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(session);
        //}

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Session == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Session == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Session'  is null.");
            }
            var session = await _context.Session.FindAsync(id);
            if (session != null)
            {
                _context.Session.Remove(session);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
          return (_context.Session?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
