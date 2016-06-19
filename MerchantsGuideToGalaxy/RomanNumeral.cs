using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MerchantsGuideToGalaxy.Exceptions;

namespace MerchantsGuideToGalaxy
{
    public class RomanNumeral
    {
        private enum RomanSymbol
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
        }  
        
             

        public int ConvertToNumber(string romanNumeral)
        {
            //check if it is only permitted char and no number
            if (FollowsRepetitionRule(romanNumeral))
            {
                List<int> singleNumbers = GetSingleNumbers(romanNumeral.ToCharArray());
                int convertedNumber = 0;

                if (singleNumbers != null)
                {
                    if (FollowsSubtractionRule(singleNumbers))
                    {
                        singleNumbers = UpdateList(singleNumbers);

                        foreach (var singleNumber in singleNumbers)
                        {
                            convertedNumber += singleNumber;
                        }
                    }
                }

                return convertedNumber;
            }
            else
            {
                throw new InvalidRomanNumeralException("Repetition rule is not respected!");
            }            
        }

       
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="romanNumeral"></param>
        /// <returns></returns>
        public bool FollowsRepetitionRule(string romanNumeral)
        {
            string[] incorrectValues = { "(IIII+)", "(XXXX+)", "(CCCC+)", "(MMMM+)", "(DD+)", "(LL+)", "(VV+)" };

            foreach (var invalidPattern in incorrectValues)
            {
                if (Regex.IsMatch(romanNumeral, invalidPattern))
                {
                    return false;
                }  
            }

            return true;
        }

        public bool FollowsSubtractionRule(List<int> numbers)
        {
            int currentNumber;
            int nextNumber;

            for (int i = 0; i < numbers.Count - 1; i++) //the last number is ok
            {
                currentNumber = numbers[i];
                nextNumber = numbers[i + 1];

                if ((currentNumber < nextNumber) && !CanBeSubtracted(currentNumber, nextNumber))
                {
                    throw new InvalidRomanNumeralException("Subtraction rule is not respected!");
                }
            }

            return true;
        }

        private bool CanBeSubtracted(int subtrahend, int minuend)
        {
            if (subtrahend == (int)RomanSymbol.V
                || subtrahend == (int)RomanSymbol.L
                || subtrahend == (int)RomanSymbol.D)
            {
                throw new InvalidRomanNumeralException("V, L, and D can never be subtracted!");
            }

            return minuend <= 10 * subtrahend;
        }

        private List<int> UpdateList(List<int> singleNumbers)
        {
            int currentElement;
            int nextElement;            

            for (int i = 0; i < singleNumbers.Count - 1; i++) //the last number is never subtract
            {
                currentElement = singleNumbers[i];
                nextElement = singleNumbers[i + 1];

                if (currentElement < nextElement)
                {
                    singleNumbers[i] = -currentElement;                    
                }               
            }

            return singleNumbers;
        }
        
        /// <summary>
        /// Gets the equivalent number of a given roman symbol
        /// </summary>
        /// <param name="romanSymbol">A roman symbol</param>
        /// <returns>Number equivalent to roman symbol</returns>
        public int GetNumberFromRomanChar(char romanSymbol)
        {
            int number = 0;

            switch (romanSymbol)
            {
                case 'I':
                    number = (int)RomanSymbol.I;
                    break;
                case 'V':
                    number = (int)RomanSymbol.V;
                    break;
                case 'X':
                    number = (int)RomanSymbol.X;
                    break;
                case 'L':
                    number = (int)RomanSymbol.L;
                    break;
                case 'C':
                    number = (int)RomanSymbol.C;
                    break;
                case 'D':
                    number = (int)RomanSymbol.D;
                    break;
                case 'M':
                    number = (int)RomanSymbol.M;
                    break;
                default:
                    break;
            }

            return number;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="romanSymbols"></param>
        /// <returns></returns>
        private List<int> GetSingleNumbers(char[] romanSymbols)
        {
            List<int> singleNumbers = new List<int>();

            foreach (var symbol in romanSymbols)
            {
                singleNumbers.Add(GetNumberFromRomanChar(symbol));
            }

            return singleNumbers;
        }        
    }
}
