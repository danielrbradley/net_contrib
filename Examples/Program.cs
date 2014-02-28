namespace Examples
{
    using System;

    static class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Simple exmple:");
            Console.ResetColor();
            SimpleExample.Run();
        }
    }
}
