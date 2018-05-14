using EngineCenso.Parser.Json;
using EngineCenso.Parser.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineCenso.Parser
{
    internal static class ParserFactory
    {
        internal static IParser BuildParserForInput(string input)
        {
            var firstNonBlankChar = input.First(x => !char.IsWhiteSpace(x));
            if (firstNonBlankChar == '{')
                return new JsonParser(input);
            else
                return new XMLParser(input);
        }
    }
}
