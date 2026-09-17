using Microsoft.EntityFrameworkCore;
using ZombieParty.Data;

namespace ZombieParty.Models.Data
{
    public class ZombiePartyDbContext : DbContext
    {
        public ZombiePartyDbContext(DbContextOptions<ZombiePartyDbContext> options) : base(options)
        {

        }
        public DbSet<Zombie> Zombies { get; set; }
        public DbSet<ZombieType> ZombieTypes { get; set; }
        public DbSet<HuntingLog> HuntingLogs { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

        // Étape 4 : Ajout de la méthode de configuration au démarrage
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Générer des données de départ
            modelBuilder.GenerateData();
        }
    }
}
