using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reselling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Reselling.Controllers
{
    [Authorize]
    public class HomeController : Controller
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

        public IActionResult sell()
        {
            checkuser();

            return View();
        }
        [HttpPost]
        public IActionResult sell(Book book)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            ResellingContext db = new ResellingContext();
            var new_book = new Book();

            new_book.Category = book.Category;
            new_book.UserId = userId;
            new_book.Pages = book.Pages;
            new_book.Photo = book.Photo;
            new_book.Author = book.Author;
            new_book.Descripition = book.Descripition;
            new_book.Year = book.Year;
            new_book.Title = book.Title;
            new_book.Language = book.Language;
            new_book.Price = book.Price;
            new_book.Status = "available";
            db.Books.Add(new_book);
            db.SaveChanges();
            return Redirect("/home/sell");
        }

        
    }
}