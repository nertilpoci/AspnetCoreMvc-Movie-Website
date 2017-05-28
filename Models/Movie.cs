using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public decimal Rating { get; set; }

        public string YoutubeVideoId { get; set; }

        public string PosterPortrait{ get; set; }
        public string PosterLandscape { get; set; }


        [Required]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public HashSet<MovieLinks> Links { get; set; } = new HashSet<MovieLinks>();
    }
}
