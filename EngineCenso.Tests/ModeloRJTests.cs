using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EngineCenso.Tests
{
    [TestFixture]
    public class ModeloRJTests
    {
        public static CensoMapping rjMapper = new CensoMapping("/corpo/cidade", "nome", "populacao", "bairros/bairro", "nome", "populacao");

        [TestCase]
        public void InputNoModeloRJ_ComUmaCidadeEUmBairro_RetornaStringNoPadraoDefinido()
        {
            const string input = 
@"<corpo>
    <cidade>
        <nome>Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>
        </bairros>
    </cidade>
</corpo>";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio de Janeiro",
                        Habitantes = 10345678,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Tijuca",
                                Habitantes = 135678
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso(new List<CensoMapping> { ModeloACTests.acMapper, ModeloRJTests.rjMapper, ModeloMGTests.mgMapper });
            var actualOutput = engine.Process(input);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloRJ_ComUmaCidadeEDoisBairros_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<corpo>
    <cidade>
        <nome>Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>
            <bairro>
                <nome>Botafogo</nome>
                <regiao>Zona Sul</regiao>
                <populacao>12456</populacao>
            </bairro>
        </bairros>
    </cidade>
</corpo>";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio de Janeiro",
                        Habitantes = 10345678,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Tijuca",
                                Habitantes = 135678
                            },
                            new Bairro ()
                            {
                                Nome = "Botafogo",
                                Habitantes = 12456
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso(new List<CensoMapping> { ModeloACTests.acMapper, ModeloRJTests.rjMapper, ModeloMGTests.mgMapper });
            var actualOutput = engine.Process(input);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloRJ_ComDuasCidadesEUmBairroCadaUma_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<corpo>
    <cidade>
        <nome>Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>
        </bairros>
    </cidade>
    <cidade>
        <nome>Petrópolis</nome>
        <populacao>300000</populacao>
        <bairros>
            <bairro>
                <nome>Mosela</nome>
                <regiao>Central</regiao>
                <populacao>21234</populacao>
            </bairro>
        </bairros>
    </cidade>
</corpo>";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio de Janeiro",
                        Habitantes = 10345678,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Tijuca",
                                Habitantes = 135678
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Petrópolis",
                        Habitantes = 300000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Mosela",
                                Habitantes = 21234
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso(new List<CensoMapping> { ModeloACTests.acMapper, ModeloRJTests.rjMapper, ModeloMGTests.mgMapper });
            var actualOutput = engine.Process(input);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloRJ_ComDuasCidadesEDoisBairrosCadaUma_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<corpo>
    <cidade>
        <nome>Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>
            <bairro>
                <nome>Botafogo</nome>
                <regiao>Zona Sul</regiao>
                <populacao>12456</populacao>
            </bairro>
        </bairros>
    </cidade>
    <cidade>
        <nome>Petrópolis</nome>
        <populacao>300000</populacao>
        <bairros>
            <bairro>
                <nome>Mosela</nome>
                <regiao>Central</regiao>
                <populacao>21234</populacao>
            </bairro>
            <bairro>
                <nome>Retiro</nome>
                <regiao>Central</regiao>
                <populacao>51368</populacao>
            </bairro>
        </bairros>
    </cidade>
</corpo>";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio de Janeiro",
                        Habitantes = 10345678,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Tijuca",
                                Habitantes = 135678
                            },
                            new Bairro ()
                            {
                                Nome = "Botafogo",
                                Habitantes = 12456
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Petrópolis",
                        Habitantes = 300000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Mosela",
                                Habitantes = 21234
                            },
                            new Bairro ()
                            {
                                Nome = "Retiro",
                                Habitantes = 51368
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso(new List<CensoMapping> { ModeloACTests.acMapper, ModeloRJTests.rjMapper, ModeloMGTests.mgMapper });
            var actualOutput = engine.Process(input);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

    }
}
