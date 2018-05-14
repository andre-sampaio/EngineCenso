using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.Parser.Json
{
    internal class JsonElement : IParserElement
    {
        private JToken element;

        public JsonElement(JToken element)
        {
            this.element = element;
        }

        public List<IParserElement> SelectElements(string path)
        {
            List<IParserElement> elements = new List<IParserElement>();

            var jsonElements = element.SelectTokens(path);

            foreach (var element in jsonElements)
            {
                elements.Add(new JsonElement(element));
            }

            return elements;
        }

        public int SelectInt(string path)
        {
            return element.SelectToken(path).Value<int>();
        }

        public string SelectString(string path)
        {
            return element.SelectToken(path).Value<string>();
        }
    }
}
