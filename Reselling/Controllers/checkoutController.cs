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
    public class checkoutController : Controller
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
            ResellingContext db = new ResellingContext();
            List<Book> cart = HttpContext.Session.GetJson<List<Book>>("Book") ?? new List<Book>();
            var books_sold = new List<Book>();
            foreach(var item in cart)
            {
                var target_book = db.Books.Where(x => x.Id == item.Id).FirstOrDefault();
				if (target_book.Status == "sold")
				{
                    books_sold.Add(item);
                    cart.Remove(item);
                }
                    
            }
            HttpContext.Session.SetJson("Book", cart);


            if (books_sold.Count > 0)
            {
                return View(books_sold);
            }
            return RedirectToAction("create");
        }
        public IActionResult create()
        {
            checkuser();

            return View();
        }
        [HttpPost]
        public IActionResult create(Ordere ordere)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ResellingContext db = new ResellingContext();
            /////---------------Ordere---------------------////////
            var new_order = new Ordere();
            new_order.Date=DateTime.Now;
            new_order.UserId = userId;    
            new_order.Adress = ordere.Adress;
            new_order.Phone = ordere.Phone;
            new_order.CustomerName = ordere.CustomerName;
            new_order.CustomerEmail = ordere.CustomerEmail;
            new_order.Status= "Processing";
            db.Orderes.Add(new_order);
            db.SaveChanges();
            //////---------------OrderDetail--------------------//////////////
            var target_order = db.Orderes.Where(x => x.UserId == userId).FirstOrDefault();
            List<Book> cart = HttpContext.Session.GetJson<List<Book>>("Book") ?? new List<Book>();
            foreach(var put_book in cart)
			{
                var order_details = new OrderDetail();
                order_details.BookId = put_book.Id;
                order_details.OrderId=target_order.Id;
                db.OrderDetails.Add(order_details);
                db.SaveChanges();

                var book = db.Books.Where(x => x.Id == put_book.Id).FirstOrDefault();
                book.Status = "sold";
                db.Books.Update(book);
                db.SaveChanges();
			}
            ///////////--------------REMOVE SESSION---------------------////////////////
            HttpContext.Session.Remove("Book");


            return Redirect("/allbook/index");
        }
    }
}
