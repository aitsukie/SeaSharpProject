using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeaSharpProject.Data;
using SeaSharpProject.Models;

namespace SeaSharpProject.Controllers
{
    [Authorize]
    public class SeaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sea
        public async Task<IActionResult> Index()
        {
              return View(await _context.SeaEntity.ToListAsync());
        }

        // GET: Sea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SeaEntity == null)
            {
                return NotFound();
            }

            var seaEntity = await _context.SeaEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seaEntity == null)
            {
                return NotFound();
            }

            return View(seaEntity);
        }

        // GET: Sea/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,UserName")] SeaEntity seaEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seaEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seaEntity);
        }

        // GET: Sea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SeaEntity == null)
            {
                return NotFound();
            }

            var seaEntity = await _context.SeaEntity.FindAsync(id);
            if (seaEntity == null)
            {
                return NotFound();
            }
            return View(seaEntity);
        }

        // POST: Sea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,UserName")] SeaEntity seaEntity)
        {
            if (id != seaEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seaEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeaEntityExists(seaEntity.Id))
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
            return View(seaEntity);
        }

        // GET: Sea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SeaEntity == null)
            {
                return NotFound();
            }

            var seaEntity = await _context.SeaEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seaEntity == null)
            {
                return NotFound();
            }

            return View(seaEntity);
        }

        // POST: Sea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SeaEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SeaEntity'  is null.");
            }
            var seaEntity = await _context.SeaEntity.FindAsync(id);
            if (seaEntity != null)
            {
                _context.SeaEntity.Remove(seaEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeaEntityExists(int id)
        {
          return _context.SeaEntity.Any(e => e.Id == id);
        }
    }
}
