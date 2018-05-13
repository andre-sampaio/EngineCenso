using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EngineCenso
{
    internal class CensoMGParser : CensoParser
    {
        internal override string Process(string input)
        {
            XDocument document = XDocument.Parse(input);


            var output = new CensoOutput
            {
                Result = ParseCidades(document.Element("body").Elements("region").Elements("cities").Elements("city"))
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
                Cidade = cidadeElement.Element("name").Value,
                Habitantes = int.Parse(cidadeElement.Element("population").Value),
                Bairros = ParseBairros(cidadeElement.Element("neighborhoods").Elements("neighborhood"))
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
                Nome = bairroElement.Element("name").Value,
                Habitantes = int.Parse(bairroElement.Element("population").Value)
            };
        }
    }
}