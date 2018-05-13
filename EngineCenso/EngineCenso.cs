using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EngineCenso
{
    public class EngineCenso
    {
        private CensoParser parser;
        private string input;

        public EngineCenso(string input, IEnumerable<CensoMapper> mapperCandidates)
        {
            var viableMappers = mapperCandidates.Where(x => x.MatchesInput(input));

            if (viableMappers.Count() > 1)
                throw new Exception("Ambiguous input. More than one viable candidate.");
            if (viableMappers.Count() < 1)
                throw new Exception("No viable candidate found.");

            parser = new CensoParser(input, viableMappers.First());

            this.input = input;
        }

        public string Process()
        {
            return parser.Process();
        }
    }
}
