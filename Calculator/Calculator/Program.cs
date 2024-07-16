using System;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Simple Calculator");
                Console.WriteLine();
                Console.WriteLine("Enter first number");
                string input1 = Console.ReadLine();
                if (!isValidNumber(input1, out double number1)) continue;
                Console.WriteLine("Enter an operator");
            invalidoperator:
                string inputOperator = Console.ReadLine();
                if (inputOperator.Trim().ToLower() == "exit") break;
                if (!isValidOperator(inputOperator))
                {
                    Console.WriteLine("Please Enter again");
                    goto invalidoperator;
                };
                Console.WriteLine("Enter second number");
            invalidinput:
                string input2 = Console.ReadLine();
                if (input2.Trim().ToLower() == "exit") break;
                if (!isValidNumber(input2, out double number2))
                {
                    Console.WriteLine("Please Enter number again");
                    goto invalidinput;
                }
                double result = calculation(number1, number2, inputOperator);
                Console.WriteLine($"Result : {result}");

            }

        }
        static bool isValidNumber(string input, out double number)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("input cannot be null");
                number = 0;
                return false;
            }
            if (!double.TryParse(input, out number))
            {
                Console.WriteLine("Enter Valid Number!");
                return false;
            }
            return true;
        }
        static bool isValidOperator(string operation)
        {
            if (string.IsNullOrWhiteSpace(operation))
            {
                Console.WriteLine("operation cannot be null");
                return false;
            }
            if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
            {
                Console.WriteLine("Enter Valid Operator!");
                return false;
            }
            return true;
        }
        static double calculation(double number1, double number2, string operation)
        {
            double result = 0;
            switch (operation)
            {
                case "+":
                    result = number1 + number2;
                    break;
                case "-":
                    result = number1 - number2;
                    break;
                case "*":
                    result = number1 * number2;
                    break;
                case "/":
                    result = number1 / number2;
                    break;
            }
            return result;
        }
    }
}
