using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class Book
    {
        //public is an access modifier, it means whether or not the field definition(var) or Method can be accessed from outside the Class

        public Book(string name)
        {
            //constructor method;
            //must have same name as class, and cannot have a return type
            //grades = new List<double>(){25.2, 10.7, 22.3, 87.4};  

            Name = name;
            grades = new List<double>();

        }

        public void AddGrade(double grade)
        {
            if(grade <= 100 || grade >= 0){
                if(grade >= 0){
                    this.grades.Add(grade);   
                }                
            } else {
                System.Console.WriteLine("Invalid value");
            }
        }


        public void ShowStatistics(){
            Statistics result = this.GetStatistics();

            System.Console.WriteLine($"Low: {result.Low}");
            System.Console.WriteLine($"High: {result.High}");
            System.Console.WriteLine($"Average: {result.Average:N1}");
        }

        public Statistics GetStatistics(){
            var stats = new Statistics();

            stats.Average = this.GetAverage(this.grades);
            stats.Low = this.GetLowest(this.grades);
            stats.High = this.GetHighest(this.grades);
            
            
            //for loop
            
            //return stats;

            /*
            code from the video, using a do,while            
            var index = 0;
            do {
            stats.Low = Math.Min(grades[index], stats.Low);
            stats.High = Math.Max(grades[index], stats.High);
            stats.Average += grades[index];
            index++;
            } while(index < grades.Count);
            */            
        }

        private double GetHighest(List<double> grades){            
            double result = double.MinValue;
            foreach(double grade in grades){
                result = Math.Max(grade, result);
            }
            return result;
        }

        private double GetLowest(List<double> grades){
            double result = double.MaxValue;

            foreach(double grade in grades){
                result = Math.Min(grade, result);
            }
            return result;
        }

        private double GetAverage(List<double> grades){
            double result = 0.0;
            foreach(double grade in grades){
                result += grade;                
            }
            result /= grades.Count;
            return result;
        }

        //code cannot access this private field definition as it's private, it's local only to Book Class

         private List<double> grades;
         public string Name;

    }
}