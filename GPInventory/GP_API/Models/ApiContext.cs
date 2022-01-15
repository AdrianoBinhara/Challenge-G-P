using System;
using GP_API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GP_API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Item> items { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options): base(options)
        {
        }
    }
}
