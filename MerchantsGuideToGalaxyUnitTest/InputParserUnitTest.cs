using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using MerchantsGuideToGalaxy;

namespace MerchantsGuideToGalaxyUnitTest
{
    [TestClass]
    public class InputParserUnitTest
    {
        [TestMethod]
        public void IsUnit()
        {
            string sentence = "lallaa lelele is I";
            InputParser inputParser = new InputParser();
            Assert.IsTrue(Regex.IsMatch(sentence, inputParser.UnitPattern));
        }

        [TestMethod]
        public void IsMetal()
        {
            string sentence = "glob glob Silver is 34 Credits";
            InputParser inputParser = new InputParser();
            Assert.IsTrue(Regex.IsMatch(sentence, inputParser.MetalPattern));
        }

        [TestMethod]
        public void IsQuestion()
        {
            string sentence = "how much is pish tegj glob glob ?";
            InputParser inputParser = new InputParser();
            Assert.IsTrue(Regex.IsMatch(sentence, inputParser.QuestionPattern));
        }

        [TestMethod]
        public void TestTranslate()
        {
            string expectedResult = "I have no idea what you are talking about";
            InputParser inputParser = new InputParser();
            Assert.AreEqual(expectedResult, inputParser.Translate());           
        }
    }
}
