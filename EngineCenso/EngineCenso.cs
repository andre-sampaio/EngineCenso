using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EngineCenso
{
    public class EngineCenso
    {
        public string Process(string input)
        {
            XDocument document = XDocument.Parse(input);


            var output = new CensoOutput
            {
                Result = ParseCidades(document.Element("corpo").Elements("cidade"))
            };

            var resolver = new CamelCasePropertyNamesContractResolver();
            return Newtonsoft.Json.JsonConvert.SerializeObject(output, new JsonSerializerSettings() { ContractResolver = resolver });
        }

        private IEnumerable<Result> ParseCidades(IEnumerable<XElement> cidadeElements)
        {
            foreach(var element in cidadeElements)
            {
                yield return ParseCidade(element);
            }
        }

        private Result ParseCidade(XElement cidadeElement)
        {
            return new Result()
            {
                Cidade = cidadeElement.Element("nome").Value,
                Habitantes = int.Parse(cidadeElement.Element("populacao").Value),
                Bairros = ParseBairros(cidadeElement.Element("bairros").Elements("bairro"))
            };
        }

        private IEnumerable<Bairro> ParseBairros(IEnumerable<XElement> bairroElements)
        {
            foreach(var element in bairroElements)
            {
                yield return ParseBairro(element);
            }
        }

        private Bairro ParseBairro(XElement bairroElement)
        {
            return new Bairro()
            {
                Nome = bairroElement.Element("nome").Value,
                Habitantes = int.Parse(bairroElement.Element("populacao").Value)
            };
        }
    }
}
