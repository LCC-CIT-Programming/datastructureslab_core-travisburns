
//Using the Visual Studio Solution provided in the starting files as a starting point,
//write an application that asks the user to enter a string from the keyboard and prints the string in reverse order.
//Use a stack to reverse the string.



//Input 
//User Input(string)


//Proccessing
//Stores user input in a string vairable 
//Use a stack to reverse the string 
//Define a stack with char type named charStack equal to a new instance of its type. 
//set up a for each loop with char in str found
//within the loop push the char 
//outside the loop create a char array called reversedChars and set it equal to a new char with the full legnth of the string. \
//setup a empty integer to be used later. 
//setup a while loop with charStack's count being greater than zero
//while this condition is met, take reversedChars at its iteration and set it equal to charStack.pop
//return the string


//OutPut
//Reversed String
using System;
using System.Collections.Generic;

namespace StringReverseStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a word:");
            string userInput = Console.ReadLine();
            string reversedString = ReverseString(userInput);
            Console.WriteLine("Reversed string: " + reversedString);
        }

        static string ReverseString(string str)
        {
            Stack<char> charStack = new Stack<char>();

            foreach (char c in str)
            {
                charStack.Push(c);
            }

            char[] reversedChars = new char[str.Length];
            int i = 0;
            while (charStack.Count > 0)
            {
                reversedChars[i++] = charStack.Pop();
            }

            return new string(reversedChars);
        }
    }
}