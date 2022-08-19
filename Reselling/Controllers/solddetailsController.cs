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
    public class solddetailsController : Controller
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
        public IActionResult Index(int id)
        {
            checkuser();
            ResellingContext db = new ResellingContext();
            var target_order_details = db.OrderDetails.Where(x => x.BookId == id).FirstOrDefault();
            var target_order = db.Orderes.Where(x => x.Id == target_order_details.OrderId).FirstOrDefault();
            Console.WriteLine("222222222222222222222");
            Console.WriteLine(target_order.Id);
            Console.WriteLine("222222222222222222222");

            return View(target_order);
        }
        [HttpPost]
        public IActionResult status_order(Ordere ordere)
        {
            
            checkuser();
            ResellingContext db = new ResellingContext();
            var target_order = db.Orderes.Where(x => x.CustomerEmail== ordere.CustomerEmail&&x.CustomerName==ordere.CustomerName&&x.Phone==ordere.Phone).FirstOrDefault();

            
            target_order.Status = ordere.Status;
            db.Update(target_order);
            db.SaveChanges();
           
            return Redirect("/allbook/index");
        }
    }
}
