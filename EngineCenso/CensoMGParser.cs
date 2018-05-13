using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso
{
    internal class CensoMGParser : CensoParser
    {
        protected override string xPathCidades => "/body/region/cities/city";
        protected override string xPathCidadeNome => "name";
        protected override string xPathCidadeHabitantes => "population";
        protected override string xPathCidadeBairros => "neighborhoods/neighborhood";
        protected override string xPathBairroNome => "name";
        protected override string xPathBairroHabitantes => "population";

        public CensoMGParser(string input): base(input)
        {

        }
    }
}