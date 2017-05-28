using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.DAL
{
   public interface IUnitOfWork:IDisposable
    {
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        ILinksRepository Links { get; }

        int Persist();
          Task<int> PersistAsync();
    }
}
