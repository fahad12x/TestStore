﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestStore.Data;
using TestStore.Models;

namespace TeleStore.Controllers
{
    public class usersaccountsController : Controller
    {
        private readonly TestStoreContext _context;

        public usersaccountsController(TestStoreContext context)
        {
            _context = context;
        }

        // GET: usersaccounts
        public async Task<IActionResult> Index()
        {
              return _context.usersaccounts != null ? 
                          View(await _context.usersaccounts.ToListAsync()) :
                          Problem("Entity set 'TestStoreContext.usersaccounts'  is null.");
        }

        // GET: usersaccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usersaccounts == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        // GET: usersaccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usersaccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,role")] usersaccounts usersaccounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersaccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersaccounts);
        }

        // GET: usersaccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usersaccounts == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            if (usersaccounts == null)
            {
                return NotFound();
            }
            return View(usersaccounts);
        }

        // POST: usersaccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role")] usersaccounts usersaccounts)
        {
            if (id != usersaccounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersaccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usersaccountsExists(usersaccounts.Id))
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
            return View(usersaccounts);
        }

        // GET: usersaccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usersaccounts == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        // POST: usersaccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usersaccounts == null)
            {
                return Problem("Entity set 'TestStoreContext.usersaccounts'  is null.");
            }
            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            if (usersaccounts != null)
            {
                _context.usersaccounts.Remove(usersaccounts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usersaccountsExists(int id)
        {
          return (_context.usersaccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // Action Logout Button
        public IActionResult Logout()

        {
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Role");

            return RedirectToAction("Login", "usersaccounts");

        }
        public IActionResult registration()

        {

            return View();

        }
        [HttpPost]

        public IActionResult registration(string Name, string Password, string ConfirmPassword, string Job, string Email, bool Married, string Gender, string Location)
        {
            if (_context.usersaccounts.Any(u => u.name == Name))
            {
                ViewData["Message"] = "Username already exists. Enter Another Customer customer name.";
                return View();
            }
            else
            {
                usersaccounts userModel = new usersaccounts
                {
                    name = Name,
                    pass = Password,
                    role = "customer"
                };

                _context.usersaccounts.Add(userModel);
                _context.SaveChanges();
                SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobilesStoreDB;Integrated Security=True;Pooling=False");
                conn.Open();
                string sql;
                sql = "INSERT INTO customer (name, email, job, married, gender, location)  values  ('" + Name + "','" + Email + "','" + Job + "','" + Married + "' ,'" + Gender + "' ,'" + Location + "')";

                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                return RedirectToAction(nameof(Login));


            }









        }








        public IActionResult Login()

        {
            if (!HttpContext.Request.Cookies.ContainsKey("Name"))
                return View();
            else
            {
                string na = HttpContext.Request.Cookies["Name"].ToString();
                string ro = HttpContext.Request.Cookies["Role"].ToString();
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", ro);
                return View();


            }
        }
        [HttpPost, ActionName("login")]
        public async Task<IActionResult> login(string na, string pa, string auto)
        {
            var ur = await _context.usersaccounts.FromSqlRaw("SELECT * FROM usersaccounts where name = '" + na + "' and pass = '" + pa + "' ").FirstOrDefaultAsync();
            if (ur != null)
            {
                int id = ur.Id;
                string na1 = ur.name;
                string ro = ur.role;
                HttpContext.Session.SetString("userid", Convert.ToString(id));
                HttpContext.Session.SetString("Name", na1);
                HttpContext.Session.SetString("Role", ro);
                if (auto == "on")
                {
                    HttpContext.Response.Cookies.Append("Name", na1);
                    HttpContext.Response.Cookies.Append("Role", ro);
                }
                if (ro == "customer")
                    return RedirectToAction("customerhome", "usersaccounts");
                else if (ro == "admin")
                    return RedirectToAction("adminhome", "usersaccounts");
                else
                    return View();
            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }
        // GET: adminhome
        public async Task<IActionResult> adminhome()
        {
            HttpContext.Session.LoadAsync();
            string ss = HttpContext.Session.GetString("Role");
            if (ss == "admin")
            {
                ViewData["name"] = HttpContext.Session.GetString("Name");
                ViewData["role"] = HttpContext.Session.GetString("Role");


                return View();
            }
            else
                return RedirectToAction("login", "usersaccount");
        }
        //cushome
        public async Task<IActionResult> customerhome()
        {
            HttpContext.Session.LoadAsync();
            string ss = HttpContext.Session.GetString("Role");

            if (ss == "customer")
            {
                ViewData["name"] = HttpContext.Session.GetString("Name");
                ViewData["role"] = HttpContext.Session.GetString("Role");

                var discount = await _context.items
                    .Where(b => b.discount == "yes")
                    .ToListAsync();
                return View(discount);
            }
            else
                return RedirectToAction("login", "usersaccount");
        }



        /// Email  
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost, ActionName("SendEmail")]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmail(string address, string subject, string body)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("toto60655577@gmail.com");
            mail.To.Add(address); // receiver email address 
            mail.Subject = "HELLO to Deema Store";
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("toto60655577@gmail.com", "jfglmgfvpzyihkgh");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            ViewData["Message"] = "Email sent !!";
            return View();
        }





        /// Search for Customer
        public async Task<IActionResult> users_search()
        {
            {
                usersaccounts custs = new usersaccounts();
                return View(custs);
            }
        }
        [HttpPost]
        public async Task<IActionResult> users_search(string name)
        {
            var uAcc = await _context.usersaccounts.FromSqlRaw("select * from usersaccounts where Name = '" + name + "' ").FirstOrDefaultAsync();

            return View(uAcc);
        }



        //addadmin
        public IActionResult AddAdmin()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAdmin(string UserName, string Password, string ConfirmPassword)
        {



            if (Password != ConfirmPassword)
            {
                ViewData["Message"] = "Passwords do not match. Please try again.";
                return View();
            }

            if (_context.usersaccounts.Any(uname => uname.name == UserName))
            {
                ViewData["Message"] = "Admin Name already exists.....";
                return View();
            }
            usersaccounts userModel = new usersaccounts
            {
                name = UserName,
                pass = Password,
                role = "Admin",
            };
            _context.usersaccounts.Add(userModel);
            _context.SaveChanges();



            return RedirectToAction(nameof(Index));
        }















    }
}