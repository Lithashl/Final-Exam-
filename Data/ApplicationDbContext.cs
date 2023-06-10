using mysql_connect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mysql_connect.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        
        base.OnModelCreating(modelbuilder);
    }
    //private Guid _no;
    //[Key]
    //public Guid NO
    //{
    //    get { return _no; }
    //}

    public DbSet<Book> Book { get; set; }
}

