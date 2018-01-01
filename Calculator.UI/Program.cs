using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please note:");
            Console.WriteLine("1) the calculator ignores whitespace;");
            Console.WriteLine("2) only the following operators are supported: - + * /");
            Console.WriteLine("3) to exit normally type 'quit'.");
            Console.WriteLine("Provide an expression to calculate:");
            var calculator = new Calculator();
            while(true)
            {
                var input = Console.ReadLine();
                if (input.Equals("quit", StringComparison.InvariantCultureIgnoreCase)) break;

                try
                {
                    var result = calculator.Feed(input);
                    Console.Write("Result is ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result);
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error: ");
                    Console.ResetColor();
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("-===-");
            }
        }
    }
}
