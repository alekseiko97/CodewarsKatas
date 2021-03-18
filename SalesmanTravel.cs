using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodewarsKatas
{
    public static class SalesmanTravel
    {
        // "123 Main Street St. Louisville OH 43071
        // 432 Main Long Road St. Louisville OH 43071
        // 786 High Street Pollocksville NY 56432"
        public static string Travel(string r, string zipcode)
        {
            string grouped = zipcode + ":";

            // check if zipcode is contained in string 'r'
            if (!r.Contains(zipcode)) return zipcode + ":/";

            string[] addresses = r.Split(',');
            
            List<string> streets = new List<string>();
            List<int> houseNumbers = new List<int>();

            grouped += string.Format("{0}:{1}/{2}", houseNumbers, streets, zipcode);

            foreach (string address in addresses)
            {
                // extract streets and house numbers and put them into different arrays
                houseNumbers.Add(int.Parse(address.Substring(0, 3)));
                streets.Add(address.Substring(5).Replace(zipcode, ""));
            }

            // generate a string
            houseNumbers.ForEach(h => grouped += h + ",");
            grouped.Remove(grouped.Length - 1);

            grouped += "/";

            streets.ForEach(s => grouped += s + ",");
            grouped.Remove(grouped.Length - 1);


            return grouped;
        }
    }
}
