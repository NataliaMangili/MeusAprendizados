using PetAdopt.Domain.Aggregates.NgoAggregate;
using PetAdopt.Domain.AggregatesModels.NgoAggregate;

namespace PetAdopt.Infrastructure;

public class PetContext : DbContext
{
    public DbSet<AdoptionForm> AdoptionForms { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetImage> PetImages { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Ngo> Ngos { get; set; }
    public DbSet<NgoAddress> NgoAddresses { get; set; }
    public DbSet<VolunteersContact> VolunteersContacts { get; set; }


    public PetContext(DbContextOptions<PetContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NgoAddress>()
        .OwnsOne(ua => ua.addressVO);

        modelBuilder.Entity<AdoptionForm>()
        .OwnsOne(ua => ua.addressVO);

        modelBuilder.Entity<VolunteersContact>()
        .OwnsOne(ua => ua.contactVO);


        base.OnModelCreating(modelBuilder);
    }
}