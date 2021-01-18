using GigHub.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.ViewModel;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var user = User.Identity.IsAuthenticated;
            var upcomingGigs = _context.Gigs
                .Include(m => m.Artist)
                .Include(m => m.Genre)
                .Where(m => m.DateTime > DateTime.Now);

            var viewModel = new HomeViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = user
            };
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}