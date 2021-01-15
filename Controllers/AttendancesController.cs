using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        public ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var user = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == user && a.GigId == dto.GigId))
                return BadRequest("The Attendance Already Exist");


            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = user,
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
