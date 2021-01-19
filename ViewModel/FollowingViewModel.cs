using GigHub.Models;
using System.Collections.Generic;

namespace FollowingHub.ViewModel
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> FollowedUsers{ get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}