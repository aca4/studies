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
        public void IsIntergalaticDigit()
        {
            string sentence = "lallaa lelele is I";
            Line line = new Line(sentence.ToLower());
            Assert.IsTrue(Regex.IsMatch(sentence, line.IntergalaticDigitPattern));
        }

        [TestMethod]
        public void IsMetal()
        {
            string sentence = "glob glob Silver is 34 credits";
            Line line = new Line(sentence.ToLower());
            Assert.IsTrue(Regex.IsMatch(sentence, line.MetalPattern));
        }

        [TestMethod]
        public void IsHowMuchQuestion()
        {
            string sentence = "how much is pish tegj glob glob ?";
            Line line = new Line(sentence.ToLower());
            Assert.IsTrue(Regex.IsMatch(sentence, line.HowMuchQuestionPattern));
        }

        [TestMethod]
        public void IsHowManyQuestion()
        {
            string sentence = "how many Credits is glob prok Iron ?";
            Line line = new Line(sentence.ToLower());
            Assert.IsTrue(Regex.IsMatch(sentence, line.HowMuchQuestionPattern));
        } 
        
        [TestMethod]
        public void IsValidInput()
        {

        }       
    }
}
