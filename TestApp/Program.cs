using Ag.Tools;
using Ag.Tools.Extensions;
class Program
{
    static void Main(string[] args)
    {
        int row = 0;

        {
            AgErrorInfo errorInfo = new AgErrorInfo(AgError.E_OTHER);
            AgException exc = new AgException(errorInfo);

            string s = "HelloWorld".AgLoadString("defauuullltttt");
            Console.WriteLine(s);
            
        }

        do
        {
            if (row == 0 || row >= 25)
                ResetConsole();

            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) break;
            Console.WriteLine($"Input: {input}");
            Console.WriteLine("Begins with uppercase? " +
                 $"{(input.AgStartsWithUpper() ? "Yes" : "No")}");
            Console.WriteLine();
            row += 4;
        } while (true);
        return;

        // Declare a ResetConsole local method
        void ResetConsole()
        {
            if (row > 0)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine($"{Environment.NewLine}Press <Enter> only to exit; otherwise, enter a string and press <Enter>:{Environment.NewLine}");
            row = 3;
        }
    }
}
