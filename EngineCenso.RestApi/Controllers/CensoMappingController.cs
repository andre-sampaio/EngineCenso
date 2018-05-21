using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EngineCenso.DataAccess;
using EngineCenso.RestApi.Filters;
using EngineCenso.RestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EngineCenso.RestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CensoMapping")]
    [Authorize]
    [ServiceFilter(typeof(LoggingActionFilter))]
    public class CensoMappingController : Controller
    {
        private ICensoMappingRepository censoMappingRepository;
        private ILogger logger;
        private IMapper mapper;

        public CensoMappingController(ICensoMappingRepository censoMappingRepository, ILogger<CensoMappingController> logger, IMapper mapper)
        {
            this.censoMappingRepository = censoMappingRepository;
            this.logger = logger;
            this.mapper = mapper;
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
            var mapping = await censoMappingRepository.Get(name);
            if (mapping == null)
                return NotFound();

            return Json(mapping);
        }
        
        // POST: api/CensoMapping
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CensoMappingInsertModel map)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMap = await censoMappingRepository.Get(map.Name);

            if (existingMap != null)
                return StatusCode(409, new { error = $"A mapping with name {map.Name} already exists. Please choose another name." }); // 409: Conflict

            var actualMap = mapper.Map<CensoMapping>(map);

            await censoMappingRepository.Insert(actualMap);

            var requestUrl = Request.GetEncodedUrl();

            return Created($"{requestUrl}/{map.Name}", map);
        }
        
        // PUT: api/CensoMapping/RJ
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody]CensoMappingUpdateModel map)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mapping = await censoMappingRepository.Get(name);
            if (mapping == null)
                return NotFound();

            var actualMap = mapper.Map<CensoMapping>(map);

            var success = await censoMappingRepository.Update(name, actualMap);
            if (success)
                return Ok();
            else
                return StatusCode(500); // Error
        }
        
        // DELETE: api/ApiWithActions/RJ
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var mapping = await censoMappingRepository.Get(name);
            if (mapping == null)
                return NotFound();

            var success = await censoMappingRepository.Delete(name);
            if (success)
                return Ok();
            else
                return StatusCode(500); // Error
        }
    }
}
