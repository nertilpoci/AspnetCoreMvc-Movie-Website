using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.DAL
{
    public class MovieRepository: Repository<Movie>, IMovieRepository
    {
        private ApplicationDbContext dbContext;
        public MovieRepository(ApplicationDbContext context)
            : base(context)
        {
            dbContext = context;
        }



        public async Task<IEnumerable<Movie>> GetFeaturedMoviesAsync(int count)
        {
         return await  dbContext.Movies.OrderBy(m => m.ReleaseDate).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetHeaderSliderMovies(int count)
        {
            // have some cutom logic on what to show 
            return await dbContext.Movies.OrderBy(m => m.ReleaseDate).Take(count).ToListAsync();
        }

     

        public  async Task<IEnumerable<KeyValuePair<Genre, IEnumerable<Movie>>>> GetLatestMoviesForGendersAsync(int count)
        {
           
            var genres = await dbContext.Genres.Where(z => z.Movies.Any()).ToListAsync();
            var output = new KeyValuePair<Genre, IEnumerable<Movie>>[genres.Count];//array is lest costly when you know the amount of data to be entered
            int i = 0;
            foreach (var genre in genres)
             {
              output[i] =new KeyValuePair<Genre, IEnumerable<Movie>>(genre,  genre.Movies.Take(count));
                i++;
              }
            return output;
        }

        public async Task<IEnumerable<Movie>> GetMoviesYouMightLike(int genreId, int count)
        {
            return await dbContext.Movies.Where(m => m.GenreId == genreId).OrderBy(m => m.ReleaseDate).Take(count).ToListAsync();
        }
    }
}
