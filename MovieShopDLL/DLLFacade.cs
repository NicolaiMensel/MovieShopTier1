using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopDLL.Entities;
using MovieShopDLL.Repositories;

namespace MovieShopDLL
{
    public class DLLFacade
    {
        private IRepository<Customer, int> cm;
        private IRepository<Movie, int> mm;
        private IRepository<Order, int> om;
        private IRepository<Genre, int> gm;

        public IRepository<Customer, int> GetCustomerRepository()
        {
            return cm ?? (cm = new CustomerRepository());
        }

        public IRepository<Movie, int> GetMovieRepository()
        {
            return mm ?? (mm = new MovieRepository());
        }

        public IRepository<Order, int> GetOrderRepository()
        {
            return om ?? (om = new OrderRepository());
        }

        public IRepository<Genre, int> GetGenreRepository()
        {
            return gm ?? (gm = new GenreRepository());
        }
    }
}
