using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Library.DAL;
using My_Library.Models;
using My_Library.Views.Books.ViewModels;

namespace My_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _context.Books.Include(b => b.Set).Include(b => b.Shelf).ToListAsync();
                return View(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
           
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Set)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            
            return View(GetCreateViewModel());
        }

        private CreateViewModel GetCreateViewModel(Book? book = null)
        {
            ViewData["genreId"] = new SelectList(_context.Genres, "Id", "Name");
            string message = string.Empty;
            if (!_context.Genres.Any()) message = "יש ליצור קודם ז'אנרים ומדפים";
            else if (!_context.Shelves.Any()) message = "יש ליצור קודם מדפים";
            var res = new CreateViewModel
            {
                ErrorModel = string.IsNullOrEmpty(message) ? new ErrorModel { Message = message } : null,
                Shelves = _context.Shelves.Include(s => s.Books).Include(s => s.Library.Genre).ToList(),
                Sets = _context.Sets.Include(s => s.Books).ToList(),
                Book = book ?? null
            };
            return res;
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Width,Height,ShelfId, SetId")] Book book)
        {
            if (book.ShelfId > 0) book.Shelf = await _context.Shelves.FirstOrDefaultAsync(s => s.Id == book.ShelfId);
            if (book.SetId > 0) book.Set = await _context.Sets.FirstOrDefaultAsync(s => s.Id == book.SetId);

            ModelState.Remove("book.Shelf");
            if (ModelState.IsValid && book.Shelf != null)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(GetCreateViewModel(book));
        }

        public async Task<JsonResult> GetShelvesBySet(int setId)
        {
            List<Shelf> shelves = new List<Shelf>();
            Set shelfTemp = _context.Sets.Include(s => s.Books).FirstOrDefault(s => s.Id == setId);
            if (shelfTemp != null)
            {
                if (shelfTemp.Books.Count > 0)
                {
                    shelves = await _context.Shelves.Where(s => s.Id == shelfTemp.Books[0].Id).ToListAsync();
                }
                else
                {
                    shelves = await _context.Shelves.Include(s => s.Library).Where(s => s.Library.GenreId == shelfTemp.GenreId).ToListAsync();
                }
            }
            return Json(shelves);
        }
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["SetId"] = new SelectList(_context.Sets, "Id", "Id", book.SetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelves, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Width,Height,ShelfId,SetId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["SetId"] = new SelectList(_context.Sets, "Id", "Id", book.SetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelves, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Set)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
