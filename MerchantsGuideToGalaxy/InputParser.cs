using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MerchantsGuideToGalaxy
{
    public class InputParser
    {
        public string UnitPattern { get; } = @"^(\w+ )+is [IVXLCCDM]$";        
        public string MetalPattern { get; } = @"^(\w+ )+(Silver|Gold|Iron) is \d+ Credits$";
        //considerar que pode nao ter much/many?
        public string QuestionPattern { get; } = @"^how (much|many Credits) is (\w+ )+(Silver |Gold |Iron )?\?$";
        private string invalidQuery = "I have no idea what you are talking about";

        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        private List<string> sentences = new List<string>();

        //com ou sem a entrada como parametro?
        public List<string> Translate()
        {
            List<string> answers = new List<string>();

            foreach (var line in sentences)
            {
                if (Regex.IsMatch(line, UnitPattern))
                {
                    DefineRomanNumeral(line);
                }
                else if (Regex.IsMatch(line, MetalPattern))
                {
                    //do credits calculation
                }
                else if (Regex.IsMatch(line, QuestionPattern))
                {
                    //answer question
                }
                else
                {
                    answers.Add(invalidQuery);
                }
            }

            return answers;
        }

        private void DefineRomanNumeral(string sentence)
        {
            Regex regex = new Regex(@"\bis\b");
            string[] tokens = regex.Split(sentence);

            //TODO: verificar se são sempre dois mesmo
            dictionary.Add(tokens[0], tokens[1]);
        }

        //TODO: change name
        private void DefineCredits(string sentence)
        {

        }
    }
}
