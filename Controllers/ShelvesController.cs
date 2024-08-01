using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Library.DAL;
using My_Library.Models;
using My_Library.Views.Shelves.ViewModels;

namespace My_Library.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly DataContext _context;

        public ShelvesController(DataContext context)
        {
            _context = context;
        }

        // GET: Shelves
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Shelves.Include(s => s.Books).Include(s => s.Library);
            return View(await dataContext.ToListAsync());
        }

        // GET: Shelves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelves
                .Include(s => s.Library)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // GET: Shelves/Create
        public IActionResult Create()
        {
            ViewData["LibraryId"] = new SelectList(_context.Libraries, "Id", "Name");

            var isLibraryExist = _context.Libraries.Count();
            CreateViewModel model = new CreateViewModel
            {
                ErrorModel = isLibraryExist == 0 ? new ErrorModel { Message = "יש ליצור קודם ספריה" } : null,
                Libraries = _context.Libraries.Include(l => l.ShelfList).ToList(),
            };
            return View(model);
        }

        // POST: Shelves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Height,LibraryId")] Shelf shelf)
        {
            if (shelf.LibraryId > 0)
            {
                var res = await _context.Libraries.FirstOrDefaultAsync(g => g.Id == shelf.LibraryId);
                if (res != null)
                {
                    shelf.Library = res;
                }
            }
            ModelState.Remove("shelf.Library");
            if (ModelState.IsValid && shelf.Library != null)
            {
                _context.Add(shelf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibraryId"] = new SelectList(_context.Libraries, "Id", "Name", shelf.LibraryId);
            var isGenreExist = _context.Libraries.Count();
            CreateViewModel model = new CreateViewModel
            {
                ErrorModel = isGenreExist == 0 ? new ErrorModel { Message = "יש ליצור קודם ספריה" } : null,
                Shelf = shelf,
                Libraries = _context.Libraries.ToList()
            };
            return View(model);
        }

        // GET: Shelves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }
            ViewData["LibraryId"] = new SelectList(_context.Libraries, "Id", "Id", shelf.LibraryId);
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Height,LibraryId")] Shelf shelf)
        {
            if (id != shelf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelfExists(shelf.Id))
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
            ViewData["LibraryId"] = new SelectList(_context.Libraries, "Id", "Id", shelf.LibraryId);
            return View(shelf);
        }

        // GET: Shelves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelf = await _context.Shelves
                .Include(s => s.Library)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // POST: Shelves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf != null)
            {
                _context.Shelves.Remove(shelf);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelfExists(int id)
        {
            return _context.Shelves.Any(e => e.Id == id);
        }
    }
}
