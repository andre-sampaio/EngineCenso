using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.Parser.Json
{
    internal class JsonParser : IParser
    {
        private JObject jsonDocument;

        public JsonParser(string input)
        {
            jsonDocument = JObject.Parse(input);
        }

        public bool IsValidExpression(string path)
        {
            try
            {
                jsonDocument.SelectTokens(path);
                return true;
            }
            catch(JsonException)
            {
                // Throws if jsonPath is not valid.
                //  Ignore exception and return false
                return false;
            }
        }

        public List<IParserElement> SelectElements(string path)
        {
            List<IParserElement> elements = new List<IParserElement>();

            var jsonElements = jsonDocument.SelectTokens(path);

            foreach (var element in jsonElements)
            {
                elements.Add(new JsonElement(element));
            }

            return elements;
        }
    }
}
