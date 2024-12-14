using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestStore.Data;
using TestStore.Models;

namespace TestStore.Controllers
{
    public class itemsController : Controller
    {
        private readonly TestStoreContext _context;

        public itemsController(TestStoreContext context)
        {
            _context = context;
        }

        // GET: items
        public async Task<IActionResult> Index()
        {
            ViewData["role"] = HttpContext.Session.GetString("Role");
            return _context.items != null ?
                          View(await _context.items.ToListAsync()) :
                          Problem("Entity set 'TestStoreContext.items'  is null.");
        }

        // GET: items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["role"] = HttpContext.Session.GetString("Role");

            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }


        // GET: items/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,name,description,price,discount,pubdate,category,quantity,imgfile")] items items)
        {
            {
                if (file != null)
                {
                    string filename = file.FileName;
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    { await file.CopyToAsync(filestream); }

                    items.imgfile = filename;
                }
                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var items = await _context.items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }





        // POST: items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("Id,name,description,price,discount,pubdate,category,quantity,imgfile")] items items)
        {
            if (id != items.Id)
            { return NotFound(); }

            if (file != null)
            {
                string filename = file.FileName;
                // string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                items.imgfile = filename;
            }
            _context.Update(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DashBoard()
        {
            {
                string sql = "";

                var builder = WebApplication.CreateBuilder();
                string conStr = builder.Configuration.GetConnectionString("TestStoreContext");
                SqlConnection conn = new SqlConnection(conStr);

                SqlCommand comm;
                conn.Open();
                sql = "SELECT COUNT( Id) FROM Items where Category =1";
                comm = new SqlCommand(sql, conn);
                ViewData["d1"] = (int)comm.ExecuteScalar();
                sql = "SELECT COUNT( Id) FROM Items where Category =2";
                comm = new SqlCommand(sql, conn);
                ViewData["d2"] = (int)comm.ExecuteScalar();

                return View();
            }
        }
        public async Task<IActionResult> List()
        {
            ViewData["role"] = HttpContext.Session.GetString("Role");


            var items = await _context.items.OrderBy(item => item.category).ToListAsync();
            return View(items);

        }


        // GET: items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.items == null)
            {
                return Problem("Entity set 'TestStoreContext.items'  is null.");
            }
            var items = await _context.items.FindAsync(id);
            if (items != null)
            {
                _context.items.Remove(items);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool itemsExists(int id)
        {
            return _context.items.Any(e => e.Id == id);
        }
    }
}
