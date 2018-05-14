using EngineCenso.Parser;
using System.Collections.Generic;

namespace EngineCenso
{
    internal class CensoProcesser
    {
        IParser parser;
        CensoMapping mapper;

        internal CensoProcesser(IParser parser, CensoMapping mapper)
        {
            this.parser = parser;
            this.mapper = mapper;
        }

        internal CensoOutput Process()
        {
            var output = new CensoOutput
            {
                Result = ParseCidades(parser.SelectElements(mapper.CidadesPath))
            };

            return output;
        }

        internal List<Result> ParseCidades(IEnumerable<IParserElement> cidadeElements)
        {
            List<Result> results = new List<Result>();
            foreach (var element in cidadeElements)
            {
                results.Add(ParseCidade(element));
            }

            return results;
        }

        internal Result ParseCidade(IParserElement cidadeElement)
        {
            return new Result()
            {
                Cidade = cidadeElement.SelectString(mapper.NomeCidadePath),
                Habitantes = cidadeElement.SelectInt(mapper.HabitantesCidadePath),
                Bairros = ParseBairros(cidadeElement.SelectElements(mapper.BairrosPath))
            };
        }

        internal List<Bairro> ParseBairros(IEnumerable<IParserElement> bairroElements)
        {
            List<Bairro> bairros = new List<Bairro>();
            foreach (var element in bairroElements)
            {
                bairros.Add(ParseBairro(element));
            }
            return bairros;
        }

        internal Bairro ParseBairro(IParserElement bairroElement)
        {
            return new Bairro()
            {
                Nome = bairroElement.SelectString(mapper.NomeBairroPath),
                Habitantes = bairroElement.SelectInt(mapper.HabitantesBairroPath)
            };
        }
    }
}
