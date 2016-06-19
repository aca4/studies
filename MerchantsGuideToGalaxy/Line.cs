using System.Text.RegularExpressions;

namespace MerchantsGuideToGalaxy
{
    public class Line
    {
        private string IntergalaticDigitPattern = @"^(\w+ )+is [IVXLCDM]$";        
        public string MetalPattern { get; } = @"^(?<grouptIntergalatic>\w+ )+(?<groupMetal>[A-Z]\w+) is \d+ Credits$";
        public string HowManyQuestionPattern { get; } = @"^how many Credits is (\w+ )+(?<groupMetal>[A-Z][a-z]\w+) \?$";
        public string HowMuchQuestionPattern { get; } = @"^how much is (\w+ )+\?$";
        
        public LineType Type { get; set; }
        public string Text { get; set; }

        public Line (string text)
        {
            Text = text;
            SetLineType();
        }

        private void SetLineType()
        {
            if (Regex.IsMatch(Text, IntergalaticDigitPattern))
            {
                Type = LineType.IntergalaticDigit;
            }
            else if (Regex.IsMatch(Text, MetalPattern))
            {
                Type = LineType.Metal;
            }
            else if (Regex.IsMatch(Text, HowManyQuestionPattern))
            {
                Type = LineType.HowManyQuestion;
            }
            else if (Regex.IsMatch(Text, HowMuchQuestionPattern))
            {
                Type = LineType.HowMuchQuestion;
            }
            else
            {
                Type = LineType.Invalid;
            }
        }
    }
}
