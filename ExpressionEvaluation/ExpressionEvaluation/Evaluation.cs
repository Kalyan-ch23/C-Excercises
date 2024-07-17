using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace ExpressionEvaluation
{
    class Evaluation
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter your expression");
                string expression = Console.ReadLine();
                if(IsvalidExpression(expression))
                {
                    Expression e = new Expression(expression);
                    var result = e.Evaluate();
                    Console.WriteLine($"The result of the {expression} is: {result}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid expression.");
                }
                
            }
           
        }
        static bool IsvalidExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return false;
            }
            string validChars = "0123456789+-*/()";
            foreach(char c in expression)
            {
                if(!validChars.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
