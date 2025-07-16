
using System.ComponentModel;

namespace TDD_RomanNum
{
    public class RomanNum
    {
        public string ConvertToRoman(int num)
        {
            string result = string.Empty;

            while (num > 0)
            {
                if (num >= 1000)
                {
                    result += "M";
                    num -= 1000;
                }
                else if (num >= 900)
                {
                    result += "CM";
                    num -= 900;
                }
                else if (num >= 500)
                {
                    result += "D";
                    num -= 500;
                }
                else if (num >= 400)
                {
                    result += "CD";
                    num -= 400;
                }
                else if (num >= 100)
                {
                    result += "C";
                    num -= 100;
                }
                else if (num >= 90)
                {
                    result += "XC";
                    num -= 90;
                }
                else if (num >= 50)
                {
                    result += "L";
                    num -= 50;
                }
                else if (num >= 40)
                {
                    result += "XL";
                    num -= 40;
                }
                else if (num >= 10)
                {
                    result += "X";
                    num -= 10;
                }
                else if (num >= 9)
                {
                    result += "IX";
                    num -= 9;
                }
                else if (num >= 5)
                {
                    result += "V";
                    num -= 5;
                }
                else if (num == 4)
                {
                    result += "IV";
                    num -= 4;
                }
                else if (num < 4)
                {
                    result += "I";
                    num -= 1;
                }
            }

            return result;
        }

        public int ConvertToNum(string roman)
        {
            int result = 0;
            int prevValue = 0;
            foreach (char c in roman)
            {
                int value = c switch
                {
                    'M' => 1000,
                    'D' => 500,
                    'C' => 100,
                    'L' => 50,
                    'X' => 10,
                    'V' => 5,
                    'I' => 1,
                    _ => throw new InvalidEnumArgumentException("Invalid Roman numeral character")
                };
                if (value > prevValue)
                {
                    result += value - 2 * prevValue; // Adjust for previous value
                }
                else
                {
                    result += value;
                }
                prevValue = value;
            }
            return result;
        }
    }
}
