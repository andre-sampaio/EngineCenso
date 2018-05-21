using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineCenso.DataAccess;
using EngineCenso.RestApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EngineCenso.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LoggingActionFilter))]
    public class CensoController : Controller
    {
        private ICensoMappingRepository censoMappingRepository;
        private ILogger logger;

        public CensoController(ICensoMappingRepository censoMappingRepository, ILogger<CensoController> logger)
        {
            this.censoMappingRepository = censoMappingRepository;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]string censoData)
        {
            if (string.IsNullOrWhiteSpace(censoData))
                return BadRequest(new { error = $"{nameof(censoData)} must not be empty" }); // BadRequest

            var mappings = await censoMappingRepository.GetAll();

            if (mappings == null || mappings.Count() < 1)
                return StatusCode(500, new { error = $"No mapping found" });

            EngineCenso engine = new EngineCenso();
            var viableMappers = engine.FindViableMappers(mappings.Select(x => x.ToPropertyMapper()), censoData);

            if (viableMappers == null || viableMappers.Count() < 1)
                return StatusCode(500, new { error = $"No mappings able to parse data" });

            if (viableMappers.Count() > 1)
                logger.LogWarning($"{viableMappers.Count()} viable mappers found for input {censoData}.");

            var output = engine.Process(censoData, viableMappers.First());
            return Json(output);
        }
    }
}
