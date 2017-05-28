using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.DAL
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetFeaturedMoviesAsync(int count);
        Task<IEnumerable<KeyValuePair<Genre, IEnumerable<Movie>>>> GetLatestMoviesForGendersAsync(int count);
        Task<IEnumerable<Movie>> GetHeaderSliderMovies(int count);
        Task<IEnumerable<Movie>> GetMoviesYouMightLike(int genreId,int count);

    }
}
