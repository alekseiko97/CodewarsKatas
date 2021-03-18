using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodewarsKatas
{
    public class HackerRank
    {
        // Complete the hourglassSum function below.
        static int hourglassSum(int[][] arr)
        {
            int sum = 0;
            List<int> listOfSums = new List<int>();

            /* Row */
            for (int i = 0; i < arr.Length - 2; i++)
            {
            /* Column */
                for (int j = 0; j < arr[i].Length - 2; j++)
                {
                    // get the sum of 3 consecutive elements in a row
                    sum += arr[i][j];
                    sum += arr[i][j + 1];
                    sum += arr[i][j + 2];

                    // next, add a middle element from the underneath row
                    sum += arr[i + 1][j + 1];

                    // finally, add the sum of 3 consecutive elements of i+2 row
                    int thirdRowIndex = i + 2;

                    sum += arr[thirdRowIndex][j];
                    sum += arr[thirdRowIndex][j + 1];
                    sum += arr[thirdRowIndex][j + 2];

                    // before going to the next row, add hourglass sum to the list
                    listOfSums.Add(sum);
                    sum = 0;
                }

            }

            return listOfSums.Max();
        }

    }
}
