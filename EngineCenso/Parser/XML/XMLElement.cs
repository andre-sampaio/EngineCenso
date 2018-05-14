using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso.Parser.XML
{
    internal class XMLElement : IParserElement
    {
        private XElement element;

        public XMLElement(XElement element)
        {
            this.element = element;
        }

        public List<IParserElement> SelectElements(string path)
        {
            List<IParserElement> elements = new List<IParserElement>();

            var xmlElements = element.XPathSelectElements(path);

            foreach (var element in xmlElements)
            {
                elements.Add(new XMLElement(element));
            }

            return elements;
        }

        public int SelectInt(string path)
        {
            return int.Parse(element.XPathSelectElement(path).Value);
        }

        public string SelectString(string path)
        {
            return element.XPathSelectElement(path).Value;
        }
    }
}
