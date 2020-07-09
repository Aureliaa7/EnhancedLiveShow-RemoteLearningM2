using System;
using System.ComponentModel.DataAnnotations;

namespace LiveShowClient.ViewModels
{
    public class ShowWithIdVM
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        [Display(Name = "Date time")]
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Is canceled")]
        public bool IsCanceled { get; set; }
        [Display(Name = "Artist")]
        public string ArtistName { get; set; }
        public int Attendances { get; set; }
    }
}
