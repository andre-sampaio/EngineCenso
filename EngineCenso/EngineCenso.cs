using EngineCenso.Parser;
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
        public IEnumerable<CensoPropertyMapper> FindViableMappers(IEnumerable<CensoPropertyMapper> mapperCandidates, string input)
        {
            var parser = ParserFactory.BuildParserForInput(input);
            var viableMappers = mapperCandidates.Where(x => x.CanBeParsedBy(parser)).ToList();
            return viableMappers;
        }

        public CensoOutput Process(string input, CensoPropertyMapper mapper)
        {
            var parser = ParserFactory.BuildParserForInput(input);
            var processer = new CensoProcesser(parser, mapper);
            return processer.Process();
        }
    }
}
