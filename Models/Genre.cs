using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public HashSet<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
