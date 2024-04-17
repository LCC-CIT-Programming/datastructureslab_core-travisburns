//Using the Visual Studio Solution provided in the starting files as a starting point,
//write an application that asks the user to enter a positive integer from the keyboard
//and prints the binary numbers from 1 to that number.  Use a queue to generate the binary
//numbers.

//In mathematics and digital electronics, a binary number is a number expressed in the base-2
//numeral system or binary numeral system, which uses only two symbols: typically "0" (zero)
//and "1" (one).


//Input
//user enters a number(must be positive, this is a int)



//Processing
//store the int as a variable
//generate a set of binary numbers using a queue
//this means taking the base number entered and dividing it by 2,
//then taking the qoutent's of the base and dividing it by 2

//For example- user enters 12, so 12/2 = 6 so no remainders(0)
//the next qoutent is 6, 6 / 2 = 3 so no remainders(0)
//the next qoutent is 3, 3/2 equals 1 but there is a remainder(1)
//the next qoutent is 1, 1/2 equals 1 with one remainder(1)
//the binary number to 12 is 1100


// Start by dividing the entered integer by 2
// Enqueue the remainder into the queue
// Continue dividing the quotient by 2 until it becomes zero:
// For each division, enqueue the remainder into the queue
// After all divisions are completed, dequeue the remainders from the queue to construct the binary representation
// Output the printed binary numbers from 1 up to the entered integer




//Output
//printed binary numbers from the number




using System;
using System.Collections.Generic;

namespace BinaryNumbersQueue
{
    class Program
    {
        static void Main()
        {
            //prompt user to enter a number that is positive
            Console.Write("Enter a positive number: ");
            //store the number in a int num that parses it
            int num = int.Parse(Console.ReadLine());
            //output of the program that will give the binary number
            Console.WriteLine("Binary representation of " + num + " is: " + ConvertToBinary(num));
        }
        // creation of a ConvertToBinary string that passes a int called num
        static string ConvertToBinary(int num)
        {
            //if num is equal to 0, then return 0. note that it is a string 0 not a int or decimal
            if (num == 0)
            {
                return "0";
            }
            // create a new instance of a Queue Data structure that is a int type called remainders
            Queue<int> remainders = new Queue<int>();
            //while num is greater than 0, have remainders enqueue num's remainder of 2
            // have num divide by 2
            while (num > 0)
            {
                remainders.Enqueue(num % 2);
                num /= 2;
            }
            //create a string binary and set it equal to a empty string
            string binary = "";
            //while remainders count is greater than zero, have binary equal to the remainders dequeue plus the binary
            while (remainders.Count > 0)
            {
                binary = remainders.Dequeue() + binary;
            }
            //return as a binary number
            return binary;
        }
    }
}