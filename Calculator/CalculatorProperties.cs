using System.Collections.Generic;

namespace Calculator
{
    public class CalculatorProperties
    {
        public CalculatorProperties() { }

        #region Symbol Determining
        private bool IsDelimiter(char c)
        {
            if((" =").IndexOf (c) != -1)
                return true;
            return false;
        }

        private bool IsOperator(char c)
        {
            if (("+-*/^()").IndexOf (c) != -1)
                return true;
            return false;
        }

        private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default : return 6;
            }
        }
        #endregion

        public string GetExpression(string inputExpression)
        {
            string outputResult = string.Empty;

            Stack<char> operatorStack = new Stack<char>();

            for (int i = 0; i < inputExpression.Length; i++)
            {
                if (IsDelimiter(inputExpression[i]))
                {
                    continue;
                }

                if (char.IsDigit(inputExpression[i]))
                {
                    while (!IsDelimiter(inputExpression[i]) && !IsOperator(inputExpression[i]))
                    {
                        outputResult += inputExpression[i];
                        i++;

                        if (i == inputExpression.Length)
                        {
                            break;
                        }
                    }
                    outputResult += " ";
                    i--;
                }
                else if (char.IsLetter(inputExpression[i]))
                {
                    return null;
                }
                else if (inputExpression[i] == '$')
                {
                    Environment.Exit(0);
                }

                if (IsOperator(inputExpression[i]))
                {
                    if (inputExpression[i] == '(')
                    {
                        operatorStack.Push(inputExpression[i]);
                    }
                    else if (inputExpression[i] == ')')
                    {
                        char s = operatorStack.Pop();

                        while (s != '(')
                        {
                            outputResult += s.ToString() + " ";
                            s = operatorStack.Pop();
                        }
                    }
                    else
                    {
                        if (operatorStack.Count > 0)
                            if (GetPriority(inputExpression[i]) <= GetPriority(operatorStack.Peek()))
                            {
                                outputResult += operatorStack.Pop().ToString() + " ";
                            }

                        operatorStack.Push(char.Parse(inputExpression[i].ToString()));
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                outputResult += operatorStack.Pop() + " ";
            }

            return outputResult;
        }

        public double Counting(string input)
        {
            double result = 0;

            Stack<double> tempStack = new Stack<double>();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimiter(input[i]) && !IsOperator(input[i]))
                    {
                        a += input[i];
                        i++;

                        if (i == input.Length)
                        {
                            break;
                        }
                    }
                    tempStack.Push(double.Parse(a));
                    i--;
                }
                else if (char.IsLetter(input[i]))
                {
                    break;
                }
                else if (IsOperator(input[i]))
                {
                    double a = tempStack.Pop();
                    double b = tempStack.Pop();

                    switch (input[i])
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': 
                            if (a == 0)
                            {
                                Console.WriteLine("Error! Division by 0!");
                                break;
                            }
                            else
                            {
                                result = b / a; break;
                            }
                    }
                    tempStack.Push(result);
                }
            }
            return tempStack.Peek();
        }
    }
}
