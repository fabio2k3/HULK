using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HULK
{
    class Program
    {
        static void Main(string[] args)
        {
            bool check = true;
            while (check)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    try
                    {
                        Lexer.ApplyLexer(input);
                        Parser p = new Parser(Lexer.tokens);
                        if (p.tree != null)
                            Console.WriteLine(p.Evaluate());
                    }
                    catch (MyException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Para mas info visite: " + e.HelpLink);
                    }
                }
                if (input == String.Empty)
                {
                    Console.WriteLine("Ha Terminado su experiencia con el intérprete HULK");
                    check = false;
                }
            }
        }
    }
}






