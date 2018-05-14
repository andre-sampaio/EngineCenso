using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.Parser
{
    internal interface IParser
    {
        List<IParserElement> SelectElements(string path);
        bool IsValidExpression(string path);
    }
}
