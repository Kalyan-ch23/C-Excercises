using System;
using System.Collections.Generic;
using System.Globalization;
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
                    var tokens = Tokenize(expression);
                    var rpn = ConvertToRpn(tokens);
                    var result = EvaluateRPN(rpn);

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
        static List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var number = "";
            foreach(var c in expression)
            {
                if(char.IsDigit(c))

                {
                    number += c;
                }
                else
                {
                    if(!string.IsNullOrEmpty(number))
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    if (!char.IsWhiteSpace(c))
                    {
                        tokens.Add(c.ToString());
                    }
                }
            }
            if (!string.IsNullOrEmpty(number))
            {
                tokens.Add(number);
            }
            return tokens;
        }
        static List<string> ConvertToRpn(List<string> tokens)
        {
            var result = new List<string>();
            var operators = new Stack<string>();
            var precedence = new Dictionary<string, int>
            {
                { "+", 1},
                { "-", 1},
                { "*", 2},
                { "/", 2},
            };
            foreach( var token in tokens)
            {
                if(double.TryParse(token, out _))
                {
                    result.Add(token);  
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else if (token == ")")
                {
                    while(operators.Peek() != "(")
                    {
                        result.Add(operators.Pop());
                    }
                    operators.Pop();
                }
                else
                {
                    {
                        while(operators.Count > 0 && precedence.ContainsKey(operators.Peek()) && precedence[operators.Peek()] >= precedence[token])
                        {
                            result.Add(operators.Pop());
                        }
                        operators.Push(token);
                    }
                }
            }
            while (operators.Count > 0)
            {
                result.Add(operators.Pop());
            }
            return result;
        }
        static double EvaluateRPN(List<string> tokens)
        {
            var stack = new Stack<double>();
            foreach (var token in tokens)
            {
                if(double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else
                {
                    var b = stack.Pop();
                    var a = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(a+b); break;
                        case "-":
                            stack.Push(a - b); break;
                        case "*":
                            stack.Push(a * b); break;
                        case "/":
                            stack.Push(a / b); break;
                    }
                }
            }
            return stack.Pop();
        }
    }
}
