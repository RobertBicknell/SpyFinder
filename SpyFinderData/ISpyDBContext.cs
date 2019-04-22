using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace SpyFinderData
{
    public interface ISpyDBContext 
    {
        DbSet<Spy> spies { get; set; }
        DbSet<Spy> GetSpies();
        List<Spy> GetSpiesList();
        int SaveChanges(); //TODO remove?
        void AddSpy(Spy s);
        void DeleteSpy(string spyName);
    }
}