using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reselling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Reselling.Areas.admin.Controllers
{
    [Area("admin")]
    public class solddetailsController : Controller
    {
        public IActionResult Index(int id)
        {
            ResellingContext db = new ResellingContext();
            var target_order_details = db.OrderDetails.Where(x => x.BookId == id).FirstOrDefault();
            var target_order = db.Orderes.Where(x => x.Id == target_order_details.OrderId).FirstOrDefault();
            

            return View(target_order);
           
        }
    }
}
