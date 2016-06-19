using System.Text.RegularExpressions;

namespace MerchantsGuideToGalaxy
{
    public class Line
    {
        public string IntergalaticDigitPattern { get; } = @"^(\w+ )+is [ivxlcdm]$";
        //public string MetalPattern { get; } = @"^(?<grouptIntergalatic>\w+ )+(?<groupMetal>\w+) is \d+ credits$";
        public string MetalPattern { get; } = @"^(?<grouptIntergalatic>\w+ )+(?<groupMetal>\w+) is [0-9]*(?:\.[0-9]*)? credits$";
        //@"^[0-9]*(?:\.[0-9]*)?$"
        public string HowManyQuestionPattern { get; } = @"^how many credits is (\w+ )+(?<groupMetal>\w+ )\?$";
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
