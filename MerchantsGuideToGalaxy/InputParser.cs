using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MerchantsGuideToGalaxy
{
    public class InputParser
    {        
        private Dictionary<string, string> intergalaticDictionary;
        private Dictionary<string, double> metalsDictionary;
        private RomanNumeral romanNumeral;
        private string invalidQuery = "I have no idea what you are talking about";

        public InputParser()
        {
            intergalaticDictionary = new Dictionary<string, string>();
            metalsDictionary = new Dictionary<string, double>();
            romanNumeral = new RomanNumeral();
        }

        /// <summary>
        /// Reads lines from input file and process them
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
                case LineType.IntergalaticDigit:
                    DefineIntergalaticDigit(line.Text);
                    break;
                case LineType.Metal:
                    DefineMetalValue(line.Text);
                    break;
                case LineType.HowManyQuestion:
                    AnswerHowManyQuestion(line.Text);
                    break;
                case LineType.HowMuchQuestion:
                    AnswerHowMuchQuestion(line.Text);
                    break;
                case LineType.Invalid:
                    Console.WriteLine(invalidQuery);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Retrieves intergalatic digit value and adds it into intergalaticDictionary
        /// </summary>
        /// <param name="sentence"></param>
        public void DefineIntergalaticDigit(string sentence)
        {
            string[] substrings = Util.SplitByIs(sentence);

            string key = substrings[0];
            string value = substrings[1];

            if (!intergalaticDictionary.ContainsKey(key))
            {
                intergalaticDictionary.Add(key, value);
            }            
        }

        /// <summary>
        /// Calculates metal value and adds its into metalsDictionary
        /// </summary>
        /// <param name="sentence"></param>
        public void DefineMetalValue(string sentence)
        {
            string[] substrings = Util.SplitByIs(sentence);

            Regex regex = new Regex(@"(?<groupInterNum>(\w+ )+)+(?<groupMetal>\w+)");
            MatchCollection matches = regex.Matches(substrings[0]);
            IEnumerator matchesEnum = matches.GetEnumerator();
            string groupIntergalaticNumbers = "";
            string groupMetal = "";            

            if (matchesEnum.MoveNext())
            {
                Match match = (Match)matchesEnum.Current;
                groupIntergalaticNumbers = match.Groups["groupInterNum"].Value.Trim();
                groupMetal = match.Groups["groupMetal"].Value;
            }

            string[] intergalaticNumbers = groupIntergalaticNumbers.Split(' ');
            string romanNumber = "";
            foreach (var item in intergalaticNumbers)
            {
                var romanChar = intergalaticDictionary[item];
                romanNumber += romanChar;
            }

            int intergalaticNumbersConverted = romanNumeral.ConvertToNumber(romanNumber);

            double numberOfCredits = Convert.ToDouble(Regex.Match(sentence, @"\d+").Value);

            double metalValue = numberOfCredits / intergalaticNumbersConverted;

            metalsDictionary.Add(groupMetal, metalValue);         
        }

        /// <summary>
        /// Calculates the answer for a question of type "how many"
        /// </summary>
        /// <param name="sentence"></param>
        public void AnswerHowManyQuestion(string sentence)
        {
            string[] substrings = Util.SplitByIs(sentence);

            Regex regex = new Regex(@"(?<groupInterNum>(\w+ )+)+(?<groupMetal>\w+) \?$");
            MatchCollection matches = regex.Matches(substrings[1]);
            IEnumerator matchesEnum = matches.GetEnumerator();
            string groupIntergalaticNumbers = "";
            string groupMetal = "";
            
            if (matchesEnum.MoveNext())
            {
                Match match = (Match)matchesEnum.Current;
                groupIntergalaticNumbers = match.Groups["groupInterNum"].Value.Trim();
                groupMetal = match.Groups["groupMetal"].Value;
            }           

            string romanNumber = "";           
            string[] intergalaticNumbers = groupIntergalaticNumbers.Split(' ');
            
            for (int i = 0; i < intergalaticNumbers.Length; i++)
            {
                romanNumber += intergalaticDictionary[intergalaticNumbers[i]];
            }

            int intergalaticNumbersValue = romanNumeral.ConvertToNumber(romanNumber);
            double metalValue = metalsDictionary[groupMetal];
            
            double answer = intergalaticNumbersValue * metalValue;
            string outputMessage = groupIntergalaticNumbers + " " + groupMetal + " is " + answer;

            Console.WriteLine(outputMessage);           
        }

        /// <summary>
        /// Calculates the answer for a question of type "how much"
        /// </summary>
        /// <param name="sentence"></param>
        public void AnswerHowMuchQuestion(string sentence)
        {
            string[] substrings = Util.SplitByIs(sentence);

            Regex regex = new Regex(@"(?<groupInterNum>(\w+ )+)\?$");
            MatchCollection matches = regex.Matches(substrings[1]);
            IEnumerator matchesEnum = matches.GetEnumerator();
            string groupIntergalaticNumbers = "";                  

            if (matchesEnum.MoveNext())
            {
                Match match = (Match)matchesEnum.Current;
                groupIntergalaticNumbers = match.Groups["groupInterNum"].Value.Trim();                
            }

            string romanNumber = "";            
            string[] intergalaticNumbers = groupIntergalaticNumbers.Split(' ');

            for (int i = 0; i < intergalaticNumbers.Length; i++)
            {
                romanNumber += intergalaticDictionary[intergalaticNumbers[i]];
            }

            int answer = romanNumeral.ConvertToNumber(romanNumber);

            string outputMessage = groupIntergalaticNumbers + " is " + answer;
            Console.WriteLine(outputMessage);
        }
    }
}
