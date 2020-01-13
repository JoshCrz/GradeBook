using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args) 
        {
            
            var book = new Book("testo");            
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(24.5);

            var stats = book.GetStatistics();

            System.Console.WriteLine($"Low: {stats.Low}");
            System.Console.WriteLine($"High: {stats.High}");
            System.Console.WriteLine($"Average: {stats.Average:N1}");

        }
    }
}
