using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace GSMarketing
{
    public interface DNAENcoded
    {
        void ShowEncodedDNA();
    }
    public class EncodingIndex : DNAENcoded
    {
        private string encodedString;

        public EncodingIndex()
        {
            encodedString = "";
        }
        
        public EncodingIndex(string eS)
        {
            encodedString = eS;
        }

        //Interface method to check that the substring set contains C, A, T, or G.
        public void ShowEncodedDNA()
        {
            bool dNAExist = false;
            for (int i = 0; i <= encodedString.Length; i++)
            {
                if(encodedString.Length >= (i + 4)) {
                    if (encodedString[i].ToString().Contains("A") || encodedString[i].ToString().Contains("T") || encodedString[i].ToString().Contains("G") || encodedString[i].ToString().Contains("C"))
                    {
                        if (encodedString[i + 1].ToString().Contains("A") || encodedString[i + 1].ToString().Contains("T") || encodedString[i + 1].ToString().Contains("G") || encodedString[i + 1].ToString().Contains("C"))
                        {
                            if (encodedString[i + 2].ToString().Contains("A") || encodedString[i + 2].ToString().Contains("T") || encodedString[i + 2].ToString().Contains("G") || encodedString[i + 2].ToString().Contains("C"))
                            {
                                if (encodedString[i + 3].ToString().Contains("A") || encodedString[i + 3].ToString().Contains("T") || encodedString[i + 3].ToString().Contains("G") || encodedString[i + 3].ToString().Contains("C"))
                                {
                                    Console.WriteLine("Index: " + i.ToString() + "\n");
                                    dNAExist = true;
                                }
                            }
                        }
                    }
                }
            }
            if (dNAExist == false)
            {
                Console.WriteLine("Index: -1\n");
            }
        }
    }
    class Program
    {
        public static string dNAA = "00";
        public static string dNAT = "01";
        public static string dNAG = "10";
        public static string dNAC = "11";

        static void Main(string[] args)
        {
            //Options Menu
            while (true) {
                Console.WriteLine("Enter 1, 2, 3, 4, or 0 for options below: " +
                    "\n 1. To convert as DNA. " +
                    "\n 2. To convert as RNA. " +
                    "\n 3. To identify standard ASCII text substrings encoded as DNA." +
                    "\n 4. Extract Primary strand from Complimentary strand. " +
                    "\n 5. Find the longest common subsequence of two strands of DNA. " +
                    "\n 0. Exit Console.\n ");
                string dNAOrRNA = Console.ReadLine();

                //Objective 1
                if (dNAOrRNA == "1")
                {
                    Console.WriteLine("\nInput: ");
                    string result = Console.ReadLine();
                    //Console.WriteLine("Output: " + ConvertStringToBinary(result));
                    string binaryResults = ConvertStringToBinary(result);
                    Console.WriteLine("Output DNA: " + StringToDNAOrRNA(binaryResults, dNAOrRNA) + "\n");
                }

                //Objective 2
                else if (dNAOrRNA == "2")
                {
                    Console.WriteLine("\nInput: ");
                    string result = Console.ReadLine();
                    Console.WriteLine("Output: " + ConvertStringToBinary(result));
                    string binaryResults = ConvertStringToBinary(result);
                    Console.WriteLine("Output DNA: " + StringToDNAOrRNA(binaryResults, dNAOrRNA) + "\n");
                }

                //Objective 3
                else if (dNAOrRNA == "3")
                {
                    Console.WriteLine("\nInput a string of characters: ");
                    string result = Console.ReadLine();
                    EncodingIndex eI = new EncodingIndex(result);
                    eI.ShowEncodedDNA();
                }

                //Objective 4
                else if (dNAOrRNA == "4")
                {
                    Console.WriteLine("\nInput a primary strand of DNA: ");
                    string result = Console.ReadLine();

                    //Make sure that the string only contains A, T, G, or C.
                    bool onlyDNA = result.All(s => "ATGC".Contains(s));

                    //Check to make sure that the DNA strands are in sets of 4 and contain A, T, G, or C.
                    if (onlyDNA == false || result.Length % 4 != 0)
                    {
                        Console.WriteLine("Your input is invalid.\n");
                    }
                    
                    //Conversion of input into primary strand, then converted to binary.
                    string binaryResult = ComplimentaryStringToBinary(ConversionToPrimaryStrand(result).ToString());

                    //Conversion of binary data to ASCII Char.
                    var binaryData = BinaryToASCII(binaryResult);
                    var stringResult = Encoding.ASCII.GetString(binaryData);

                    Console.WriteLine("The Primary Strand in ASCII is " + stringResult + "\n");

                }

                //Objective 5
                else if (dNAOrRNA == "5")
                {
                    Console.WriteLine("\nInput the first string of DNA: \n");
                    string firstResult = Console.ReadLine();

                    //Make sure that the string only contains A, T, G, or C.
                    bool firstDNA = firstResult.All(s => "ATGC".Contains(s));

                    //Confirm that first input is valid.
                    if (firstDNA == false)
                    {
                        Console.WriteLine("Your input is invalid.\n");
                    }
                    else
                    {
                        Console.WriteLine("\nInput the second string of DNA: \n");
                        string secondResult = Console.ReadLine();

                        //Make sure that the string only contains A, T, G, or C.
                        bool secondDNA = secondResult.All(s => "ATGC".Contains(s));

                        //Confirm that second input is valid.
                        if (secondDNA == false)
                        {
                            Console.WriteLine("Your input is invalid.\n");
                        }

                        else
                        {
                        //Call subsequence method from the subsequence interface in other file.
                        Subsequence sODNA = new Subsequence(firstResult, secondResult);
                            sODNA.SubsequenceComparison();
                        }
                    }
                    
                }

                else if (dNAOrRNA == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("This is an invalid entry. Please try again.");
                }

            }
        }

        //Conversion of string to binary.
        public static string ConvertStringToBinary(string result)
        {
            StringBuilder sB = new StringBuilder();

            foreach (char s in result.ToCharArray())
            {
                sB.Append(Convert.ToString(s, 2).PadLeft(8, '0'));
            }
            return sB.ToString();
        }

        //Conversion from string to either DNA or RNA format.
        public static string StringToDNAOrRNA(string dNAResult, string rNAOrDNA)
        {
            string finalResult = "";
            for (int i = 0; i < dNAResult.Length; i += 2)
            {
                if(dNAResult.Substring(i, 2) == dNAA)
                {
                    finalResult += "A";
                }
                else if (dNAResult.Substring(i, 2) == dNAT && rNAOrDNA == "1")
                {
                    finalResult += "T";
                }
                else if (dNAResult.Substring(i, 2) == dNAT && rNAOrDNA == "2")
                {
                    finalResult += "U";
                }
                else if (dNAResult.Substring(i, 2) == dNAG)
                {
                    finalResult += "G";
                }
                else if (dNAResult.Substring(i, 2) == dNAC)
                {
                    finalResult += "C";
                }
            }
            return finalResult;
        }

        //Conversion from Complimentary Strand to Primary Strand
        public static string ConversionToPrimaryStrand(string result)
        {
            string finalResult = "";
            for(int i = 0; i < result.Length; i++)
            {
                if(result[i].ToString() == "A")
                {
                    finalResult += "T";
                }
                else if(result[i].ToString() == "T")
                {
                    finalResult += "A";
                }
                else if (result[i].ToString() == "G")
                {
                    finalResult += "C";
                }
                else if (result[i].ToString() == "C")
                {
                    finalResult += "G";
                }
            }
            return finalResult;
        }

        //Conversion of Complimentary Strand to Binary Language
        public static string ComplimentaryStringToBinary(string result)
        {
            string finalResult = "";
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].ToString() == "A")
                {
                    finalResult += "00";
                }
                else if (result[i].ToString() == "T")
                {
                    finalResult += "01";
                }
                else if (result[i].ToString() == "G")
                {
                    finalResult += "10";
                }
                else if (result[i].ToString() == "C")
                {
                    finalResult += "11";
                }
            }
            return finalResult;
        }

        //Conversion of Binary to ASCII Characters.
        public static Byte[] BinaryToASCII(String result)
        {
            var list = new List<Byte>();

            for (int i = 0; i < result.Length; i += 8)
            {
                if (result.Length >= (i + 8))
                {
                    String finalResult = result.Substring(i, 8);

                    list.Add(Convert.ToByte(finalResult, 2));
                }
            }

            return list.ToArray();
        }
    }
}
