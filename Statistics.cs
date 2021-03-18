using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodewarsKatas
{
    public static class Statistics
    {
        public static string stat(string strg)
        {
            if (strg == "") return "";
            string stats = "";

            // store individual results
            List<DateTime> formattedResults = new List<DateTime>();
            
            // split input where comma delimiter occurs
            string[] results = strg.Split(',');

            // parse input string
            foreach (string r in results)
            {
                string[] numbers = r.Split('|');

                if (numbers.Length != 3) return "";

                string[] format = { "hh|mm|ss", "h|mm|s", "h|mm|ss", "h|m|ss" }; 

                DateTime dateTime;
                DateTime.TryParseExact(r, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

                formattedResults.Add(dateTime);
            }

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            // 1. Find range
            foreach (DateTime fr in formattedResults)
            {
                if (fr < minDate) minDate = fr;
                if (fr > maxDate) maxDate = fr;
            }

            // find the difference between min and max results and convert it back to date format
            TimeSpan range = maxDate - minDate;
            stats += range.ToString();

            // find mean

            // find median

            return stats;
        }
    }
}
