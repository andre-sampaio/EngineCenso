using EngineCenso.Parser.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.Tests.ParserTests
{
    [TestFixture]
    public class JsonElementParserTests
    {
        [TestCase]
        public void SelectElements_WithAValidPathCorrespondingToNoElements_ReturnsAnEmptySet()
        {
            string json =
@"{
    ""testElement"": 123
}";
            string path = "$.notAnElement";

            JsonElement element = new JsonElement(JToken.Parse(json));

            var actual = element.SelectElements(path);

            Assert.That(actual, Is.Empty);
        }

        [TestCase]
        public void SelectElements_WithAValidPathCorrespondingToOneElement_ReturnsASetWithOneElement()
        {
            string json =
@"{
    ""testElement"": 123
}";
            string path = "$.testElement";

            JsonElement element = new JsonElement(JToken.Parse(json));

            var actual = element.SelectElements(path);

            Assert.That(actual, Has.Count.EqualTo(1));
        }

        [TestCase]
        public void SelectElements_WithAValidPathCorrespondingToTwoElements_ReturnsASetWithTwoElements()
        {
            string json =
@"{
    ""testElement"": [
        1234,
        45789
    ]
}";
            string path = "$.testElement[*]";

            JsonElement element = new JsonElement(JToken.Parse(json));

            var actual = element.SelectElements(path);

            Assert.That(actual, Has.Count.EqualTo(2));
        }

        [TestCase]
        public void SelectElements_WithInvalidPath_ThrowsJsonException()
        {
            string json =
@"{
    ""testElement"": [
        1234,
        45789
    ]
}";
            string path = "[[[";

            JsonElement element = new JsonElement(JToken.Parse(json));

            Assert.Throws<JsonException>(() => element.SelectElements(path));
        }

        [TestCase]
        public void SelectInt_WithPathToInt_ReturnsThatInt()
        {
            int expected = 1579;
            string json =
@"{
    ""testElement"": " + expected + @"
}";
            string path = "$.testElement";

            JsonElement element = new JsonElement(JToken.Parse(json));

            var actual = element.SelectInt(path);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void SelectInt_WithPathToIntOnQuotes_ReturnsThatInt()
        {
            int expected = 1579;
            string json =
@"{
    ""testElement"": """ + expected + @"""
}";
            string path = "$.testElement";

            JsonElement element = new JsonElement(JToken.Parse(json));

            var actual = element.SelectInt(path);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void SelectInt_WithPathToNonInt_ThrowsFormatException()
        {
            string json =
@"{
    ""testElement"": ""sadsadsa""
}";
            string path = "$.testElement";

            JsonElement element = new JsonElement(JToken.Parse(json));

            Assert.Throws<FormatException>(() => element.SelectInt(path));
        }
    }
}
