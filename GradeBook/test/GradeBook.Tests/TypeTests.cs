using System;
using Xunit;

namespace GradeBook.Tests
{

    //delegate 
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {

        [Fact]
        public void WriteLogDelegateCanPointToMethod(){
            WriteLogDelegate log;

            log = new WriteLogDelegate(ReturnMessage);

            var result = log("Hello");
            Assert.Equal("Hello", result);

        }

        string ReturnMessage(string message){
            return message;
        }

        //testing my addGrade method is working as expected
        [Fact]
        public void TestAddGrade(){
            //arrange 
            //act
            var book = new Book("My book");
            book.AddGrade(105);

            System.Console.WriteLine(book.value);            
            //Assert
            Assert.Equals(book.grades[0].value, 105);
        }

        private string MakeUppercase(string z){
            return z.ToUpper();
        }

        [Fact]
        public void StringsBehaveLikeValueTypes(){
            string name = "string";
            name = MakeUppercase(name);

            Assert.Equal(name, "STRING");
        }

        [Fact]
        public void Test1(){
            var x = GetInt();
            SetInt(x);
            Assert.Equal(x, 3);
        }

        private void SetInt(int x){
            string s = "";
            x = 42;
        }

        private int GetInt(){
            return 3;
        }

        void GetBookSetName(ref Book book, string name){
            book = new Book(name);            
            book.Name = name;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");

            //pssing a var to another method will create a copy of that var, it will not change the original origin of the var
            GetBookSetName(ref book1, "New Name");
                        
            Assert.Equal("New Name", book1.Name);
        }
        

        void GetBookSetName(Book book, string name){
            book = new Book(name);            
            book.Name = name;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");

            //pssing a var to another method will create a copy of that var, it will not change the original origin of the var
            GetBookSetName(book1, "New Name");
                        
            Assert.Equal("Book 1", book1.Name);
        }
        

        void SetName(Book book, string name){
            book.Name = name;
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");
            
            //Assert.Equal("New Name", book1.Name);

        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            //arrange - put together test data and arrange obj's and value
            
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");
            //act - invoking methods to perform functions and produce results            
            
            //assert - asserting something with the value from 'act'
            
            /*Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);*/
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            //arrange - put together test data and arrange obj's and value            
            var book1 = GetBook("Book 1");
            var book2 = book1;

            //act - invoking methods to perform functions and produce results            
            
            //assert - asserting something with the value from 'act'
            
            //.Same is checking if both objects are actually the same instance
            /*Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));*/
        }


        Book GetBook(string name){
            return new Book(name);            
        }

    }
}
