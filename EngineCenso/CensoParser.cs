using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso
{
    internal abstract class CensoParser
    {
        protected abstract string xPathCidades { get; }
        protected abstract string xPathCidadeNome { get; }
        protected abstract string xPathCidadeHabitantes { get; }
        protected abstract string xPathCidadeBairros { get; }
        protected abstract string xPathBairroNome { get; }
        protected abstract string xPathBairroHabitantes { get; }


        private XDocument document;

        public CensoParser(string input)
        {
            document = XDocument.Parse(input);
        }

        internal protected string Process()
        {
            var output = new CensoOutput
            {
                Result = ParseCidades(document.XPathSelectElements(xPathCidades))
            };

            var resolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(output, new JsonSerializerSettings() { ContractResolver = resolver });
        }

        private IEnumerable<Result> ParseCidades(IEnumerable<XElement> cidadeElements)
        {
            foreach (var element in cidadeElements)
            {
                yield return ParseCidade(element);
            }
        }

        private Result ParseCidade(XElement cidadeElement)
        {
            return new Result()
            {
                Cidade = cidadeElement.XPathSelectElement(xPathCidadeNome).Value,
                Habitantes = int.Parse(cidadeElement.XPathSelectElement(xPathCidadeHabitantes).Value),
                Bairros = ParseBairros(cidadeElement.XPathSelectElements(xPathCidadeBairros))
            };
        }

        private IEnumerable<Bairro> ParseBairros(IEnumerable<XElement> bairroElements)
        {
            foreach (var element in bairroElements)
            {
                yield return ParseBairro(element);
            }
        }

        private Bairro ParseBairro(XElement bairroElement)
        {
            return new Bairro()
            {
                Nome = bairroElement.XPathSelectElement(xPathBairroNome).Value,
                Habitantes = int.Parse(bairroElement.XPathSelectElement(xPathBairroHabitantes).Value)
            };
        }
    }
}
