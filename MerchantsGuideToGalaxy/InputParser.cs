using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace MerchantsGuideToGalaxy
{
    public class InputParser
    {        
        private Dictionary<string, string> intergalaticDictionary;
        private string invalidQuery = "I have no idea what you are talking about";

        public InputParser()
        {
            intergalaticDictionary = new Dictionary<string, string>();
        }

        /// <summary>
        /// Reads lines from input files and process them
        /// </summary>
        /// <param name="filePath">The path of the input file</param>
        public void Translate(string filePath)
        {
            string sentence;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while ((sentence = sr.ReadLine()) != null)
                    {
                        Line line = new Line(sentence);
                        ParseLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading input file: " + ex.Message);
            }
        }

        /// <summary>
        /// Parses line according to its type
        /// </summary>
        /// <param name="line">Line from input file</param>
        private void ParseLine(Line line)
        {
            switch (line.Type)
            {
                case LineType.Unit:
                    DefineRomanNumeral(line.Text);
                    break;
                case LineType.Metal:
                    DefineCredits(line.Text);
                    break;
                case LineType.Question:
                    //answer question
                    break;
                case LineType.Invalid:
                    Console.WriteLine(invalidQuery);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adds entry into dictionary
        /// </summary>
        /// <param name="sentence"></param>
        public void DefineRomanNumeral(string sentence) //TODO: change name
        {
            Regex regex = new Regex(@"\bis\b");
            string[] tokens = regex.Split(sentence);

            string key = tokens[0].Trim();
            string value = tokens[1].Trim();

            if (!intergalaticDictionary.ContainsKey(key))
            {

                intergalaticDictionary.Add(key, value);
            }
        }

        //TODO: change name
        public void DefineCredits(string sentence)
        {
            Match firstPartBeforeIs = Regex.Match(sentence, @"^(\w+ )+(Silver|Gold|Iron)");

            string matchString = firstPartBeforeIs.Value;

            string metal = Regex.Match(firstPartBeforeIs.Value, @"(Silver|Gold|Iron)").Value;

            string temp = matchString.Replace(metal, "").Trim();

            string[] intergalaticNumbers = temp.Split(' ');

            RomanNumeral romanNumeral = new RomanNumeral();
            int sumOfRomanNumebers = 0;
            string romanNumber = "";

            foreach (var item in intergalaticNumbers)
            {
                var romanChar = intergalaticDictionary[item];
                //int arabicNumber = romanNumeral.GetNumberFromRomanChar(romanChar[0]);
                //sumOfRomanNumebers += arabicNumber;
                romanNumber += romanChar;
            }

            //foreach (var item in intergalaticNumbers)
            //{
            //    var value = intergalaticDictionary[item];
            //    int temp2= romanNumeral.ConvertToNumber(value);
            //    sumOfRomanNumebers += temp2;
            //    romanNumber += value;
            //}

            int intergalaticNumbersConverted = romanNumeral.ConvertToNumber(romanNumber);

            double numberOfCredits = Convert.ToDouble(Regex.Match(sentence, @"\d+").Value);

            double metalValue = numberOfCredits / sumOfRomanNumebers;

            intergalaticDictionary.Add(metal, Convert.ToString(metalValue, CultureInfo.InvariantCulture));         
        }        
    }
}
