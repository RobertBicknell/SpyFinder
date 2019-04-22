using System;
using Xunit;
using SpyFinderData;
using System.Collections.Generic;
//
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SpyFinderTests
{
    public class Data {
        SpyDBContext context;
        string ConnectionString = "Host=localhost;Database=Spies;Username=postgres;Password=password"; //TODO: Lookup from config
        public Data() {
            var optionsBuilder = new DbContextOptionsBuilder<SpyDBContext>();
            optionsBuilder.UseNpgsql(ConnectionString); //fetch from Test project app.settings.. config....
            context = new SpyDBContext(optionsBuilder.Options);
        }

        void CleanDB() {
            context.spies.RemoveRange(context.spies);
        }

        List<Spy> TestSpies() {
            var testSpies = new List<Spy>();
            testSpies.AddRange(new Spy[] {
                new Spy {
                    name = "James Bond",
                    code = new int[] { 0, 0, 7 }
                },
                new Spy {
                    name = "Ethan Hunt",
                    code = new int[] { 3, 1, 4 }
                }

            });
            return testSpies;
        }

        void InsertSpies(IEnumerable<Spy> spies) {
            context.spies.AddRange(spies);
            context.SaveChanges();
        }

        class SpyNameComparer : IComparer<Spy> {
            public int Compare(Spy s1, Spy s2) {
                return s1.name.CompareTo(s2.name);
            }
        }

        [Fact]
        public void LoadSpies() {
            CleanDB();
            var answer = TestSpies();
            InsertSpies(answer);
            answer.Sort(new SpyNameComparer());

            var result = context.spies.ToList();
            result.Sort(new SpyNameComparer());

            Assert.Equal(answer.Count, result.Count);
            for (var i = 0; i < answer.Count; i++) {
                Assert.Equal(answer[i].name, result[i].name); 
                Assert.True(Enumerable.SequenceEqual(answer[i].code, result[i].code));
            }
        }

        [Fact]
        public void AddNewSpy() {
            CleanDB();
            var answer = TestSpies();
            InsertSpies(answer);

            var smiley = new Spy
            {
                name = "George Smiley",
                code = new int[] { 1, 1, 1 }
            };

            context.spies.Add(smiley);
            context.SaveChanges();
            var newSpy = context.spies.Where(s => s.name == smiley.name).First();
            var areEqual = Enumerable.SequenceEqual(smiley.code, newSpy.code);
            Assert.True(areEqual);
        }

        [Fact]
        public void TryAddExitingSpy() {
            var existingSpy = new Spy
            {
                name = "James Bond",
                code = new int[] { 1, 1, 1 }
            };
            var failed = false;
            try
            {
                context.spies.Add(existingSpy);
                context.SaveChanges();
            }
            catch
            {
                failed = true;
            }
            Assert.True(failed);
        }

        [Fact]
        public void DeleteSpy() {
            CleanDB();
            var answer = TestSpies();
            InsertSpies(answer);

            var deleteSpy = answer[0];
            context.spies.Remove(deleteSpy);
            context.SaveChanges();

            var deletedSpyCount = context.spies.Where(s => s.name == deleteSpy.name).Count();
            Assert.Equal(0, deletedSpyCount);
        }

        [Fact]
        public void TryDeleteNonexistentSpy() {
            var smiley = new Spy
            {
                name = "George Smiley",
                code = new int[] { 1, 1, 1 }
            };

            var failed = false;
            try
            {
                context.spies.Remove(smiley); 
                context.SaveChanges(); 
            }
            catch
            {
                failed = true;
            }
            Assert.True(failed);
        }
    }
}
