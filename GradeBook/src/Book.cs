using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{

    public delegate void GradeAddedDelegate(object sender, EventArgs args);


    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
         //challenge, open file witgh same name, writre new linbe into the file with grade value
         //using will automatically clean up at runtime
         using (var writer = File.AppendText($"{Name}.txt")){
            writer.WriteLine(grade);
            if(GradeAdded != null){
                GradeAdded(this, new EventArgs());
            }
            //writer.Dispose();     this now automatically happens due to the 'using';
         }         
        }
        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    //deriving from a base class
    public class NamedObject {        

        //constructor
        //base() is accessing the constructor of the parent class
        public NamedObject(string name){
            Name = name;
        }

        public string Name {
            get;
            set;
        }
    }


    //convention of type interface, should begin with uppercase 'I'
    public interface IBook 
    {        
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    //abstract class
    public abstract class Book : NamedObject, IBook
    {        
        //use the constructor's parameter and forward that to the base class constructor
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        //an abstract method is a way of allowing derived classes to inherit the method without actually knowing what it does..
        //so the child classes would implement and define what the method is doing
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();       
    }


    //InMemoryBook is inherited from Book    
    public class InMemoryBook : Book
    {
        //public is an access modifier, it means whether or not the field definition(var) or Method can be accessed from outside the Class

        //constructor method, so name is required;        
        public InMemoryBook(string name) : base(name)
        {
            //must have same name as class, and cannot have a return type
            //grades = new List<double>(){25.2, 10.7, 22.3, 87.4};  

            Name = name;
            grades = new List<double>();

        }

        //override, this overrides whatever the base class is providing (as this method is an abstract method on the base class)
        public override void AddGrade(double grade)
        {
                
            if(grade <= 100 && grade >= 0){                
                    this.grades.Add(grade);   
                    if(GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
            } else {
                throw new ArgumentException($"Invalid {nameof(grade)}");   //exception must be caught, otherwise program will crash       
            }
        }


        public override event GradeAddedDelegate GradeAdded;


        public void ShowStatistics(){
            Statistics result = this.GetStatistics();

            System.Console.WriteLine($"Low: {result.Low}");
            System.Console.WriteLine($"High: {result.High}");
            System.Console.WriteLine($"Average: {result.Average:N1}");
        }

        //InMemoryBook is defining this method below, but it's also inheriting the same method name from the interface IBook, so use override 
        //as we do for override
        public override Statistics GetStatistics(){
            var stats = new Statistics();

            stats.Average = this.GetAverage(this.grades);
            stats.Low = this.GetLowest(this.grades);
            stats.High = this.GetHighest(this.grades);
            
            return stats;

            //for loop
            
            

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
         //public string Name;


        public string NewName {
            //auto property
            get; 
            set;
            //private set;
        }

        /*
         public string Name
         {
             get
             {//this code runs if someone wants to read the name property
                return name;
             }
             set 
             {//value is implicit variable, it will always be there, it will represent what the value is that someone is trying to write into property
                if(!String.IsNullOrEmpty(value)){
                name = value;    
                } else {
                    //user is trying to set null or empty string as my value
                }
                
             }
         }
        */ 

        //backing field 
        private string name;    


        //readonly can only be changed when initialised (ie, in a constructor)
        readonly string category = "Science";

        //const - cannot change the value of the const.
        //many devs will uppercase const's
        const string CATEGORYTWO = "Physics";

    }
}