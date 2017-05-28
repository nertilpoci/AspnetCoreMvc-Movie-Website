using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class MovieLinks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
