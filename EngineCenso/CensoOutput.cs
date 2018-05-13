using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso
{
    public class CensoOutput
    {
        public IEnumerable<Result> Result { get; set; }
    }

    public class Result
    {
        public string Cidade { get; set; }
        public int Habitantes { get; set; }
        public IEnumerable<Bairro> Bairros { get; set; }
    }

    public class Bairro
    {
        public string Nome { get; set; }
        public int Habitantes { get; set; }
    }
}
