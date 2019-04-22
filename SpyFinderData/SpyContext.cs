using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SpyFinderData
{
    public class SpyDBContext : DbContext, ISpyDBContext
    {
        public SpyDBContext(DbContextOptions<SpyDBContext> options) : base(options) { }
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
