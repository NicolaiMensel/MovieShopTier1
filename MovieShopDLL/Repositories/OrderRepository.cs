﻿using System;
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
                dbContext.Entry(t).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                return t;
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
