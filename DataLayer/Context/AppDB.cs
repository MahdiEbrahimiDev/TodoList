using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) :base(options)
        {

        }
        public DbSet<TodoItem>TodoItems { get; set; }
        public DbSet<User> Users{ get; set; }

    }
}
