using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EngineCenso
{
    internal class CensoRJParser : CensoParser
    {
        protected override string xPathCidades => "/corpo/cidade";
        protected override string xPathCidadeNome => "nome";
        protected override string xPathCidadeHabitantes => "populacao";
        protected override string xPathCidadeBairros => "bairros/bairro";
        protected override string xPathBairroNome => "nome";
        protected override string xPathBairroHabitantes => "populacao";

        public CensoRJParser(string input) : base(input)
        {

        }
    }
}
