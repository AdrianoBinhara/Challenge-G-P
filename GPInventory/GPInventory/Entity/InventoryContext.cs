using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace GPInventory.Entity
{
    public class InventoryContext : DbContext
    {
        public DbSet<Items> ItemsList {get;set;}

        public InventoryContext()
        {
            this.Database.OpenConnection();
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = string.Empty;
            if(Device.RuntimePlatform == Device.Android)
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Inventory.db");
            }
            else
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"..","Library", "Inventory.db");
            }

            optionsBuilder.UseSqlite($"Data Source = {dbPath}");
        }
    }
}
