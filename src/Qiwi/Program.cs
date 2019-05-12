using System;
using System.Text;

namespace Qiwi
{
    class Program
    {
        private const string ForExample = "For example, '5 +77777777 +88888888'.";
        private static readonly string InvalidInputString = string.Format(
            "Invalid input string.{0}" +
            "Please the enter correct input string.{0}{1}",
            Environment.NewLine,
            ForExample);

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Please enter the input string. {ForExample}");
                    var input = Console.ReadLine();

                    if (input == "exit") break;

                    Console.WriteLine(GetOutput(input));
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
        }

        static string GetOutput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return InvalidInputString;

            var splitedInput = input.Split(' ');

            if (!int.TryParse(splitedInput[0], out int n) || n < 0)
                return InvalidInputString;

            var output = new StringBuilder();
            var persons = PersonParser.CreateQueueFromPhones(splitedInput);
            
            for(var i = 0; i < n; i++)
            {
                if (persons.Count == 0) break;

               output.Append($"{persons.Dequeue().Phone} ");
            }

            return output.ToString();
        }
    }
}
