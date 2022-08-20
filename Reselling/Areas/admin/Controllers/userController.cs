using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reselling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Reselling.Areas.admin.Controllers
{
    [Area("admin")]
    public class userController : Controller
    {
        public IActionResult alluser()
        {
            ResellingContext db = new ResellingContext();
            var list_user = db.Users.ToList();

            return View(list_user);
        }

        public IActionResult hisbook(string id)
        {
            ResellingContext db = new ResellingContext();
            var list_book = db.Books.Where(x => x.UserId == id).ToList();
            return View(list_book);
        }

    }
}
