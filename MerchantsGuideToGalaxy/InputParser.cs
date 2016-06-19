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
        private Dictionary<string, double> metalsDictionary;
        private string invalidQuery = "I have no idea what you are talking about";
        RomanNumeral romanNumeral;
        

        public InputParser()
        {
            intergalaticDictionary = new Dictionary<string, string>();
            metalsDictionary = new Dictionary<string, double>();
            romanNumeral = new RomanNumeral();
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
                    AnswerQuestion(line.Text);
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

            //if (!intergalaticDictionary.ContainsKey(value))
            //{
            //    intergalaticDictionary.Add(value, romanNumeral.GetNumberFromRomanChar(value[0]).ToString());
            //}
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
            string romanNumber = "";

            foreach (var item in intergalaticNumbers)
            {
                var romanChar = intergalaticDictionary[item];                
                romanNumber += romanChar;
            }

            int intergalaticNumbersConverted = romanNumeral.ConvertToNumber(romanNumber);

            double numberOfCredits = Convert.ToDouble(Regex.Match(sentence, @"\d+").Value);

            double metalValue = numberOfCredits / intergalaticNumbersConverted;

            metalsDictionary.Add(metal, metalValue);         
        }

        public void AnswerQuestion(string sentence)
        {
            Regex regex = new Regex(@"(?<group1>\d+ )+(?<group2>\w+)");
            Match match = regex.Match("3231 234 kalala");
            string group1 = match.Groups["group1"].Value;
            string group2 = match.Groups["group2"].Value;

            Regex regex2 = new Regex(@"^(?<group1>how many Credits is) (?<group2>\w+ )+\?$");
            Match match2 = regex2.Match(sentence);
            group1 = match2.Groups["group1"].Value;
            group2 = match2.Groups["group2"].Value;

            Regex regex3 = new Regex(@"^(?<group1>how many Credits is) (\w+ )+(?<group3>[A-Z][a-z]\w+) \?$");
            Match match3 = regex3.Match(sentence);
            group1 = match3.Groups["group1"].Value;
            group2 = match3.Groups["group2"].Value;
            string group3 = match3.Groups["group3"].Value;

            string[] stringSeparators = new string[] { "is" };
            string[] pqp = sentence.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            string t = pqp[1].Trim();
            t = t.Substring(0, t.Length - 1);
            t = t.Trim();

            string[] pqp2 = t.Split(' ');

            double totalValue = 0;

           
            string romanNumber = "";
            for(int i =0; i < pqp2.Length - 1; i++)
            {
                var romanChar = intergalaticDictionary[pqp2[i]];
                romanNumber += romanChar;
            }

            int intergalaticNumbersConverted = romanNumeral.ConvertToNumber(romanNumber);
            double metalValue = metalsDictionary[pqp2[pqp2.Length - 1]];

            totalValue = intergalaticNumbersConverted * metalValue;

            Console.WriteLine(totalValue);

            //Regex regex = new Regex(@"\bis\b");
            //string[] tokens = regex.Split(sentence);
            //double response = 0;

            //Match firstPartBeforeIs = Regex.Match(tokens[1], @"(\w+ )+");
            //firstPartBeforeIs = Regex.Match(sentence, @"^how many Credits is (\w+ )+([A-Z][a-z]\w+) \?$");
            ////@"^how many Credits isd+ (\w+ )+([A-Z][a-z]\w+) \?$"
            ////TODO: check groups on match
            //string what = firstPartBeforeIs.Groups["groupMetal"].Value;

            //string[] values = firstPartBeforeIs.Value.Trim().Split(' ');

            //foreach (var token in values)
            //{
            //    string vixe = intergalaticDictionary[token];

            //    //if (romanNumeral.r)


            //}
        }
    }
}
