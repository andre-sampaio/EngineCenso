using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EngineCenso.RestApi.Models
{
    public class CensoMappingInsertModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CidadesPath { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NomeCidadePath { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string HabitantesCidadePath { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string BairrosPath { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NomeBairroPath { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string HabitantesBairroPath { get; set; }
    }
}
