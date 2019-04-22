using Xunit;
using SpyFinder;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using SpyFinderData;

namespace SpyFinderTests
{
    public class Api
    {
        [Fact]
        public void GetSpyList()
        {
            var answer = new List<Spy>();
            var s1 = new Spy
            {
                name = "James Bond",
                code = new int[] { 0, 0, 7 }
            };
            var s2 = new Spy
            {
                name = "Ethan Hunt",
                code = new int[] { 3, 1, 4 }
            };
            answer.Add(s1);
            answer.Add(s2);

            var context = Substitute.For<ISpyDBContext>();
            context.GetSpiesList().Returns(answer);
            var c = new SpyFinder.Controllers.SpiesController(context);
            var result = c.Get().Value;
            Assert.True(Enumerable.SequenceEqual(answer, result));
        }

        [Fact]
        public void PutSpy()
        {
            var newSpy = new Spy
            {
                name = "George Smiley",
                code = new int[] { 6, 6, 6 }
            };

            var context = Substitute.For<ISpyDBContext>();
            var dummy = 0; // TODO: hack =- fix
            context.When(x => x.AddSpy(newSpy)).Do( x => dummy++); 
            var c = new SpyFinder.Controllers.SpiesController(context);
            var result = c.Put(newSpy).Value;
            Assert.Equal("Spy successfully added", result); 
        }

        [Fact]
        public void DeleteSpy()
        {
            var deleteSpyName = "George Smiley";
            var context = Substitute.For<ISpyDBContext>();
            var dummy = 0; // TODO: hack =- fix
            context.When(x => x.DeleteSpy(deleteSpyName)).Do(x => dummy++); 
            var c = new SpyFinder.Controllers.SpiesController(context);
            var result = c.Delete(deleteSpyName).Value;
            Assert.Equal("Spy successfully deleted", result); 
        }

        [Fact]
        public void CheckMessageForSpy()
        {
            var answer = true;
            var query = new MessageQuery {
                Spy = "James Bond",
                Message = new int[] {1,0,2,0,7,9}
                
            };

            var spyList = new List<Spy>();
            var s1 = new Spy
            {
                name = "James Bond",
                code = new int[] { 0, 0, 7 }
            };
            var s2 = new Spy
            {
                name = "Ethan Hunt",
                code = new int[] { 3, 1, 4 }
            };
            spyList.Add(s1);
            spyList.Add(s2);

            var context = Substitute.For<ISpyDBContext>();
            context.GetSpiesList().Returns(spyList);
            var c = new SpyFinder.Controllers.SpiesController(context);
            var result = c.Post(query).Value;
            Assert.Equal(answer, result);
        }
    }
}
