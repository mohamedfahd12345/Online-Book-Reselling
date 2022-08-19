using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reselling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Reselling.Infrastructure;

namespace Reselling.Controllers
{
    [Authorize]
    public class myordersController : Controller
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
            var target_order = db.Orderes.Where(x => x.UserId == userId).ToList();

            return View(target_order);
        }
        public IActionResult details(int id)
        {
            checkuser();
            ResellingContext db = new ResellingContext();
            var list_orders_details = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            var list_books = new List<Book>();
            foreach(var item in list_orders_details)
            {
                var target_book = db.Books.Where(x => x.Id == item.BookId).FirstOrDefault();
                list_books.Add(target_book);
            }
            return View(list_books);
        }
    }
}
