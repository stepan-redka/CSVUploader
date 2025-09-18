using Microsoft.EntityFrameworkCore;

namespace TestApp.Models;

public class DbAppContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public DbAppContext(DbContextOptions options) : base(options) 
    {
        //base ctor 
    }
}