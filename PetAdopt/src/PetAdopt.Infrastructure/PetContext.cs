using Microsoft.EntityFrameworkCore;
using PetAdopt.Domain.AggregatesModels.AdoptionAggregate;

namespace PetAdopt.Infrastructure;

public class PetContext : DbContext
{
    public DbSet<Adoption> Adoptions { get; set; }

    public PetContext(DbContextOptions<PetContext> options) : base(options)
    {

    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    //modelBuilder.Entity<>()
    //    //.OwnsOne(ua => ua.);

    //    base.OnModelCreating(modelBuilder);
    //}
}