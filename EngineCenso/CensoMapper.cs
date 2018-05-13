using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso
{
    public class CensoMapper
    {
        internal string CidadesPath { get; set; }
        internal string NomeCidadePath { get; set; }
        internal string HabitantesCidadePath { get; set; }
        internal string BairrosPath { get; set; }
        internal string NomeBairroPath { get; set; }
        internal string HabitantesBairroPath { get; set; }

        public CensoMapper(string cidadesPath, string nomeCidadePath, string habitantesCidadePath, string bairrosPath, string nomeBairroPath, string habitantesBairroPath)
        {
            this.CidadesPath = cidadesPath;
            this.NomeCidadePath = nomeCidadePath;
            this.HabitantesCidadePath = habitantesCidadePath;
            this.BairrosPath = bairrosPath;
            this.NomeBairroPath = nomeBairroPath;
            this.HabitantesBairroPath = habitantesBairroPath;
        }

        public bool MatchesInput(string input)
        {
            XDocument document = XDocument.Parse(input);

            return true;
        }

        private bool CidadesPathIsValid(XDocument document)
        {
            return document.XPathSelectElements(CidadesPath).Any();
        }

        private bool NomeCidadePathIsValid(XDocument document)
        {
            return document.XPathSelectElements($"{CidadesPath}/{NomeCidadePath}").Any();
        }

        private bool HabitantesCidadePathIsValid(XDocument document)
        {
            var elements = document.XPathSelectElements($"{CidadesPath}/{HabitantesCidadePath}");

            if (!elements.Any())
                return false;

            return int.TryParse(elements.First().Value, out int x);
        }

        private bool BairrosPathIsValid(XDocument document)
        {
            return document.XPathSelectElements($"{CidadesPath}/{BairrosPath}").Any();
        }

        private bool NomeBairroPathIsValid(XDocument document)
        {
            return document.XPathSelectElements($"{CidadesPath}/{BairrosPath}/{NomeBairroPath}").Any();
        }

        private bool HabitantesBairroPathIsValid(XDocument document)
        {
            var elements = document.XPathSelectElements($"{CidadesPath}/{BairrosPath}/{HabitantesBairroPath}");

            if (!elements.Any())
                return false;

            return int.TryParse(elements.First().Value, out int x);
        }
    }
}
