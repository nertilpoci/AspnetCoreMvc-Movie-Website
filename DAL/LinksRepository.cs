using Movies.Data;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.DAL
{
    public class LinksRepository: Repository<MovieLinks>, ILinksRepository
    {
        private ApplicationDbContext dbContext;
        public LinksRepository(ApplicationDbContext context)
            : base(context)
        {
            dbContext = context;
        }
    }
}
