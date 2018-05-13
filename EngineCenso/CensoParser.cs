using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso
{
    internal class CensoParser
    {
        private XDocument document;
        private CensoMapper mapper;

        internal CensoParser(string input, CensoMapper mapper)
        {
            document = XDocument.Parse(input);
            this.mapper = mapper;
        }

        internal protected string Process()
        {
            var output = new CensoOutput
            {
                Result = ParseCidades(document.XPathSelectElements(mapper.CidadesPath))
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
                Cidade = cidadeElement.XPathSelectElement(mapper.NomeCidadePath).Value,
                Habitantes = int.Parse(cidadeElement.XPathSelectElement(mapper.HabitantesCidadePath).Value),
                Bairros = ParseBairros(cidadeElement.XPathSelectElements(mapper.BairrosPath))
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
                Nome = bairroElement.XPathSelectElement(mapper.NomeBairroPath).Value,
                Habitantes = int.Parse(bairroElement.XPathSelectElement(mapper.HabitantesBairroPath).Value)
            };
        }
    }
}
