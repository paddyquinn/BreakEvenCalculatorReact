using System;
using System.Text.RegularExpressions;

namespace BreakEvenCalculator
{
    // Parses strings into American Odds and converts to other formats.
    public class AmericanOdds
    {
        private const string rx_string = @"^([\+-])(\d\d\d+)$";
        private readonly int odds;
        private readonly bool favorite;

        public static bool IsValid(string am_odds)
        {
            return Regex.IsMatch(am_odds, rx_string);
        }

        public AmericanOdds(string am_odds)
        {
            var valid = false;
            var rx = new Regex(rx_string, RegexOptions.Compiled);

            MatchCollection matches = rx.Matches(am_odds);
            if (matches.Count < 1)
            {
                valid = false;
            }
            else 
            {
                GroupCollection groups = matches[0].Groups;
                if (groups[1].Value == "+")
                {
                    favorite = false;
                    valid = true;
                }
                else if (groups[1].Value == "-")
                {
                    favorite = true;
                    valid = true;
                }
                else
                {
                    valid = false;
                }
                if (int.TryParse(groups[2].Value, out odds)) { }
                else
                {
                    valid = false;
                }
            }
            if (!valid)
            {
                throw new ArgumentException("Invalid odds format", nameof(am_odds));
            }
        }

        public double GetBreakEvenPercentage()
        {
            double amountToWin;
            if (favorite) {
                amountToWin = 100.0 / odds;
            } else {
                amountToWin = odds / 100.0;
            }
            
            return 1.0 / (amountToWin + 1.0);
        }
    }
}
