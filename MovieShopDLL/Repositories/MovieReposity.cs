using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopDLL.Context;
using MovieShopDLL.Entities;

namespace MovieShopDLL.Repositories
{
    public class MovieRepository : IRepository<Movie, int>
    {
        private IRepository<Genre, int> _genreRepository = new DLLFacade().GetGenreRepository();

        public Movie Create(Movie t)
        {
            using (var dbContext = new MovieShopContext())
            {
                dbContext.Movies.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public Movie Read(int id)
        {
            using (var dbContext = new MovieShopContext())
            {
                var m = dbContext.Movies.Include("Genre").Include("Orders").FirstOrDefault(x => x.Id == id);
                return m;
            }
        }

        public List<Movie> ReadAll()
        {
            using (var dbContext = new MovieShopContext())
            {

                var movies = dbContext.Movies.Include("Genre").Include("Orders").ToList();
                return movies;
            }
        }

        public Movie Update(Movie t)
        {
            using (var dbContext = new MovieShopContext())
            {
                dbContext.Entry(t).State = EntityState.Modified;
                dbContext.SaveChanges();
                return t;
            }
        }

        public bool Delete(int id)
        {
            using (var dbContext = new MovieShopContext())
            {
                var toBeDeleted = dbContext.Movies.FirstOrDefault(x => x.Id == id);
                if (toBeDeleted != null)
                {
                    dbContext.Movies.Remove(toBeDeleted);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
