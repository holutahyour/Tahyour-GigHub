using FollowingHub.ViewModel;
using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult MyGigs()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = viewModel.Venue,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
            var viewModel = new GigViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };
            return View("Gigs",viewModel);
        }


        [Authorize]
        public ActionResult FollowedUser()
        {
            var userId = User.Identity.GetUserId();

            var followedUsers = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(g => g.Followee)
                .ToList();

            var viewModel = new FollowingViewModel
            {
                FollowedUsers = followedUsers,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "The Users Following Me "
            };
            return View(viewModel);
        }
    }
}