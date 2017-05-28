using Movies.Data;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.DAL
{
    public class GenreRepository: Repository<Genre>, IGenreRepository
    {
        private ApplicationDbContext dbContext;
        public GenreRepository(ApplicationDbContext context)
            : base(context)
        {
            dbContext = context;
        }
    }
}
