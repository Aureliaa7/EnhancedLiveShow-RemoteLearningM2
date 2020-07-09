using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Dal.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        ICollection<Show> Shows { get; set; }

        public Genre()
        {
            Shows = new List<Show>();
        }
    }
}
