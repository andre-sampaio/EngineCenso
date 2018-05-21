using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EngineCenso.Tests
{
    [TestFixture]
    public class ModeloACTests
    {
        public static CensoPropertyMapper acMapper = new CensoPropertyMapper("$.cities[*]", "name", "population", "neighborhoods[*]", "name", "population");

        [TestCase]
        public void InputNoModeloAC_ComUmaCidadeEUmBairro_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""cities"":[
        {
            ""name"":""Rio Branco"",
            ""population"":576589,
            ""neighborhoods"":[
                {
                    ""name"":""Habitasa"",
                    ""population"":7503
                }
            ]
        }
    ]
}";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio Branco",
                        Habitantes = 576589,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Habitasa",
                                Habitantes = 7503
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, acMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloAC_ComUmaCidadeEDoisBairros_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""cities"":[
        {
            ""name"":""Rio Branco"",
            ""population"":576589,
            ""neighborhoods"":[
                {
                    ""name"":""Habitasa"",
                    ""population"":7503
                },
                {
                    ""name"":""Areial"",
                    ""population"":5310
                }
            ]
        }
    ]
}";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio Branco",
                        Habitantes = 576589,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Habitasa",
                                Habitantes = 7503
                            },
                            new Bairro ()
                            {
                                Nome = "Areial",
                                Habitantes = 5310
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, acMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloAC_ComDuasCidadesEUmBairroCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""cities"":[
        {
            ""name"":""Rio Branco"",
            ""population"":576589,
            ""neighborhoods"":[
                {
                    ""name"":""Habitasa"",
                    ""population"":7503
                }
            ]
        },
        {
            ""name"":""Epitaciolândia"",
            ""population"":13434,
            ""neighborhoods"":[
                {
                    ""name"":""Beira Rio"",
                    ""population"":1230
                }
            ]
        }
    ]
}";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio Branco",
                        Habitantes = 576589,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Habitasa",
                                Habitantes = 7503
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Epitaciolândia",
                        Habitantes = 13434,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Beira Rio",
                                Habitantes = 1230
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, acMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloAC_ComDuasCidadesEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""cities"":[
        {
            ""name"":""Rio Branco"",
            ""population"":576589,
            ""neighborhoods"":[
                {
                    ""name"":""Habitasa"",
                    ""population"":7503
                },
                {
                    ""name"":""Areial"",
                    ""population"":5310
                }
            ]
        },
        {
            ""name"":""Epitaciolândia"",
            ""population"":13434,
            ""neighborhoods"":[
                {
                    ""name"":""Beira Rio"",
                    ""population"":1230
                },
                {
                    ""name"":""Cetel"",
                    ""population"":3157
                }
            ]
        }
    ]
}";

            CensoOutput expectedOutput = new CensoOutput()
            {
                Result = new List<Result>()
                {
                    new Result()
                    {
                        Cidade = "Rio Branco",
                        Habitantes = 576589,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Habitasa",
                                Habitantes = 7503
                            },
                            new Bairro ()
                            {
                                Nome = "Areial",
                                Habitantes = 5310
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Epitaciolândia",
                        Habitantes = 13434,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Beira Rio",
                                Habitantes = 1230
                            },
                            new Bairro ()
                            {
                                Nome = "Cetel",
                                Habitantes = 3157
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, acMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }
    }
}
