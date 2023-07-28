// See https://aka.ms/new-console-template for more information
using DemoConsoleApp.Data;
using System;

namespace DemoConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, Sign Team!");

            InitDB.Start();
        }
    }
}