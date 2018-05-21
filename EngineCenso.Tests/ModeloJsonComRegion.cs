using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.Tests
{
    [TestFixture]
    public class ModeloJsonComRegion
    {
        public static CensoPropertyMapper jsonMgMapper = new CensoPropertyMapper("$.regions[*].cities[*]", "name", "population", "neighborhoods[*]", "name", "population");

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComUmaCidadeEUmBairro_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComUmaCidadeEDoisBairros_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        },
                        {
                            ""name"": ""Fundinho"",
                            ""zone"": ""Centro"",
                            ""population"": 19864
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            },
                            new Bairro ()
                            {
                                Nome = "Fundinho",
                                Habitantes = 19864
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComDuasCidadesEUmBairroCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        }
                    ]
                },
                {
                    ""name"": ""Uberaba"",
                    ""population"": 300000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Manoel Mendes"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 16543
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Uberaba",
                        Habitantes = 300000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Manoel Mendes",
                                Habitantes = 16543
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComDuasCidadesEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        },
                        {
                            ""name"": ""Fundinho"",
                            ""zone"": ""Centro"",
                            ""population"": 19864
                        }
                    ]
                },
                {
                    ""name"": ""Uberaba"",
                    ""population"": 300000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Manoel Mendes"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 16543
                        },
                        {
                            ""name"": ""Santa Maria"",
                            ""zone"": ""Centro"",
                            ""population"": 16845
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            },
                            new Bairro ()
                            {
                                Nome = "Fundinho",
                                Habitantes = 19864
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Uberaba",
                        Habitantes = 300000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Manoel Mendes",
                                Habitantes = 16543
                            },
                            new Bairro ()
                            {
                                Nome = "Santa Maria",
                                Habitantes = 16845
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComUmaCidadeCadaEUmBairro_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        }
                    ]
                }
            ]
        },
        {
            ""name"": ""Zona da Mata Mineira"",
            ""cities"": [
                {
                    ""name"": ""Juiz de Fora"",
                    ""population"": 600000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Costa Carvalho"",
                            ""zone"": ""Região Central"",
                            ""population"": 80654
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Juiz de Fora",
                        Habitantes = 600000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Costa Carvalho",
                                Habitantes = 80654
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComUmaCidadeCadaEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        },
                        {
                            ""name"": ""Fundinho"",
                            ""zone"": ""Centro"",
                            ""population"": 19864
                        }
                    ]
                }
            ]
        },
        {
            ""name"": ""Zona da Mata Mineira"",
            ""cities"": [
                {
                    ""name"": ""Juiz de Fora"",
                    ""population"": 600000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Costa Carvalho"",
                            ""zone"": ""Região Central"",
                            ""population"": 80654
                        },
                        {
                            ""name"": ""Grajaú"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 25006
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            },
                            new Bairro ()
                            {
                                Nome = "Fundinho",
                                Habitantes = 19864
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Juiz de Fora",
                        Habitantes = 600000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Costa Carvalho",
                                Habitantes = 80654
                            },
                            new Bairro ()
                            {
                                Nome = "Grajaú",
                                Habitantes = 25006
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComDuasCidadesCadaEUmBairroCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        }
                    ]
                },
                {
                    ""name"": ""Uberaba"",
                    ""population"": 300000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Manoel Mendes"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 16543
                        }
                    ]
                }
            ]
        },
        {
            ""name"": ""Zona da Mata Mineira"",
            ""cities"": [
                {
                    ""name"": ""Juiz de Fora"",
                    ""population"": 600000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Costa Carvalho"",
                            ""zone"": ""Região Central"",
                            ""population"": 80654
                        }
                    ]
                },
                {
                    ""name"": ""Bicas"",
                    ""population"": 14000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Alhadas"",
                            ""zone"": ""Região Central"",
                            ""population"": 2300
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Uberaba",
                        Habitantes = 300000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Manoel Mendes",
                                Habitantes = 16543
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Juiz de Fora",
                        Habitantes = 600000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Costa Carvalho",
                                Habitantes = 80654
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Bicas",
                        Habitantes = 14000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Alhadas",
                                Habitantes = 2300
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComDuasCidadesCadaEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"{
    ""regions"":[
        {
            ""name"": ""Triangulo Mineiro"",
            ""cities"": [
                {
                    ""name"": ""Uberlandia"",
                    ""population"": 700001,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Santa Monica"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 13012
                        },
                        {
                            ""name"": ""Fundinho"",
                            ""zone"": ""Centro"",
                            ""population"": 19864
                        }
                    ]
                },
                {
                    ""name"": ""Uberaba"",
                    ""population"": 300000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Manoel Mendes"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 16543
                        },
                        {
                            ""name"": ""Santa Maria"",
                            ""zone"": ""Centro"",
                            ""population"": 16845
                        }
                    ]
                }
            ]
        },
        {
            ""name"": ""Zona da Mata Mineira"",
            ""cities"": [
                {
                    ""name"": ""Juiz de Fora"",
                    ""population"": 600000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Costa Carvalho"",
                            ""zone"": ""Região Central"",
                            ""population"": 80654
                        },
                        {
                            ""name"": ""Grajaú"",
                            ""zone"": ""Zona Leste"",
                            ""population"": 25006
                        }
                    ]
                },
                {
                    ""name"": ""Bicas"",
                    ""population"": 14000,
                    ""neighborhoods"": [
                        {
                            ""name"": ""Alhadas"",
                            ""zone"": ""Região Central"",
                            ""population"": 2300
                        },
                        {
                            ""name"": ""Edgar Moreira"",
                            ""zone"": ""Zona Oeste"",
                            ""population"": 3201
                        }
                    ]
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
                        Cidade = "Uberlandia",
                        Habitantes = 700001,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Santa Monica",
                                Habitantes = 13012
                            },
                            new Bairro ()
                            {
                                Nome = "Fundinho",
                                Habitantes = 19864
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Uberaba",
                        Habitantes = 300000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Manoel Mendes",
                                Habitantes = 16543
                            },
                            new Bairro ()
                            {
                                Nome = "Santa Maria",
                                Habitantes = 16845
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Juiz de Fora",
                        Habitantes = 600000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Costa Carvalho",
                                Habitantes = 80654
                            },
                            new Bairro ()
                            {
                                Nome = "Grajaú",
                                Habitantes = 25006
                            }
                        }
                    },
                    new Result()
                    {
                        Cidade = "Bicas",
                        Habitantes = 14000,
                        Bairros = new List<Bairro>()
                        {
                            new Bairro ()
                            {
                                Nome = "Alhadas",
                                Habitantes = 2300
                            },
                            new Bairro ()
                            {
                                Nome = "Edgar Moreira",
                                Habitantes = 3201
                            }
                        }
                    }
                }
            };

            EngineCenso engine = new EngineCenso();
            var actualOutput = engine.Process(input, jsonMgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }
    }
}
