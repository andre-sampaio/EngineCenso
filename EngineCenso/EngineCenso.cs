﻿using EngineCenso.Parser;
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
        private IEnumerable<CensoMapping> mapperCandidates;

        public EngineCenso(IEnumerable<CensoMapping> mapperCandidates)
        {
            this.mapperCandidates = mapperCandidates;
        }

        public CensoOutput Process(string input)
        {
            var parser = ParserFactory.BuildParserForInput(input);
            var viableMappers = mapperCandidates.Where(x => x.CanBeParsedBy(parser)).ToList();

            if (viableMappers.Count() > 1)
                throw new Exception("Ambiguous call. More than one viable candidate");
            if (viableMappers.Count() < 1)
                throw new Exception("No viable parser");

            var processer = new CensoProcesser(parser, viableMappers.First());

            return processer.Process();
        }
    }
}
