using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineCenso.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngineCenso.RestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CensoMapping")]
    [Authorize]
    public class CensoMappingController : Controller
    {
        private ICensoMappingRepository censoMappingRepository;

        public CensoMappingController(ICensoMappingRepository censoMappingRepository)
        {
            this.censoMappingRepository = censoMappingRepository;
        }

        // GET: api/CensoMapping
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await censoMappingRepository.GetAll());
        }

        // GET: api/CensoMapping/RJ
        [HttpGet("{name}", Name = "Get")]
        public async Task<IActionResult> Get(string name)
        {
            return Json(await censoMappingRepository.Get(name));
        }
        
        // POST: api/CensoMapping
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CensoMappingModel map)
        {
            await censoMappingRepository.Insert(map);

            return Ok();
        }
        
        // PUT: api/CensoMapping/RJ
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody]CensoMappingModel map)
        {
            var success = await censoMappingRepository.Update(name, map);
            if (success)
                return Ok();
            else
                return NotFound();
        }
        
        // DELETE: api/ApiWithActions/RJ
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var success = await censoMappingRepository.Delete(name);
            if (success)
                return Ok();
            else
                return NotFound();
        }
    }
}
