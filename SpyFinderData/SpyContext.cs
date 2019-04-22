using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SpyFinderData
{
    public class SpyDBContext : DbContext, ISpyDBContext
    {
        //string ConnectionString; // = "Host=localhost;Database=Spies;Username=postgres;Password=password"; //TODO: load from config

        //public SpyDBContext(string testDbConnectionString) 
        //{
          //  ConnectionString = testDbConnectionString;

//        }

        //DbContextOptionsBuilder optionsBuilder => optionsBuilder.UseNpgsql(ConnectionString);

  //      public void ConfigureConnecrtion(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql(ConnectionString); 


        public SpyDBContext(DbContextOptions<SpyDBContext> options) : base(options) { }



        //{
        //   ConnectionString = connectionString;
        // }
        public DbSet<Spy> spies { get; set; }

        public virtual DbSet<Spy> GetSpies() {
            return spies;
        }

        public virtual List<Spy> GetSpiesList()
        {
            return spies.ToList();
        }

        public virtual void AddSpy(Spy newSpy)
        {
            spies.Add(newSpy);
            SaveChanges();
        }

        public virtual void DeleteSpy(string name)
        {
            var deleteSpy = spies.Where(s => s.name == name).First();
            spies.Remove(deleteSpy);
            SaveChanges();
        }
    }
}
