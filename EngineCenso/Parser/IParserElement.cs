using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.Parser
{
    internal interface IParserElement
    {
        string SelectString(string path);
        int SelectInt(string path);
        List<IParserElement> SelectElements(string path);
    }
}
