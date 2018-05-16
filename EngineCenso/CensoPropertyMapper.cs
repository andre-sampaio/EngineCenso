using EngineCenso.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineCenso
{
    public class CensoPropertyMapper
    {
        public string CidadesPath { get; private set; }
        public string NomeCidadePath { get; private set; }
        public string HabitantesCidadePath { get; private set; }
        public string BairrosPath { get; private set; }
        public string NomeBairroPath { get; private set; }
        public string HabitantesBairroPath { get; private set; }

        public CensoPropertyMapper(string cidadesPath, string nomeCidadePath, string habitantesCidadePath, string bairrosPath, string nomeBairroPath, string habitantesBairroPath)
        {
            this.CidadesPath = cidadesPath;
            this.NomeCidadePath = nomeCidadePath;
            this.HabitantesCidadePath = habitantesCidadePath;
            this.BairrosPath = bairrosPath;
            this.NomeBairroPath = nomeBairroPath;
            this.HabitantesBairroPath = habitantesBairroPath;
        }

        internal bool CanBeParsedBy(IParser parser)
        {
            return CanParseCidade(parser)
                && CanParseNomeCidade(parser)
                && CanParseHabitantesCidade(parser)
                && CanParseBairros(parser)
                && CanParseNomeBairro(parser)
                && CanParseHabitantesBairro(parser);
        }

        private bool CanParseHabitantesBairro(IParser parser)
        {
            var validExpressionCidadesPath = parser.IsValidExpression(CidadesPath);

            if (!validExpressionCidadesPath)
                return false;

            var cidades = parser.SelectElements(CidadesPath);

            if (!cidades.Any())
                return false;

            var validExpressionBairrosPath = parser.IsValidExpression(BairrosPath);

            if (!validExpressionBairrosPath)
                return false;

            var bairros = cidades.First().SelectElements(BairrosPath);

            if (!bairros.Any())
                return false;

            var validExpressionHabitantesBairroPath = parser.IsValidExpression(HabitantesBairroPath);

            if (!validExpressionHabitantesBairroPath)
                return false;

            return !string.IsNullOrWhiteSpace(bairros.First().SelectString(HabitantesBairroPath));
        }

        private bool CanParseNomeBairro(IParser parser)
        {
            var validExpressionCidadesPath = parser.IsValidExpression(CidadesPath);

            if (!validExpressionCidadesPath)
                return false;

            var cidades = parser.SelectElements(CidadesPath);

            if (!cidades.Any())
                return false;

            var validExpressionBairrosPath = parser.IsValidExpression(BairrosPath);

            if (!validExpressionBairrosPath)
                return false;

            var bairros = cidades.First().SelectElements(BairrosPath);

            if (!bairros.Any())
                return false;

            var validExpressionNomeBairroPath = parser.IsValidExpression(NomeBairroPath);

            if (!validExpressionNomeBairroPath)
                return false;

            return !string.IsNullOrWhiteSpace(bairros.First().SelectString(NomeBairroPath));
        }

        private bool CanParseBairros(IParser parser)
        {
            var validExpressionCidadesPath = parser.IsValidExpression(CidadesPath);

            if (!validExpressionCidadesPath)
                return false;

            var cidades = parser.SelectElements(CidadesPath);

            if (!cidades.Any())
                return false;

            var validExpressionBairrosPath = parser.IsValidExpression(BairrosPath);

            if (!validExpressionBairrosPath)
                return false;

            return cidades.First().SelectElements(BairrosPath).Any();
        }

        private bool CanParseHabitantesCidade(IParser parser)
        {
            var validExpressionCidadesPath = parser.IsValidExpression(CidadesPath);

            if (!validExpressionCidadesPath)
                return false;

            var cidades = parser.SelectElements(CidadesPath);

            if (!cidades.Any())
                return false;

            var validExpressionHabitantesCidadesPath = parser.IsValidExpression(HabitantesCidadePath);

            if (!validExpressionHabitantesCidadesPath)
                return false;

            return int.TryParse(cidades.First().SelectString(HabitantesCidadePath), out int x);
        }

        private bool CanParseNomeCidade(IParser parser)
        {
            var validExpressionCidadesPath = parser.IsValidExpression(CidadesPath);

            if (!validExpressionCidadesPath)
                return false;

            var cidades = parser.SelectElements(CidadesPath);

            if (!cidades.Any())
                return false;

            var validExpressionNomeCidadesPath = parser.IsValidExpression(NomeCidadePath);

            if (!validExpressionNomeCidadesPath)
                return false;

            return !string.IsNullOrWhiteSpace(cidades.First().SelectString(NomeCidadePath));
        }

        private bool CanParseCidade(IParser parser)
        {
            var validExpression = parser.IsValidExpression(CidadesPath);

            if (!validExpression)
                return false;

            return parser.SelectElements(CidadesPath).Any();
        }
    }
}
