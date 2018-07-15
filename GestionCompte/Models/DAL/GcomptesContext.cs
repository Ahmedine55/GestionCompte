using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GestionCompte.Models.DAL
{
    public class GcomptesContext : DbContext
    {
        public GcomptesContext() : base("GcomptesContext")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}