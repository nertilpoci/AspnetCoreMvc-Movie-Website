using Movies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
       
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Movies = new MovieRepository(_context);
            Genres = new GenreRepository(_context);
            Links = new LinksRepository(_context);

        }

        public IMovieRepository Movies { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public ILinksRepository Links { get; private set; }



        public void Dispose()
        {
            _context.Dispose();
        }

        public int Persist()
        {
            return _context.SaveChanges();
        }

        public async Task<int> PersistAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }
}
