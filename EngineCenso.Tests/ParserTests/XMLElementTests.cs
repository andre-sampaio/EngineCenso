using EngineCenso.Parser.XML;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EngineCenso.Tests.ParserTests
{
    [TestFixture]
    public class XMLElementTests
    {
        [TestCase]
        public void SelectElements_WithAValidPathCorrespondingToNoElements_ReturnsAnEmptySet()
        {
            string xml =
@"<testElement>123</testElement>";
            string path = "notAnElement";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            var actual = element.SelectElements(path);

            Assert.That(actual, Is.Empty);
        }

        [TestCase]
        public void SelectElements_WithAValidPathCorrespondingToOneElement_ReturnsASetWithOneElement()
        {
            string xml =
@"<testElement>123</testElement>";
            string path = "/";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            var actual = element.SelectElements(path);

            Assert.That(actual, Has.Count.EqualTo(1));
        }

        [TestCase]
        public void SelectElements_WithAValidPathCorrespondingToTwoElements_ReturnsASetWithTwoElements()
        {
            string xml = 
@"<root>
    <testElement>1234</testElement>
    <testElement>45789</testElement>
</root>";

            string path = "/testElement";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            var actual = element.SelectElements(path);

            Assert.That(actual, Has.Count.EqualTo(2));
        }

        [TestCase]
        public void SelectElements_WithInvalidPath_ThrowsXPathException()
        {
            string xml =
@"<testElement>1234</testElement>";

            string path = "[[[";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            Assert.Throws<XPathException>(() => element.SelectElements(path));
        }

        [TestCase]
        public void SelectInt_WithPathToInt_ReturnsThatInt()
        {
            int expected = 1579;
            string xml =
$@"<testElement>{ expected }</testElement>";

            string path = "/";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            var actual = element.SelectInt(path);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void SelectInt_WithPathToNonInt_ThrowsFormatException()
        {
            string xml =
$@"<testElement>dsasdsa</testElement>";
            string path = "/";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            Assert.Throws<FormatException>(() => element.SelectInt(path));
        }

        [TestCase]
        public void SelectInt_WithPathToNonExistentItem_ThrowsNullReferenceException()
        {
            string xml =
$@"<testElement>dsasdsa</testElement>";
            string path = "nonExistent";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            Assert.Throws<NullReferenceException>(() => element.SelectInt(path));
        }

        [TestCase]
        public void SelectString_WithPathToString_ReturnsThatString()
        {
            string expected = "testString";
            string xml =
$@"<testElement>{ expected }</testElement>";

            string path = "/";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            var actual = element.SelectString(path);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void SelectString_WithPathToNonExistentItem_ThrowsNullReferenceException()
        {
            string xml =
$@"<testElement>123</testElement>";
            string path = "nonExistent";

            XMLElement element = new XMLElement(XElement.Parse(xml));

            Assert.Throws<NullReferenceException>(() => element.SelectString(path));
        }
    }
}
