using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace GSMarketing
{
    public interface SubsequenceOfDNA
    {
        void SubsequenceComparison();
    }
    public class Subsequence : SubsequenceOfDNA
    {
        private string dNAStrandOne;
        private string dNAStrandTwo;

        public Subsequence()
        {
            dNAStrandOne = "";
            dNAStrandTwo = "";
        }

        public Subsequence(string dNAOne, string dNATwo)
        {
            dNAStrandOne = dNAOne;
            dNAStrandTwo = dNATwo;
        }

        public void SubsequenceComparison()
        {
            string finalResult = "";
            int indexOne = 0;
            int indexTwo = 0;

            //For loop used to run through both sets of DNA data, and make comparisons.
            for(int i = indexOne; i < dNAStrandOne.Length; i++)
            {
                int subsequentResult = dNAStrandTwo.IndexOf(dNAStrandOne[i], indexTwo);
                if(subsequentResult != -1)
                {
                    finalResult += dNAStrandOne[i];
                    indexOne += 1;
                    indexTwo = subsequentResult + 1;
                    subsequentResult += 1;
                }
            }
            Console.WriteLine("The longest common subsequence is: " + finalResult + "\n");
        }

        class SubsequenceDNA
        {
        }
    }
}
