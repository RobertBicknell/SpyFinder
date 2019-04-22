using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpyFinderData;
using SpyFinderLogic;

namespace SpyFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiesController : ControllerBase
    {

        private readonly ISpyDBContext _context;

        public SpiesController(ISpyDBContext context)
        {
            _context = context;
        }



        // GET api/spies
        [HttpGet]
        public ActionResult<List<Spy>> Get()
        {
            //var context = new SpyDBContext();
            return _context.GetSpiesList(); // ().ToList(); 
        }

        // POST api/spies
        [HttpPost]
        public ActionResult<bool> Post([FromBody] MessageQuery query)
        //public ActionResult<bool> Post([FromForm] string whatever)
        //public ActionResult<string> Post([FromForm] string whatever)
        {
            //if (ModelState.IsValid) { //TODO - need this?
            //    var q = 0;
            //}
            //if (query != null)
            //{
            //    var x = query;
            //}
            //var context = new SpyDBContext();
            var code = _context.GetSpiesList().Where(s => s.name == query.Spy).First().code;
            return SpyChecker.MessageContainsSpy(query.Message, code);
            //return true; //fix//
        }

        // PUT api/spies/name/code
        //[HttpPut("{name}/{code}")]
        [HttpPut]
        public ActionResult<string> Put([FromBody] Spy newSpy)
        {
            //var context = new SpyDBContext();
            //var spy = new Spy {
             //   name = _name,
              //  code = _code
            //};
            //context.spies.Add(spy);
            //_context.spies.Add(newSpy);
            //_context.SaveChanges();
            _context.AddSpy(newSpy);

            return "Spy successfully added"; // true; //TODO could use OK and supply a more informqt
        }

        // DELETE api/spies/SpyName
        [HttpDelete("{name}")]
        public ActionResult<string> Delete(string name)
        {
            //var context = new SpyDBContext();
            //var spyToDelete = _context.spies.Where(s => s.name == name).First();
            //_context.spies.Remove(spyToDelete);
            //_context.SaveChanges();
            _context.DeleteSpy(name);
            return "Spy successfully deleted"; // true; //TODO could use OK and supply a more informqt
        }
    }
}
