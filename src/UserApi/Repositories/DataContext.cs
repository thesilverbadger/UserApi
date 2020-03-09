using System;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
