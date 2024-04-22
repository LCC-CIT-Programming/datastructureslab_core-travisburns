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
            /*
             * declare variables for the user's scorecard and the computer's scorecard
             * declare a variable for the number of turns the user has taken and another for the number of moves the computer has taken
             * declare a boolean that knows if it is the user's turn and set it to false
             * 
             * call ResetScorecard for the user
             * call ResetScorecard for the computer
             * 
             * set the window size of the console window because displaying the scorecard requires about 50 lines
             * 
             * do
             *      set the userTurn variable to the opposite value
             *      call UpdateScorecard for the user
             *      call UpdateScorecard for the computer
             *      call DisplayScorecards
             *      if it's the user's turn
             *          display a message
             *          you might also want to pause
             *          call UserPlay
             *      else
             *          display a message
             *          call ComputerPlay
             *      end if
             *  while both the user's count and the computer's count are <= yahtzee
             *  
             *  call UpdateScorecard for the user
             *  call UpdateScorecard for the computer
             *  call DisplayScorecards
             *  
             *  display a message about who won
             */

            // Declare variables for the user's scorecard and the computer's scorecard
            int[] userScorecard = new int[TOTAL + 1];
            int[] computerScorecard = new int[TOTAL + 1];
            int userTurnCount = 0;
            int computerTurnCount = 0;
            bool isUserTurn = false;

            // Call ResetScorecard for the user and the computer
            ResetScorecard(userScorecard);
            ResetScorecard(computerScorecard);

            // Set the window size of the console window because displaying the scorecard requires about 50 lines
            Console.WindowHeight = 60;

            // Game loop
            do
            {
                // Switch turns
                isUserTurn = !isUserTurn;

                // Update scorecards
                UpdateScorecard(userScorecard);
                UpdateScorecard(computerScorecard);

                // Display scorecards
                DisplayScoreCards(userScorecard, computerScorecard);

                // Player's turn
                if (isUserTurn)
                {
                    Console.WriteLine("Your turn!");
                    Pause(); 
                    UserPlay(userScorecard, userTurnCount);
                }
                // Computer's turn
                else
                {
                    Console.WriteLine("Computer's turn...");
                    Pause(); 
                    ComputerPlay(computerScorecard, computerTurnCount);
                }

            } while (userTurnCount <= YAHTZEE && computerTurnCount <= YAHTZEE);

            // Update scorecards
            UpdateScorecard(userScorecard);
            UpdateScorecard(computerScorecard);

            // Display final scorecards
            DisplayScoreCards(userScorecard, computerScorecard);

            // Display winner
            if (userScorecard[TOTAL] > computerScorecard[TOTAL])
            {
                Console.WriteLine("Congratulations! You win!");
            }
            else if (userScorecard[TOTAL] < computerScorecard[TOTAL])
            {
                Console.WriteLine("Sorry, the computer wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }


            /*
            // Initialize and populate a list of dice values for testing
            List<int> dice = new List<int> { 1, 1, 1, 1, 1 };

            // Call the Count method to test
            int countOfThrees = Count(3, dice);
            int countOfFours = Count(4, dice);
            Console.WriteLine($"Count of threes: {countOfThrees}");
            Console.WriteLine($"Count of fours: {countOfFours}");

            // Call the GetCounts method to test
            int[] counts = GetCounts(dice);
            Console.WriteLine("Counts of each dice value:");
            for (int i = 0; i < counts.Length; i++)
            {
                Console.WriteLine($"Count of {i + 1}s: {counts[i]}");
            }

            // Test the Sum method
            int sum = Sum(counts);
            Console.WriteLine($"Sum of all dice values: {sum}");

            // Test the HasCount method
            int[] testCounts = { 1, 2, 2, 3, 5 };
            int howMany = 2;
            bool hasTwo = HasCount(howMany, testCounts);
            Console.WriteLine($"Has {howMany}: {hasTwo}");

            howMany = 4;
            bool hasFour = HasCount(howMany, testCounts);
            Console.WriteLine($"Has {howMany}: {hasFour}");

            // Test the ScoreChance method
            int chanceScore = ScoreChance(counts);
            Console.WriteLine($"Chance score: {chanceScore}");

            // Call the ScoreOnes - scoreSixes method to get the score 
            int onesScore = ScoreOnes(counts);
            int twosScore = ScoreTwos(counts);
            int threesScore = ScoreThrees(counts);
            int foursScore = ScoreFours(counts);
            int threeKind = ScoreThreeOfAKind(counts);
            int Yahtzee = ScoreYahtzee(counts);

            // Output the result for the score for (numbers)
            Console.WriteLine($"Score for Ones: {onesScore}");
            Console.WriteLine($"Score for Twos: {twosScore}");
            Console.WriteLine($"Score for Threes: {threesScore}");
            Console.WriteLine($"Score for Fours: {foursScore}");
            Console.WriteLine($"Score for ThreeKind: {threeKind}");
            Console.WriteLine($"Score for ScoreYahtzee: {Yahtzee}");
            */
            Console.ReadLine();
        }

        #region Scorecard Methods

        // sets all of the items in a scorecard to -1 to start the game
        // takes a data structure for a scorecard and the corresponding score card count as parameters.  Both are altered by the method.
        static void ResetScorecard(int[] scorecard)
        {
            for (int i = 0; i < scorecard.Length; i++)
            {
                scorecard[i] = NONE;
            }
        }

        // calculates the subtotal, bonus and the total for the scorecard
        // takes a data structure for a scorecard as it's parameter
        //watch for this............------------------------------------------
        static void UpdateScorecard(int[] scorecard)
        {
            // you can uncomment this code once you declare the parameter
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

        // takes the data structure for the user's scorecard and the data structure for the computer's scorecard as parameters
        static void DisplayScoreCards(int[] uScorecard, int[] cScorecard)
        {
            // you can uncomment this code when you have declared the parameters
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
        // rolls the specified number of dice and adds them to the data structure for the dice
        // takes an integer that represents the number of dice to roll and a data structure to hold the dice as it's parameters
        static void Roll(int numberOfDice, List <int> dice)
        {
            Random random = new Random();

            dice.Clear();

            //Roll for the number of dice
            for (int i = 0; i < numberOfDice; i++)
            {
                // Generate a random number between 1 and 6 for each die
                int dieValue = random.Next(1, 7);
                // Add the rolled value to the dice list
                dice.Add(dieValue);
            }

        }

        // takes a data structure that is a set of dice as it's parameter.  Call it dice.
        static void DisplayDice(List <int> dice)
        {
            // you can uncomment this code when you have declared the parameter
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

        // figures out the highest possible score for the set of dice for the computer
        // takes the scorecard datastructure and the set of dice that the computer is keeping as it's parameters
        static int GetComputerScorecardItem(int[] scorecard, List<int> keeping)
        {
            int indexOfMax = 0;
            int max = 0;

            // you can uncomment this code once you've identified the parameters for this method
            for (int i = ONES; i <= YAHTZEE; i++)
            {
                if (scorecard[i] == -1)
                {
                    int score = Score(i, keeping);
                    if (score >= max)
                    {
                        max = score;
                        indexOfMax = i;
                    }
                }
            }
            

            return indexOfMax;
        }

        // implements the computer's turn.  The computer only rolls once.
        
        // takes the computer's scorecard data structure and scorecard count as parameters.  Both are altered by the method.
        static void ComputerPlay(int[] cScorecard, int cScorecardCount)
        {
            /* you can uncomment this code once you declare the parameters
            declare a data structure for the dice that the computer is keeping.  Call it keeping.
            */
            List<int> keeping = new List<int>();
            int itemIndex = -1;

            Roll(5, keeping);
            Console.WriteLine("The dice the computer rolled: ");
            DisplayDice(keeping);
            Pause();
            Pause();

            itemIndex = GetComputerScorecardItem(cScorecard, keeping);
            cScorecard[itemIndex] = Score(itemIndex, keeping);
            cScorecardCount++;
            
        }
        #endregion

        #region User Play Methods

        // moves the dice that the user want to keep from the rolling data structure to the keeping data structure
        // takes the rolling data structure and the keeping data structure as parameters

        //--watch for a error in this
        static void GetKeeping(List<int> rollingDice, List<int> keepingDice)
        {
            // Display the dice the user rolled and prompt for input
    Console.WriteLine("Enter the index of the dice you want to keep:");
    DisplayDice(rollingDice);

            // Get user input for the indexes of dice to keep
            string input = Console.ReadLine();
            string[] indexes = input.Split(' ');

            // Convert the input indexes to integers and move the corresponding dice to the keeping dice list
            foreach (string indexStr in indexes)
            {
                if (int.TryParse(indexStr, out int index) && index >= 0 && index < rollingDice.Count)
                {
                    // Add the selected dice to the keeping dice list and remove them from the rolling dice list
                    keepingDice.Add(rollingDice[index]);
                    rollingDice.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine($"Invalid input for index: {indexStr}. Please enter valid indexes.");
                }
            }
        }

        // on the last roll moves the dice that the user just rolled into the data structure for the dice that the user is keeping
        static void MoveRollToKeep(List<int> rollingDice, List<int> keepingDice)
        {
            // iterate through the rolling data structure and copy each item into the keeping data structure
            foreach (int die in rollingDice)
            {
                keepingDice.Add(die);
            }

            // Clear the rolling dice
            rollingDice.Clear();
        }

        // asks the user which item on the scorecard they want to score 
        // must make sure that the scorecard doesn't already have a value for that item
        // remember that the scorecard is initialized with -1 in each item
        // takes a scorecard data structure as it's parameter 
        static int GetScorecardItem(int[] scorecard)
        {
            // Display the options for scoring
            Console.WriteLine("Choose an item to score:");

            Console.WriteLine("0: One");
            Console.WriteLine("1: Two");
            Console.WriteLine("2: Three");
            Console.WriteLine("3: Fours");
            Console.WriteLine("4: Fives");
            Console.WriteLine("5: Sixes");
            
            

            // Prompt for user input
            Console.Write("Enter the index of the item you want to score: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice >= scorecard.Length || scorecard[choice] != NONE)
            {
                Console.WriteLine("Invalid choice. Please enter a valid index that has not been scored yet.");
                Console.Write("Enter the index of the item you want to score: ");
            }

            return choice;
        }

        // implments the user's turn
        // takes the user's scorecard data structure and the user's move count as parameters.  Both will be altered by the method.
        static void UserPlay(int[] scorecard, int scorecardCount)
        {

            // Declare data structures for dice
            List<int> rollingDice = new List<int>(); // Dice that the user is rolling
            List<int> keepingDice = new List<int>(); // Dice that the user is keeping

            // Declare variables
            int rolls = 0; // Number of rolls
            int scorecardItem; // Scorecard item chosen by the user

            // Implement the user's turn
            do
            {
                // Roll the dice
                Roll(5 - keepingDice.Count, rollingDice);

                // Increment the number of rolls
                rolls++;

                // Display message and dice
                Console.WriteLine($"Roll {rolls}:");
                DisplayDice(rollingDice);

                // If the number of rolls is less than 3 and the user is not keeping all 5 dice
                if (rolls < 3 && keepingDice.Count < 5)
                {
                    // Get user input for which dice to keep
                    GetKeeping(rollingDice, keepingDice);
                }
                else
                {
                    // Move the remaining rolling dice to keeping dice
                    MoveRollToKeep(rollingDice, keepingDice);
                }

                // Display the dice after keeping
                Console.WriteLine("Dice after keeping:");
                DisplayDice(keepingDice);
            }
            while (rolls < 3 && keepingDice.Count < 5);

            // Get the scorecard item chosen by the user
            scorecardItem = GetScorecardItem(scorecard);

            // Score the chosen item
            int score = Score(scorecardItem, keepingDice);

            // Update the scorecard with the scored item
            scorecard[scorecardItem] = score;

            // Increment the scorecard count
            scorecardCount++;

        }

        #endregion

        #region Scoring Methods

        // counts how many of a specified value are in the set of dice
        // takes the value that you're counting and the data structure containing the set of dice as it's parameter
        // returns the count
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

        // counts the number of ones, twos, ... sixes in the set of dice
        // takes a data structure for a set of dice as it's parameter
        // returns a data structure that contains the count for each dice value
        static int[] GetCounts(List<int> dice)
        {
            // Initialize an array to hold the counts for each dice value
            int[] counts = new int[6]; // Index 0 represents ones, index 1 represents twos, and so on

            // Iterate through the dice and count occurrences of each value
            foreach (int die in dice)
            {
                // Increment the count for the corresponding value
                counts[die - 1]++; // Subtract 1 from the die value to match the array index
            }

            // Return the array containing counts for each dice value
            return counts;
        }

        // adds the value of all of the dice based on the counts
        // takes a data structure that represents all of the counts as a parameter
        static int Sum(int[] counts)
        {
            int sum = 0;
            // you can uncomment this code once you have declared the parameter
            for (int i = ONES; i <= SIXES; i++)
            {
                int value = i + 1;
                sum += (value * counts[i]);
            }
            
            return sum;
        }

        // determines if you have a specified count based on the counts
        // takes a data structure that represents all of the counts as a parameter
        static bool HasCount(int howMany, int[] counts)
        {
            // you can uncomment this when you declare the parameter
            foreach (int count in counts)
                if (howMany == count)
                    return true;
            
            return false;
        }

        // chance is the sum of the dice
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreChance(int[] counts)
        {
            if (HasCount(1, counts)) // Check if there is at least one count
            {
                return Sum(counts); // Sum all the dice
            }
            else
            {
                return 0; // If there are no counts, return 0
            }
        }

        // calculates the score for ONES given the set of counts (from GetCounts)
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreOnes(int[] counts)
        {
            // you can comment out this line when you have declared the parameters
             return counts[ONES] * 1;
            
        }

        // WRITE ALL OF THESE: ScoreTwos, ScoreThrees, ScoreFours, ScoreFives, ScoreSixes

        static int ScoreTwos(int[] counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[TWOS] * 2;
            
        }

        static int ScoreThrees(int[] counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[THREES] * 3;
           
        }

        static int ScoreFours(int[] counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[FOURS] * 4;

        }

        static int ScoreFives(int[] counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[FIVES] * 5;

        }

        static int ScoreSixes(int[] counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[SIXES] * 6;

        }

      

        // scores 3 of a kind.  4 of a kind or 5 of a kind also can be used for 3 of a kind
        // the sum of the dice are used for the score
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreThreeOfAKind(int[] counts)
        {
            int sum = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                // Check if there are at least three dice with the same value
                if (counts[i] >= 3)
                {
                    // Calculate the sum of all dice
                    for (int j = 0; j < counts.Length; j++)
                    {
                        sum += (j + 1) * counts[j];
                    }
                    break; // Once we find a three of a kind, exit the loop
                }
            }
            return sum;
        }
        // WRITE ALL OF THESE: ScoreFourOfAKind, ScoreYahtzee - a yahtzee is worth 50 points
        static int ScoreFourOfAKind(int[] counts)
        {
            int sum = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                // Check if there are at least four dice with the same value
                if (counts[i] >= 4)
                {
                    // Calculate the sum of all dice
                    for (int j = 0; j < counts.Length; j++)
                    {
                        sum += (j + 1) * counts[j];
                    }
                    break; // Once we find a four of a kind, exit the loop
                }
            }
            return sum;
        }

        // scores a yahtzee. A yahtzee is when all five dice have the same value
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreYahtzee(int[] counts)
        {
            if (HasCount(5, counts))
            {
                return 50;
            }
            else
            {
                return 0;
            }
        }

        // takes a data structure that represents all of the counts as a parameter
        static int ScoreFullHouse(int[] counts)
        {
            // you can uncomment this code once you declare the parameter
            if (HasCount(2, counts) && HasCount(3, counts))
                return 25;
            else
            
            return 0;

        }

        // takes a data structure that represents all of the counts as a parameter
        static int ScoreSmallStraight(int[] counts)
        {
            // you can uncomment this code once you declare the parameter
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

        // takes a data structure that represents all of the counts as a parameter
        static int ScoreLargeStraight(int[] counts)
        {
            // you can uncomment this code once you declare the parameter
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

        // scores a score card item based on the set of dice
        // takes an integer which represent the scorecard item as well as a data structure representing a set of dice as parameters
        static int Score(int whichElement, List <int> dice)
        {
            // you can uncomment this code once you declare the parameter
            int[] counts = GetCounts(dice);
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

        #endregion

        static void Pause()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }
}