using System;
using System.Collections.Generic;

namespace Yahtzee
{
    class Program
    {
        const int NONE = -1;
        const int ONES = 0;
        const int TWOS = 1;
        const int THREES = 2;
        const int FOURS = 3;
        const int FIVES = 4;
        const int SIXES = 5;
        const int THREE_OF_A_KIND = 6;
        const int FOUR_OF_A_KIND = 7;
        const int FULL_HOUSE = 8;
        const int SMALL_STRAIGHT = 9;
        const int LARGE_STRAIGHT = 10;
        const int CHANCE = 11;
        const int YAHTZEE = 12;
        const int SUBTOTAL = 13;
        const int BONUS = 14;
        const int TOTAL = 15;

        static void Main(string[] args)
        {
            // declare variables for the user's scorecard and the computer's scorecard
            int[] userScorecard = new int[16];
            int[] computerScorecard = new int[16];

            // declare a variable for the number of turns the user has taken and another for the number of moves the computer has taken
            int userTurns = 0;
            int computerTurns = 0;

            // declare a boolean that knows if it is the user's turn and set it to false
            bool userTurn = false;

            // call ResetScorecard for the user
            ResetScorecard(userScorecard);
            // call ResetScorecard for the computer
            ResetScorecard(computerScorecard);

            // set the window size of the console window because displaying the scorecard requires about 50 lines
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Console.SetWindowSize(80, 50);
            }

            do
            {
                // set the userTurn variable to the opposite value
                userTurn = !userTurn;

                // call UpdateScorecard for the user
                UpdateScorecard(userScorecard);
                // call UpdateScorecard for the computer
                UpdateScorecard(computerScorecard);

                // call DisplayScorecards
                DisplayScoreCards(userScorecard, computerScorecard);

                if (userTurn)
                {
                    // display a message
                    Console.WriteLine("It's your turn.");
                    // you might also want to pause
                    Pause();
                    // call UserPlay
                    UserPlay(userScorecard, ref userTurns);
                    // increment the user's turn count
                    userTurns++;
                }
                else
                {
                    // display a message
                    Console.WriteLine("It's the computer's turn.");
                    // you might also want to pause
                    Pause();
                    // call ComputerPlay
                    ComputerPlay(computerScorecard, ref computerTurns);
                    // increment the computer's turn count
                    computerTurns++;
                }
            }
            while (userTurns <= YAHTZEE && computerTurns <= YAHTZEE);

            // call UpdateScorecard for the user
            UpdateScorecard(userScorecard);
            // call UpdateScorecard for the computer
            UpdateScorecard(computerScorecard);

            // call DisplayScorecards
            DisplayScoreCards(userScorecard, computerScorecard);

            // display a message about who won
            if (userScorecard[TOTAL] > computerScorecard[TOTAL])
            {
                Console.WriteLine("Congratulations! You won!");
            }
            else if (userScorecard[TOTAL] < computerScorecard[TOTAL])
            {
                Console.WriteLine("The computer won. Better luck next time!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }

            Console.ReadLine();
        }

        #region Scorecard Methods

        static void ResetScorecard(int[] scorecard)
        {
            for (int i = 0; i < scorecard.Length; i++)
            {
                scorecard[i] = -1; // Set each item in the scorecard to -1
            }
        }

        static void UpdateScorecard(int[] scorecard)
        {
            scorecard[SUBTOTAL] = 0;
            scorecard[BONUS] = 0;
            for (int i = ONES; i <= SIXES; i++)
                if (scorecard[i] != -1)
                    scorecard[SUBTOTAL] += scorecard[i];

            if (scorecard[SUBTOTAL] >= 63)
                scorecard[BONUS] = 35;

            scorecard[TOTAL] = scorecard[SUBTOTAL] + scorecard[BONUS];
            for (int i = THREE_OF_A_KIND; i <= YAHTZEE; i++)
                if (scorecard[i] != -1)
                    scorecard[TOTAL] += scorecard[i];
        }

        static string FormatCell(int value)
        {
            return (value < 0) ? "" : value.ToString();
        }

        static void DisplayScoreCards(int[] uScorecard, int[] cScorecard)
        {
            string[] labels = {"Ones", "Twos", "Threes", "Fours", "Fives", "Sixes",
                                "3 of a Kind", "4 of a Kind", "Full House", "Small Straight", "Large Straight",
                                "Chance", "Yahtzee", "Sub Total", "Bonus", "Total Score"};
            string lineFormat = "| {3,2} {0,-15}|{1,8}|{2,8}|";
            string border = new string('-', 39);

            Console.Clear();
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, "", "  You   ", "Computer", ""));
            Console.WriteLine(border);
            for (int i = ONES; i <= SIXES; i++)
            {
                Console.WriteLine(String.Format(lineFormat, labels[i], FormatCell(uScorecard[i]), FormatCell(cScorecard[i]), i));
            }
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, labels[SUBTOTAL], FormatCell(uScorecard[SUBTOTAL]), FormatCell(cScorecard[SUBTOTAL]), ""));
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, labels[BONUS], FormatCell(uScorecard[BONUS]), FormatCell(cScorecard[BONUS]), ""));
            Console.WriteLine(border);
            for (int i = THREE_OF_A_KIND; i <= YAHTZEE; i++)
            {
                Console.WriteLine(String.Format(lineFormat, labels[i], FormatCell(uScorecard[i]), FormatCell(cScorecard[i]), i));
            }
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, labels[TOTAL], FormatCell(uScorecard[TOTAL]), FormatCell(cScorecard[TOTAL]), ""));
            Console.WriteLine(border);
        }
        #endregion

        #region Rolling Methods

        static void Roll(int numberOfDice, List<int> dice)
        {
            dice.Clear();
            Random random = new Random();
            for (int i = 0; i < numberOfDice; i++)
            {
                int dieValue = random.Next(1, 7);
                dice.Add(dieValue);
            }
        }

        static void DisplayDice(List<int> dice)
        {
            string lineFormat = "|   {0}  |";
            string border = "*------*";
            string second = "|      |";

            foreach (int d in dice)
                Console.Write(border);
            Console.WriteLine();
            foreach (int d in dice)
                Console.Write(second);
            Console.WriteLine("");
            foreach (int d in dice)
                Console.Write(String.Format(lineFormat, d));
            Console.WriteLine();
            foreach (int d in dice)
                Console.Write(second);
            Console.WriteLine("");
            foreach (int d in dice)
                Console.Write(border);
            Console.WriteLine();
        }
        #endregion

        #region Computer Play Methods

        static int[] GetCounts(List<int> dice)
        {
            int[] counts = new int[6];

            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 0;
            }

            foreach (int die in dice)
            {
                counts[die - 1]++;
            }

            return counts;
        }

        static int GetComputerScorecardItem(int[] scorecard, List<int> keeping)
        {
            int indexOfMax = 0;
            int max = 0;
            int[] counts = GetCounts(keeping);

            for (int i = ONES; i <= YAHTZEE; i++)
            {
                if (scorecard[i] == -1)
                {
                    int score = Score(i, counts);
                    if (score >= max)
                    {
                        max = score;
                        indexOfMax = i;
                    }
                }
            }

            return indexOfMax;
        }

        static void ComputerPlay(int[] cScorecard, ref int cScorecardCount)
        {
            List<int> keeping = new List<int>();

            Roll(5, keeping);
            Console.WriteLine("The dice the computer rolled: ");
            DisplayDice(keeping);
            Pause();
            Pause();

            int itemIndex = GetComputerScorecardItem(cScorecard, keeping);
            cScorecard[itemIndex] = Score(itemIndex, GetCounts(keeping));
            cScorecardCount++;
        }
        #endregion

        #region User Play Methods

        static void GetKeeping(List<int> rollingDice, List<int> keepingDice)
        {
            Console.WriteLine("These are the dice you rolled:");
            DisplayDice(rollingDice);

            Console.WriteLine("Which dice do you want to keep? Enter the indices separated by spaces:");
            string input = Console.ReadLine();
            string[] indices = input.Split(' ');

            foreach (string indexStr in indices)
            {
                if (int.TryParse(indexStr, out int index) && index >= 0 && index < rollingDice.Count)
                {
                    keepingDice.Add(rollingDice[index]);
                    rollingDice.RemoveAt(index);
                }
            }
        }

        static void MoveRollToKeep(List<int> rollingDice, List<int> keepingDice)
        {
            foreach (int die in rollingDice)
            {
                keepingDice.Add(die);
            }

            rollingDice.Clear();
        }

        static int GetScorecardItem(int[] scorecard)
        {
            int scorecardItem;

            do
            {
                Console.WriteLine("Choose which item on the scorecard you want to score:");
                Console.WriteLine("Enter a number between 0 and 15:");

                if (!int.TryParse(Console.ReadLine(), out scorecardItem))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 15.");
                    continue;
                }

                if (scorecardItem < 0 || scorecardItem > 15)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 15.");
                    continue;
                }

                if (scorecard[scorecardItem] != -1)
                {
                    Console.WriteLine("That scorecard item already has a value. Please choose another.");
                    continue;
                }

                break;
            } while (true);

            return scorecardItem;
        }

        static void UserPlay(int[] userScorecard, ref int userTurns)
        {
            List<int> rollingDice = new List<int>();
            List<int> keepingDice = new List<int>();
            int rolls = 0;
            int scorecardItem;

            do
            {
                Roll(5, rollingDice);
                rolls++;

                Console.WriteLine($"Roll {rolls}:");
                Console.WriteLine("The dice you rolled:");
                DisplayDice(rollingDice);
                Console.WriteLine();

                if (rolls < 3)
                {
                    GetKeeping(rollingDice, keepingDice);
                }
                else
                {
                    MoveRollToKeep(rollingDice, keepingDice);
                }

                Console.WriteLine("The dice you're keeping:");
                DisplayDice(keepingDice);
                Console.WriteLine();

            } while (rolls < 3 && keepingDice.Count < 5);

            scorecardItem = GetScorecardItem(userScorecard);
            userScorecard[scorecardItem] = Score(scorecardItem, GetCounts(keepingDice));
            userTurns++;
        }
        #endregion

        #region Scoring Methods

        static int ScoreOnes(int[] counts)
        {
            return counts[ONES] * 1;
        }

        static int ScoreTwos(int[] counts)
        {
            return counts[TWOS] * 2;
        }

        static int ScoreThrees(int[] counts)
        {
            return counts[THREES] * 3;
        }

        static int ScoreFours(int[] counts)
        {
            return counts[FOURS] * 4;
        }

        static int ScoreFives(int[] counts)
        {
            return counts[FIVES] * 5;
        }

        static int ScoreSixes(int[] counts)
        {
            return counts[SIXES] * 6;
        }

        static int ScoreThreeOfAKind(int[] counts)
        {
            if (HasCount(counts, 3))
            {
                return Sum(counts);
            }
            else
            {
                return 0;
            }
        }

        static int ScoreFourOfAKind(int[] counts)
        {
            if (HasCount(counts, 4))
            {
                return Sum(counts);
            }
            else
            {
                return 0;
            }
        }

        static int ScoreYahtzee(int[] counts)
        {
            if (HasCount(counts, 5))
            {
                return 50;
            }
            else
            {
                return 0;
            }
        }

        static int ScoreFullHouse(int[] counts)
        {
            if (HasCount(2, counts) && HasCount(3, counts))
            {
                return 25;
            }
            else
            {
                return 0;
            }
        }

        static int ScoreSmallStraight(int[] counts)
        {
            for (int i = THREES; i <= FOURS; i++)
            {
                if (counts[i] == 0)
                    return 0;
            }
            if ((counts[ONES] >= 1 && counts[TWOS] >= 1) ||
                (counts[TWOS] >= 1 && counts[FIVES] >= 1) ||
                (counts[FIVES] >= 1 && counts[SIXES] >= 1))
                return 30;
            else
                return 0;
        }

        static int ScoreLargeStraight(int[] counts)
        {
            for (int i = TWOS; i <= FIVES; i++)
            {
                if (counts[i] == 0)
                    return 0;
            }
            if (counts[ONES] == 1 || counts[SIXES] == 1)
                return 40;
            else
                return 0;
        }

        static int ScoreChance(int[] counts)
        {
            return Sum(counts);
        }

        static int Score(int whichElement, int[] counts)
        {
            switch (whichElement)
            {
                case ONES:
                    return ScoreOnes(counts);
                case TWOS:
                    return ScoreTwos(counts);
                case THREES:
                    return ScoreThrees(counts);
                case FOURS:
                    return ScoreFours(counts);
                case FIVES:
                    return ScoreFives(counts);
                case SIXES:
                    return ScoreSixes(counts);
                case THREE_OF_A_KIND:
                    return ScoreThreeOfAKind(counts);
                case FOUR_OF_A_KIND:
                    return ScoreFourOfAKind(counts);
                case FULL_HOUSE:
                    return ScoreFullHouse(counts);
                case SMALL_STRAIGHT:
                    return ScoreSmallStraight(counts);
                case LARGE_STRAIGHT:
                    return ScoreLargeStraight(counts);
                case CHANCE:
                    return ScoreChance(counts);
                case YAHTZEE:
                    return ScoreYahtzee(counts);
                default:
                    return 0;
            }
        }

        static bool HasCount(int[] counts, int howMany)
        {
            foreach (int count in counts)
                if (howMany == count)
                    return true;

            return false;
        }

        static int Sum(int[] counts)
        {
            int sum = 0;
            for (int i = ONES; i <= SIXES; i++)
            {
                int value = i + 1;
                sum += (value * counts[i]);
            }

            return sum;
        }

        static int Count(int valueToCount, List<int> dice)
        {
            int count = 0;
            foreach (int die in dice)
            {
                if (die == valueToCount)
                {
                    count++;
                }
            }
            return count;
        }

        #endregion

        static void Pause()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }
}