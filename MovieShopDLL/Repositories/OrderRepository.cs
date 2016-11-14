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
    class OrderRepository : IRepository<Order, int>
    {
        private MovieShopContext dbContext;
        public Order Create(Order t)
        {
            using (dbContext = new MovieShopContext())
            {
                dbContext.Entry(t.Movie).State = EntityState.Unchanged;
                dbContext.Entry(t.Customer).State = EntityState.Unchanged;
                dbContext.Orders.Add(t);
                dbContext.SaveChanges();
                return t;
            }
        }

        public Order Read(int id)
        {
            using (dbContext = new MovieShopContext())
            {
                return dbContext.Orders.Include("Customer").Include("Movie").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Order> ReadAll()
        {
            using (dbContext = new MovieShopContext())
            {
                return dbContext.Orders.Include("Customer").Include("Movie").ToList();
            }
        }

        public Order Update(Order t)
        {
            using (dbContext = new MovieShopContext())
            {
                var oldOrder = dbContext.Orders.FirstOrDefault(x => x.Id == t.Id);
                if (oldOrder != null)
                {
                    if (t.Customer != null)
                    {
                        oldOrder.Customer = dbContext.Customers.FirstOrDefault(x => x.Id == t.Customer.Id);
                    }
                    oldOrder.Movie = dbContext.Movies.FirstOrDefault(x => x.Id == t.Movie.Id);
                    dbContext.Entry(oldOrder).CurrentValues.SetValues(t);
                    dbContext.SaveChanges();
                }
                return dbContext.Orders.FirstOrDefault(x => x.Id == t.Id);
            }
        }

        public bool Delete(int id)
        {
            using (dbContext = new MovieShopContext())
            {
                var toBeDeleted = dbContext.Orders.FirstOrDefault(x => x.Id == id);
                if (toBeDeleted != null)
                {
                    dbContext.Orders.Remove(toBeDeleted);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
