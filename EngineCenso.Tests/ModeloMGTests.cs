using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EngineCenso.Tests
{
    [TestFixture]
    public class ModeloMGTests
    {
        public static CensoPropertyMapper mgMapper = new CensoPropertyMapper("/body/region/cities/city", "name", "population", "neighborhoods/neighborhood", "name", "population");

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComUmaCidadeEUmBairro_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComUmaCidadeEDoisBairros_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Fundinho</name>
                        <zone>Centro</zone>
                        <population>19864</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComDuasCidadesEUmBairroCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                </neighborhoods>
            </city>
            <city>
                <name>Uberaba</name>
                <population>300000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Manoel Mendes</name>
                        <zone>Zona Leste</zone>
                        <population>16543</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComUmaRegiaoComDuasCidadesEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Fundinho</name>
                        <zone>Centro</zone>
                        <population>19864</population>
                    </neighborhood>
                </neighborhoods>
            </city>
            <city>
                <name>Uberaba</name>
                <population>300000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Manoel Mendes</name>
                        <zone>Zona Leste</zone>
                        <population>16543</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Santa Maria</name>
                        <zone>Centro</zone>
                        <population>16845</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComUmaCidadeCadaEUmBairro_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
    <region>
        <name>Zona da Mata Mineira</name>
        <cities>
            <city>
                <name>Juiz de Fora</name>
                <population>600000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Costa Carvalho</name>
                        <zone>Região Central</zone>
                        <population>80654</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComUmaCidadeCadaEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Fundinho</name>
                        <zone>Centro</zone>
                        <population>19864</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
    <region>
        <name>Zona da Mata Mineira</name>
        <cities>
            <city>
                <name>Juiz de Fora</name>
                <population>600000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Costa Carvalho</name>
                        <zone>Região Central</zone>
                        <population>80654</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Grajaú</name>
                        <zone>Zona Leste</zone>
                        <population>25006</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComDuasCidadesCadaEUmBairroCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                </neighborhoods>
            </city>
            <city>
                <name>Uberaba</name>
                <population>300000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Manoel Mendes</name>
                        <zone>Zona Leste</zone>
                        <population>16543</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
    <region>
        <name>Zona da Mata Mineira</name>
        <cities>
            <city>
                <name>Juiz de Fora</name>
                <population>600000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Costa Carvalho</name>
                        <zone>Região Central</zone>
                        <population>80654</population>
                    </neighborhood>
                </neighborhoods>
            </city>
            <city>
                <name>Bicas</name>
                <population>14000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Alhadas</name>
                        <zone>Região Central</zone>
                        <population>2300</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }

        [TestCase]
        public void InputNoModeloMG_ComDuasRegioesComDuasCidadesCadaEDoisBairrosCada_RetornaStringNoPadraoDefinido()
        {
            const string input =
@"<body>
    <region>
        <name>Triangulo Mineiro</name>
        <cities>
            <city>
                <name>Uberlandia</name>
                <population>700001</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Santa Monica</name>
                        <zone>Zona Leste</zone>
                        <population>13012</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Fundinho</name>
                        <zone>Centro</zone>
                        <population>19864</population>
                    </neighborhood>
                </neighborhoods>
            </city>
            <city>
                <name>Uberaba</name>
                <population>300000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Manoel Mendes</name>
                        <zone>Zona Leste</zone>
                        <population>16543</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Santa Maria</name>
                        <zone>Centro</zone>
                        <population>16845</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
    <region>
        <name>Zona da Mata Mineira</name>
        <cities>
            <city>
                <name>Juiz de Fora</name>
                <population>600000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Costa Carvalho</name>
                        <zone>Região Central</zone>
                        <population>80654</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Grajaú</name>
                        <zone>Zona Leste</zone>
                        <population>25006</population>
                    </neighborhood>
                </neighborhoods>
            </city>
            <city>
                <name>Bicas</name>
                <population>14000</population>
                <neighborhoods>
                    <neighborhood>
                        <name>Alhadas</name>
                        <zone>Região Central</zone>
                        <population>2300</population>
                    </neighborhood>
                    <neighborhood>
                        <name>Edgar Moreira</name>
                        <zone>Zona Oeste</zone>
                        <population>3201</population>
                    </neighborhood>
                </neighborhoods>
            </city>
        </cities>
    </region>
</body>";

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
            var actualOutput = engine.Process(input, mgMapper);

            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
        }
    }
}
