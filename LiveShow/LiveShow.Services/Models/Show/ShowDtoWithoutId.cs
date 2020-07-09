using System;

namespace LiveShow.Services.Models.Show
{
    public class ShowDtoWithoutId
    {
        public bool IsCanceled { get; set; }

        public Guid ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public byte GenreId { get; set; }
    }
}
