using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reselling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Reselling.Controllers
{
    [Authorize]
    public class mybookController : Controller
    {
        public void checkuser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ResellingContext db = new ResellingContext();
            var item = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (item == null)
            {
                var user = new User();
                user.Id = userId;
                user.Email = userName;
                db.Users.Add(user);
                db.SaveChanges();
            }
            ////////////////////////////////////////////////////////////////////////
        }
        public IActionResult Index()
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ResellingContext db = new ResellingContext();
           var my_books=  db.Books.Where(x => x.UserId == userId).ToList();

            return View(my_books);
        }
        public IActionResult details(int id)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ResellingContext db = new ResellingContext();

            var target_book = db.Books.Where(x => x.Id == id).FirstOrDefault();
            Console.WriteLine(target_book.Photo);
            return View(target_book);
        }
        public IActionResult edit(int id)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ResellingContext db = new ResellingContext();
            var target_book = db.Books.Where(x => x.Id == id).FirstOrDefault();

            return View(target_book);
        }
        [HttpPost]
        public IActionResult edit(Book book)
        {
            ResellingContext db = new ResellingContext();
            var new_book = db.Books.Where(x => x.Id == book.Id).FirstOrDefault();
            new_book.Category = book.Category;
            new_book.Pages = book.Pages;
            new_book.Photo = book.Photo;
            new_book.Author = book.Author;
            new_book.Descripition = book.Descripition;
            new_book.Year = book.Year;
            new_book.Title = book.Title;
            new_book.Language = book.Language;
            new_book.Price = book.Price;
            db.Update(new_book);
            db.SaveChanges();
            return Redirect("/mybook/details/" + book.Id);
        }
        public IActionResult delete(int id)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ResellingContext db = new ResellingContext();
            var target_book = db.Books.Where(x => x.Id == id).FirstOrDefault();
            db.Books.Remove(target_book);
            db.SaveChanges();
            return Redirect("/mybook/index");
        }
        public IActionResult available_books()
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user
            ResellingContext db = new ResellingContext();
            var list_books = db.Books.Where(x => x.UserId == userId && x.Status == "available").ToList();
            return View(list_books);

        }
        public IActionResult sold_books()
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user
            ResellingContext db = new ResellingContext();
            var list_books = db.Books.Where(x => x.UserId == userId && x.Status == "sold").ToList();
            return View(list_books);
        }
    }
}
