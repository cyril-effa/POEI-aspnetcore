
using System.Reflection.Metadata.Ecma335;

namespace TDD_BowlingGame
{
    public class BowlingGame
    {
        List<int> rolls = new List<int>();

        public int GetScore()
        {
            int score = 0;
            int index = 0;

            for (int turn = 0; turn < 10; turn++)
            {
                if (IsStrike(index)) // STRIKE
                {
                    score += CalculateStrike(index);
                    index++;
                }
                else if (IsSpare(index)) // SPARE
                {
                    score += CalculateSpare(index);
                    index += 2;
                }
                else // NORMAL
                {
                    score += CalculateNormal(index);
                    index += 2;
                }
            }
            return score;
        }

        private bool IsStrike(int index)
        {
            return rolls[index] == 10;
        }

        private bool IsSpare(int index)
        {
            return rolls[index] + rolls[index + 1] == 10;
        }

        private int CalculateStrike(int index)
        {
            return rolls[index] + rolls[index + 1] + rolls[index + 2];
        }

        private int CalculateSpare(int index)
        {
            return CalculateNormal(index) + rolls[index + 2]; // Same as Strike but separate for clarity
        }

        private int CalculateNormal(int index)
        {
            return rolls[index] + rolls[index + 1];
        }

        public void MakeRolls(int nbRolls, int valueRolls)
        {
            for (int i = 0; i < nbRolls; i++)
            {
                Roll(valueRolls);
            }
        }

        public void Roll(int valueRolls)
        {
            rolls.Add(valueRolls);
        }
    }
}
