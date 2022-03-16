namespace Calculator
{
    class Program
    {
        public static void Main()
        {
            while (true)
            {
                var calculator = new CalculatorProperties();

                Console.WriteLine("Please chose input method:\n " 
                                + "c - input from console \n "
                                + "f - input from the file \n "
                                + "e - for exit");

                string inputMethod;

                do
                {
                    inputMethod = Console.ReadLine();

                    if (string.IsNullOrEmpty(inputMethod))
                    {
                        Console.WriteLine("Empty input. Please try again");
                    }
                }
                while (string.IsNullOrEmpty(inputMethod));

                if (inputMethod == "c")
                {
                    Console.WriteLine("Please enter an expression:");
                    Console.WriteLine("Or if you want to exit - press \"$\"");

                    while (true)
                    {
                        string inputStr;

                        do
                        {
                            inputStr = Console.ReadLine();

                            if (string.IsNullOrEmpty(inputStr))
                            {
                                Console.WriteLine("Empty input. Please try again");
                            }
                        }
                        while (string.IsNullOrEmpty(inputStr));

                        var stringToProperties = calculator.GetExpression(inputStr);

                        if (stringToProperties == null)
                        {
                            Console.WriteLine("Invalid input! Please enter the expressio without letter");
                        }
                        else
                        {
                            var calculationResult = calculator.Counting(stringToProperties);
                            Console.WriteLine(calculationResult);
                        }
                    }
                }
                else if (inputMethod == "f")
                {
                    Console.WriteLine("Please enter a path to the source file:");

                    string inputStr;

                    do
                    {
                        inputStr = Console.ReadLine();

                        if (string.IsNullOrEmpty(inputStr))
                        {
                            Console.WriteLine("Empty input, please try again");
                        }
                    }
                    while (string.IsNullOrEmpty(inputStr));

                    List<string> filePath = File.ReadAllLines(inputStr).ToList();
                    List<string> outputReasult = new List<string>();

                    foreach (var item in filePath)
                    {
                        var stringToProperties = calculator.GetExpression(item);

                        if (stringToProperties == null)
                        {
                            outputReasult.Add("Invalid Input!");
                        }
                        else
                        {
                            var calculationReasult = calculator.Counting(stringToProperties);
                            string resultToString = calculationReasult.ToString();
                            outputReasult.Add(resultToString);
                        }
                    }

                    Console.WriteLine("Please enter a path for the result file");

                    string resultPath = Console.ReadLine();

                    var newList = filePath.Join(outputReasult, s => filePath.IndexOf(s),i => outputReasult.IndexOf(i),(s,i) => new {sv = s, iv = i}).ToList();

                    using (TextWriter tw = new StreamWriter(resultPath))
                    {
                        foreach (var item in newList)
                        {
                            tw.WriteLine(string.Format("{0} = {1}", item.sv, item.iv));
                        }
                    }

                    Console.WriteLine("Result has been added to the file");
                }
                else if (inputMethod == "e")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please select one of provided options");
                }
            }
        }
    }
}