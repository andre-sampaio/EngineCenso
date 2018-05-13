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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Rio de Janeiro"",
            ""habitantes"":10345678,
            ""bairros"":[
                {
                    ""nome"":""Tijuca"",
                    ""habitantes"":135678
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input);
            var actualOutput = engine.Process();

            // Don't care for spaces, tabs and new lines
            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Rio de Janeiro"",
            ""habitantes"":10345678,
            ""bairros"":[
                {
                    ""nome"":""Tijuca"",
                    ""habitantes"":135678
                },
                {
                    ""nome"":""Botafogo"",
                    ""habitantes"":12456
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input);
            var actualOutput = engine.Process();

            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Rio de Janeiro"",
            ""habitantes"":10345678,
            ""bairros"":[
                {
                    ""nome"":""Tijuca"",
                    ""habitantes"":135678
                }
            ]
        },
        {
            ""cidade"":""Petrópolis"",
            ""habitantes"":300000,
            ""bairros"":[
                {
                    ""nome"":""Mosela"",
                    ""habitantes"":21234
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input);
            var actualOutput = engine.Process();

            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
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

            const string expectedOutput =
@"{
""result"":[
        {
            ""cidade"":""Rio de Janeiro"",
            ""habitantes"":10345678,
            ""bairros"":[
                {
                    ""nome"":""Tijuca"",
                    ""habitantes"":135678
                },
                {
                    ""nome"":""Botafogo"",
                    ""habitantes"":12456
                }
            ]
        },
        {
            ""cidade"":""Petrópolis"",
            ""habitantes"":300000,
            ""bairros"":[
                {
                    ""nome"":""Mosela"",
                    ""habitantes"":21234
                },
                {
                    ""nome"":""Retiro"",
                    ""habitantes"":51368
                }
            ]
        }
    ]
}";

            EngineCenso engine = new EngineCenso(input);
            var actualOutput = engine.Process();

            string formatedExpectedOutput = Regex.Replace(expectedOutput, "\\s", "");
            string formatedActualOutput = Regex.Replace(actualOutput, "\\s", "");

            Assert.AreEqual(formatedExpectedOutput, formatedActualOutput);
        }

    }
}
