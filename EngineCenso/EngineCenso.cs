using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EngineCenso
{
    public class EngineCenso
    {
        private CensoParser parser;
        private string input;

        public EngineCenso(string input)
        {
            if (input.StartsWith("<corpo>"))
                parser = new CensoRJParser();
            else
                parser = new CensoMGParser();

            this.input = input;
        }

        public string Process()
        {
            return parser.Process(input);
        }
    }
}
