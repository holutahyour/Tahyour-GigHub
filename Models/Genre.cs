using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }


}