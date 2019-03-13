namespace MoreLinq.Examples
{
    using System;

    static partial class Program
    {
        static void Main()
        {
            Welcome();
        }
    }

    partial class Program
    {
        static void Welcome()
        {
            Console.WriteLine("Welcome to MoreLINQ examples!");
        }
    }
}
