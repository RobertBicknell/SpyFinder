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
            return _context.GetSpiesList(); 
        }

        // POST api/spies
        [HttpPost]
        public ActionResult<bool> Post([FromBody] MessageQuery query)
        {
            var code = _context.GetSpiesList().Where(s => s.name == query.Spy).First().code;
            return SpyChecker.MessageContainsSpy(query.Message, code);
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody] Spy newSpy)
        {
            _context.AddSpy(newSpy);
            return "Spy successfully added"; 
        }

        // DELETE api/spies/SpyName
        [HttpDelete("{name}")]
        public ActionResult<string> Delete(string name)
        {
            _context.DeleteSpy(name);
            return "Spy successfully deleted"; 
        }
    }
}
