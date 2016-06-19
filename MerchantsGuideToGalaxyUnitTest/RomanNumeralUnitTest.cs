using MerchantsGuideToGalaxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MerchantsGuideToGalaxyUnitTest
{
    [TestClass]
    public class RomanNumeralUnitTest
    {
        [TestMethod]
        public void ConvertToNumberTest()
        {
            RomanNumeral rn = new RomanNumeral();

            Assert.AreEqual(8, rn.ConvertToNumber("VIII"));
            Assert.AreEqual(3888, rn.ConvertToNumber("MMMDCCCLXXXVIII"));
            Assert.AreEqual(1983, rn.ConvertToNumber("MCMLXXXIII"));
            Assert.AreEqual(1903, rn.ConvertToNumber("MCMIII"));
            Assert.AreEqual(38, rn.ConvertToNumber("XXXVIII"));
            Assert.AreEqual(39, rn.ConvertToNumber("XXXIX"));

            try
            {
                rn.ConvertToNumber("IVI");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                rn.ConvertToNumber("MCCMLXXXIII");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                rn.ConvertToNumber("IL");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                rn.ConvertToNumber("IIII");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
