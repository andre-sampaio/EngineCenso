using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineCenso.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EngineCenso.RestApi.Controllers
{
    [Route("api/[controller]")]
    public class CensoController : Controller
    {
        private ICensoMappingRepository censoMappingRepository;

        public CensoController(ICensoMappingRepository censoMappingRepository)
        {
            this.censoMappingRepository = censoMappingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]string censoData)
        {
            var mappings = await censoMappingRepository.GetAll();

            EngineCenso engine = new EngineCenso(mappings.Select(x => x.ToPropertyMapper()));
            var output = engine.Process(censoData);
            return Json(output);
        }
    }
}
