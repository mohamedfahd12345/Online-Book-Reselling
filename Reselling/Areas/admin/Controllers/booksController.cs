using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reselling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Reselling.Areas.admin.Controllers
{
    [Area("admin")]
    public class booksController : Controller
    {
        public IActionResult Index()
        {
            ResellingContext db = new ResellingContext();
            var list_books = db.Books.ToList();
            return View(list_books);
        }
    }
}
