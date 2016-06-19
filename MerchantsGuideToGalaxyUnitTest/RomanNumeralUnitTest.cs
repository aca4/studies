using MerchantsGuideToGalaxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantsGuideToGalaxyUnitTest
{
    [TestClass]
    public class RomanNumeralUnitTest
    {
        public TestContext testContext { get; set; }

        [TestMethod]
        [DeploymentItem(@"C:\Projects\MerchantsGuideToGalaxy\romanNumberTestData.txt")]
        public void ConvertToNumber()
        {
            string testLine;
            RomanNumeral rn = new RomanNumeral();

            using (StreamReader sr = new StreamReader(@"C:\Projects\MerchantsGuideToGalaxy\romanNumberTestData.txt"))
            {
                int actualValue;
                int expectedValue;

                while ((testLine = sr.ReadLine()) != null)
                {
                    string[] lineSplit = testLine.Split(' ');
                    actualValue = rn.ConvertToNumber(lineSplit[0]);
                    expectedValue = Convert.ToInt32(lineSplit[1]);

                    Assert.AreEqual(expectedValue, actualValue);
                }
            }

            rn.ConvertToNumber("IL");                  
        }
    }
}
