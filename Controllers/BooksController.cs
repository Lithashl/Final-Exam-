using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysql_connect.Data;
using mysql_connect.Models;

namespace mysql_connect.Controllers
{
	public class BooksController : Controller
	{
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
		{
            _context = context;
        }
        // GET: Book
        public async Task<IActionResult> Index()
        {
            var _Book = await _context.Book.ToListAsync();
            if (_Book.Count < 1)
                await CreateTestData();
            return _context.Book != null ?
                        View(await _context.Book.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Book'  is null.");
        }
        // GET: Book/Details
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.no == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("no, judulbuku, pengarang, harga, penerbit, kategori, gambar")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("no, Author, pengarang, harga, penerbit, kategori, gambar")] Book book)
        {
            if (id != book.no)
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
                    if (!BookExists(book.no))
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
            return View(book);
        }
        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _Categories = await _context.Book.FindAsync(id);

                if (_Categories != null)
                {
                    _context.Book.Remove(_Categories);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(_Categories);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool BookExists(long id)
        {
            return (_context.Book?.Any(e => e.no == id)).GetValueOrDefault();
        }
        private async Task CreateTestData()
        {
            foreach (var item in GetBookList())
            {
                _context.Book.Add(item);
                await _context.SaveChangesAsync();
            }
        }
        private IEnumerable<Book> GetBookList()
        {
            return new List<Book>
            {
                new Book { judulbuku = "Janji", pengarang = "Tere Liye", penerbit = "Sabakgrip", kategori = "Novel Remaja", gambar = "" },
                new Book { judulbuku = "Rasa", pengarang = "Tere Liye", penerbit = "Republika", kategori = "Novel Remaja", gambar = "" },
                new Book { judulbuku = "Bahureksa", pengarang = "Dea", penerbit = "Sabakgrip", kategori = "Novel Remaja", gambar = "" },
                new Book { judulbuku = "Dear Nathan", pengarang = "Erisca Fevian", penerbit = "Republika", kategori = "Novel Remaja", gambar = "" },
               
            };
        }
    }
}

