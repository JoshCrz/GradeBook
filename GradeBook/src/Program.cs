using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            IBook book = new DiskBook("Sctott's Grade Book");

            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();

            book.Name = "";
            System.Console.WriteLine($"For the book named: {book.Name}");
            System.Console.WriteLine($"Low: {stats.Low}");
            System.Console.WriteLine($"High: {stats.High}");
            System.Console.WriteLine($"Average: {stats.Average:N1}");

        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or enter 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;                    
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                //catch exceptions
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    //this will always execute after a catch is successful
                    //useful if you need to close a file or close a network socket
                    System.Console.WriteLine("**");
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            System.Console.WriteLine("A grade was added");
        }
        
    }
}
