using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EngineCenso.Tests
{
    [TestFixture]
    public class ModeloMGTests
    {
        public CensoMapper mgMapper = new CensoMapper("/body/region/cities/city", "name", "population", "neighborhoods/neighborhood", "name", "population");

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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                },
                {
                    ""nome"":""Fundinho"",
                    ""habitantes"":19864
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                }
            ]
        },
        {
            ""cidade"":""Uberaba"",
            ""habitantes"":300000,
            ""bairros"":[
                {
                    ""nome"":""Manoel Mendes"",
                    ""habitantes"":16543
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                },
                {
                    ""nome"":""Fundinho"",
                    ""habitantes"":19864
                }
            ]
        },
        {
            ""cidade"":""Uberaba"",
            ""habitantes"":300000,
            ""bairros"":[
                {
                    ""nome"":""Manoel Mendes"",
                    ""habitantes"":16543
                },
                {
                    ""nome"":""Santa Maria"",
                    ""habitantes"":16845
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                }
            ]
        },
        {
            ""cidade"":""Juiz de Fora"",
            ""habitantes"":600000,
            ""bairros"":[
                {
                    ""nome"":""Costa Carvalho"",
                    ""habitantes"":80654
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                },
                {
                    ""nome"":""Fundinho"",
                    ""habitantes"":19864
                }
            ]
        },
        {
            ""cidade"":""Juiz de Fora"",
            ""habitantes"":600000,
            ""bairros"":[
                {
                    ""nome"":""Costa Carvalho"",
                    ""habitantes"":80654
                },
                {
                    ""nome"":""Grajaú"",
                    ""habitantes"":25006
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                }
            ]
        },
        {
            ""cidade"":""Uberaba"",
            ""habitantes"":300000,
            ""bairros"":[
                {
                    ""nome"":""Manoel Mendes"",
                    ""habitantes"":16543
                }
            ]
        },
        {
            ""cidade"":""Juiz de Fora"",
            ""habitantes"":600000,
            ""bairros"":[
                {
                    ""nome"":""Costa Carvalho"",
                    ""habitantes"":80654
                }
            ]
        },
        {
            ""cidade"":""Bicas"",
            ""habitantes"":14000,
            ""bairros"":[
                {
                    ""nome"":""Alhadas"",
                    ""habitantes"":2300
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Uberlandia"",
            ""habitantes"":700001,
            ""bairros"":[
                {
                    ""nome"":""Santa Monica"",
                    ""habitantes"":13012
                },
                {
                    ""nome"":""Fundinho"",
                    ""habitantes"":19864
                }
            ]
        },
        {
            ""cidade"":""Uberaba"",
            ""habitantes"":300000,
            ""bairros"":[
                {
                    ""nome"":""Manoel Mendes"",
                    ""habitantes"":16543
                },
                {
                    ""nome"":""Santa Maria"",
                    ""habitantes"":16845
                }
            ]
        },
        {
            ""cidade"":""Juiz de Fora"",
            ""habitantes"":600000,
            ""bairros"":[
                {
                    ""nome"":""Costa Carvalho"",
                    ""habitantes"":80654
                },
                {
                    ""nome"":""Grajaú"",
                    ""habitantes"":25006
                }
            ]
        },
        {
            ""cidade"":""Bicas"",
            ""habitantes"":14000,
            ""bairros"":[
                {
                    ""nome"":""Alhadas"",
                    ""habitantes"":2300
                },
                {
                    ""nome"":""Edgar Moreira"",
                    ""habitantes"":3201
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input, new List<CensoMapper> { mgMapper });
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
        }
    }
}
