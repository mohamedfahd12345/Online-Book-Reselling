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
    public class cartController : Controller
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
            
        }
        public IActionResult Index()
        {
            checkuser();
            List<Book> cart = HttpContext.Session.GetJson<List<Book>>("Book") ?? new List<Book>();
            return View(cart);
            
        }
        public IActionResult add(int id)
        {
            checkuser();
            List<Book> cart = HttpContext.Session.GetJson<List<Book>>("Book") ?? new List<Book>();
            var cart_item= cart.Where(x => x.Id == id).FirstOrDefault();

            ResellingContext db = new ResellingContext();
            var target_book=db.Books.Where(x => x.Id == id).FirstOrDefault();

            if (cart_item == null)
            {
                cart.Add(target_book);
            }
            HttpContext.Session.SetJson("Book", cart);
            return RedirectToAction("Index");
        }
        public IActionResult delete(int id)
        {
            checkuser();
            List<Book> cart = HttpContext.Session.GetJson<List<Book>>("Book") ?? new List<Book>();
            var cart_item = cart.Where(x => x.Id == id).FirstOrDefault();
            cart.Remove(cart_item);
            HttpContext.Session.SetJson("Book", cart);
            return RedirectToAction("Index");
        }
        public IActionResult clear()
        {
            checkuser();
            HttpContext.Session.Remove("Book");
            return RedirectToAction("Index");
        }
    }
}
