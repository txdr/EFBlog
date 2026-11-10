using BlogWithEf2.Data;
using BlogWithEf2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWithEf2.Controllers
{
    public class PostController : Controller
    {

        private readonly BlogDbContext _db;

        public PostController(BlogDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var posts = await _db.Posts.OrderByDescending(p => p.CreatedUTC).ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if (post is null) return NotFound();
            return View(post);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid) return View(post);

            post.CreatedUTC = DateTime.UtcNow;
            _db.Add(post);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if (post is null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post form)
        {
            if (id != form.Id) return BadRequest();
            if (!ModelState.IsValid) return View(form);

            try
            {
                _db.Entry(form).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Posts.AnyAsync(p => p.Id == id)) return NotFound();
                throw;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if (post is null) return NotFound();
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if (post is not null)
            {
                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
