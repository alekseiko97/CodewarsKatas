using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodewarsKatas
{
    class Program
    {
        static int[,] maze = new int[,] { { 1, 1, 1, 1, 1, 1, 1 },
                                       { 1, 0, 0, 0, 0, 0, 3 },
                                       { 1, 0, 1, 0, 1, 0, 1 },
                                       { 0, 0, 1, 0, 0, 0, 1 },
                                       { 1, 0, 1, 0, 1, 0, 1 },
                                       { 1, 0, 0, 0, 0, 0, 1 },
                                       { 1, 2, 1, 0, 1, 0, 1 } };

        static void Main(string[] args)
        {
            int[] c = new int[] { 0, 0, 1, 0, 0, 1, 0 };
            MessageBox.Show(Codewars.jumpingOnClouds(c).ToString());
        }
    }

    public static class Codewars
    {
        /*
         * Implement the function unique_in_order which takes as argument a sequence and returns a list of items without any elements
         * with the same value next to each other and preserving the original order of elements.
         * 
         * Examples:
         * uniqueInOrder("AAAABBBCCDAABBB") == {'A', 'B', 'C', 'D', 'A', 'B'}
           uniqueInOrder("ABBCcAD")         == {'A', 'B', 'C', 'c', 'A', 'D'}
           uniqueInOrder([1,2,2,3,3])       == {1,2,3}
         */
        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            List<T> result = new List<T>();

            // check for emptiness
            if (iterable.Count() == 0) return new List<T>();

            // convert to specific type
            IEnumerable<char> iterableAsString = iterable.Cast<char>();

            // traverse through IEnumerable
            for (int i = 0; i < iterable.Count(); i++)
            {
                // compare current element to the next one
                Type t = iterable.ElementAt(i).GetType();

                if (t == typeof(char))
                {
                    // var singleLetter = iterable.ElementAt(i);
                    // if (result.Count() == 0 || result.Last() != singleLetter)
                    //    result.Add(singleLetter);

                }

                if (t == typeof(int))
                {

                }
            }

            // cast back to generic type 
            return result.Cast<T>();
        }

        public static int FindEvenIndex(int[] arr)
        {
            // {20,10,-80,10,10,15,35}
            // Assert.AreEqual(3, Codewars.FindEvenIndex(new int[] { 1, 2, 3, 4, 3, 2, 1 }));
            for (int i = 0; i < arr.Length; i++)
            {
                // compute left-hand side
                int leftSum = 0;
                for (int j = 0; j < i; j++)
                {
                    // if splitting element is at index 0, the left-hand side equals to 0
                    leftSum += arr[j];
                }

                // compute right-hand side
                int rightSum = 0;
                for (int k = i + 1; k < arr.Length; k++)
                {
                    rightSum += arr[k];
                }

                // compare the sum on the left side to the right side
                if (leftSum == rightSum) return i;
            }


            return -1;
        }


        public static string RevRot(string strng, int sz)
        {
            // check null conditions
            if (sz <= 0 || string.IsNullOrEmpty(strng)) return "";

            // chunk is greater than the length of given string
            if (sz > strng.Length) return "";

            // cut the string into chunks
            IEnumerable<string> chunks = Enumerable.Range(0, strng.Length / sz).Select(i => strng.Substring(i * sz, sz));

            // ignore the last chunk if its size is less than sz


            List<string> modifiedChunks = new List<string>();

            foreach (string chunk in chunks)
            {
                // parse chunk to digit array
                var digits = chunk.Select(ch => ch - '0').ToList();

                // sum up the cubes
                int sum = Convert.ToInt32(digits.Sum(x => Math.Pow(x, 3)));

                /*
                 * if a chunk represents an integer such as the sum of the cubes of
                   its digits is divisible by 2, reverse that chunk
                 */

                string modifiedChunk = "";
                if (sum % 2 == 0)
                {
                    digits.Reverse();

                    // reconstrunct the string
                    digits.ForEach(x => modifiedChunk += x);
                }

                /*
                 * otherwise rotate it to the left by one position
                 */
                else
                {
                    // reconstrunct the string
                    // ex.: '123456' becomes '234561'
                    for (int i = 1; i < digits.Count; i++)
                    {
                        modifiedChunk += digits.ElementAt(i);
                    }

                    modifiedChunk += digits.ElementAt(0); // move the first digits to the end

                }

                modifiedChunks.Add(modifiedChunk);
            }


            // put together these modified chunks and return the result as string
            return string.Join("", modifiedChunks);
        }

        /*
          0 = Safe place to walk
          1 = Wall
          2 = Start Point
          3 = Finish Point

        maze = [[1,1,1,1,1,1,1],
                [1,0,0,0,0,0,3],
                [1,0,1,0,1,0,1],
                [0,0,1,0,0,0,1],
                [1,0,1,0,1,0,1],
                [1,0,0,0,0,0,1],
                [1,2,1,0,1,0,1]]

         direction = ["N","N","N","N","N","E","E","E","E","E"] == "Finish"

         N = North, E = East, W = West and S = South
        */
        public static string MazeRunner(int[,] maze, string[] directions)
        {
            // initial value
            (int, int) currentPosition = (-1, -1);

            // parse maze
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == 2) currentPosition = (i, j); // find starting index
                }
            }

            // parse directions
            foreach (string d in directions)
            {
                int x, y;
                // update the index position according to the parsed direction
                switch (d)
                {
                    case "N": // north
                        x = currentPosition.Item1 - 1;
                        y = currentPosition.Item2;
                        break;
                    case "E":
                        x = currentPosition.Item1;
                        y = currentPosition.Item2 + 1;
                        break;
                    case "W":
                        x = currentPosition.Item1;
                        y = currentPosition.Item2 - 1;
                        break;
                    case "S":
                        x = currentPosition.Item1 + 1;
                        y = currentPosition.Item2;
                        break;
                    default:
                        return "Invalid direction!";
                }

                currentPosition = (x, y);

                try
                {
                    // find the value of the new index in the maze
                    switch (maze[currentPosition.Item1, currentPosition.Item2])
                    {
                        case 0:
                            break; // safe spot, go further
                        case 1:
                            return "Dead"; // hit the wall
                        case 3:
                            return "Finish";
                        default:
                            return "Something else"; // we can't have another start (2) or any other number, or go outside the border
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    /* If you go outside the borders of the maze, you're dead */
                    return "Dead";
                }
            }

            /* If you find yourself still in the maze after using all the moves, you should return Lost.*/
            if (maze[currentPosition.Item1, currentPosition.Item2] != 3) return "Lost";


            return "Lost";
        }

        public static int[] MoveZeroes(int[] arr)
        {
            int count = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    arr[count++] = arr[i];
                }
            }

            while (count < arr.Length)
            {
                arr[count++] = 0;
            }

            return arr;
        }

        /*
         *   Three 1's => 1000 points
             Three 6's =>  600 points
             Three 5's =>  500 points
             Three 4's =>  400 points
             Three 3's =>  300 points
             Three 2's =>  200 points
             One   1   =>  100 points
             One   5   =>   50 point
         */

        public static int Score(int[] dice)
        {
            int totalScore = 0;

            Dictionary<int, int> tripletScore = new Dictionary<int, int>
            {
                { 1, 1000 },
                { 2, 200 },
                { 3, 300 },
                { 4, 400 },
                { 5, 500 },
                { 6, 600 }
            };

            Dictionary<int, int> occurenceCount = new Dictionary<int, int>();

            // Traverse through array elements and find frequencies
            for (int i = 0; i < dice.Length; i++)
            {
                if (occurenceCount.ContainsKey(dice[i]))
                {
                    var val = occurenceCount[dice[i]];
                    occurenceCount.Remove(dice[i]);
                    occurenceCount.Add(dice[i], val + 1);
                }
                else
                    occurenceCount.Add(dice[i], 1);
            }

            foreach (KeyValuePair<int, int> entry in occurenceCount)
            {
                int rest = entry.Value;
                // add triple to the final score
                if (entry.Value >= 3)
                {
                    totalScore += tripletScore[entry.Key];
                    rest = entry.Value - 3;
                }
                // add the singles to the final score
                if (entry.Key == 1 && entry.Value != 3) totalScore += (100 * rest);
                if (entry.Key == 5 && entry.Value != 3) totalScore += (50 * rest);
            }

            return totalScore;
        }

        public static List<long[]> removNb(long n)
        {
            List<long[]> removedNumbers = new List<long[]>();

            long sequence_sum = n * (n + 1) / 2;

            for (int x = 0; x <= n; x++)
            {
                long y = (sequence_sum - x) / (x + 1);
                if (y <= n && x * y == sequence_sum - x - y)
                {
                    removedNumbers.Add(new long[] { x, y });
                }
            }

            /* n = 5 
               sum = 15
               
                // 1
                x = 0, y = 15
                1, 7 = 7 -> 15 - 7 = 8
                2, 4 = 8 = 2
                3, 3 = 9
                4, 2 = 8
                5, 4 = 20
             */


            // first of all, we calculate the sum of all numbers from 1 to n
            /*            for (int x = 1; x <= n; x++)         
                            sum += x;

                        // then, we try to find the pair of numbers whose product will be equal to the sum
                        // of all numbers, excluding this pair
                        for (int i = 1; i <= n; i++)
                        {
                            for (int j = 1; j <= n; j++)
                            {
                                if ((i * j) == (sum - i - j))
                                    removedNumbers.Add(new long[] { i, j });
                            }
                        }*/

            return removedNumbers;
        }

        // Complete the sockMerchant function below.
        public static int sockMerchant(int n, int[] ar)
        {
            int pairsCounter = 0;

            Dictionary<int, int> colorCount = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                if (colorCount.ContainsKey(ar[i]))
                {
                    var value = colorCount[ar[i]];
                    colorCount.Remove(ar[i]);
                    colorCount.Add(ar[i], value++);
                }
                else
                {
                    colorCount.Add(ar[i], 1);
                }
            }

            foreach (KeyValuePair<int, int> socksColor in colorCount)
            {
                Console.WriteLine($"Key: {socksColor.Key} value: {socksColor.Value}");
                if (socksColor.Value >= 2)
                    pairsCounter += socksColor.Value / 2;
            }
            /* return: number of pairws */
            return pairsCounter;
        }

        public static int countingValleys(int steps, string path)
        {
            int valleyCounter = 0;
            int previousLevel = 0;
            int seaLevel = 0;

            foreach (char step in path)
            {
                previousLevel = seaLevel;
                switch (step)
                {
                    case 'D':
                        seaLevel--;
                        break;
                    case 'U':
                        seaLevel++;
                        break;
                    default:
                        return -1;
                }

                if (seaLevel == 0 && previousLevel < 0) valleyCounter++;
            }


            return valleyCounter;
        }


        // Complete the jumpingOnClouds function below.
        public static int jumpingOnClouds(int[] c)
        {
            int nrOfJumps = 0;
            // 0 [0] 1 0 0 1 0 => i = 1
            // 0 0 1 [0] 0 1 0 => i = 3
            // 0 0 1 0 [0] 1 0 => i = 5
            // 0 0 1 0 0 1 [0] => i = 6
            
            for (int i = 0; i < c.Length; i++)
            {
                // we check if it possible to safely jump over 2 
                int overTwoIndex = i + 2;
                if (overTwoIndex < c.Length && c[overTwoIndex] == 0)
                {
                    nrOfJumps++;
                    i+= 1; // skip the next element
                } else
                {
                    nrOfJumps++;
                }
            }

            return nrOfJumps;
        }

        // Evaluator

        // "2 / 2 + 3 * 4 - 6" 

        public static double Evaluate(string expression)
        {
            Stack<string> stack = new Stack<string>();

            string value = "";
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (!char.IsDigit(c) && c != '.' && value != "")
                {
                    stack.Push(value);
                    value = "";
                }
            }

            return 0.0;
        }

        public static int MaxSequence(int[] arr)
        {
            // maxSequence [-2, 1, -3, 4, -1, 2, 1, -5, 4]
            // Easy case is when the list is made up of only positive numbers and the maximum sum is the sum of the whole array. 
            // If the list is made up of only negative numbers, return 0 instead.


            int max_so_far = 0, max_ending_here = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                max_ending_here += arr[i];

                if (max_so_far < max_ending_here)
                {
                    max_so_far = max_ending_here;
                }

                if (max_ending_here < 0) max_ending_here = 0;
            }

            // Empty list is considered to have zero greatest sum. Note that the empty list or array is also a valid sublist/subarray.

            return max_so_far;
        }


        public int[] TwoSum(int[] nums, int target)
        {
            int[] indices = new int[2];


            for (int i = 0; i < nums.Length; i++)
            {
                
            }
        }

    }
}
