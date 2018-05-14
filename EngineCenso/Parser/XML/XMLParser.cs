using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso.Parser.XML
{
    internal class XMLParser : IParser
    {
        private XDocument xmlDocument;

        public XMLParser(string input)
        {
            xmlDocument = XDocument.Parse(input);
        }

        public bool IsValidExpression(string path)
        {
            try
            {
                XPathExpression.Compile(path);
                return true;
            }
            catch(XPathException)
            {
                // Throws if xPath is not valid.
                //  Ignore exception and return false
                return false;
            }
        }

        public List<IParserElement> SelectElements(string path)
        {
            List<IParserElement> elements = new List<IParserElement>();

            var xmlElements = xmlDocument.XPathSelectElements(path);

            foreach(var element in xmlElements)
            {
                elements.Add(new XMLElement(element));
            }

            return elements;
        }
    }
}
