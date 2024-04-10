// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
//namescape for what we are going to use in every class
namespace Recipe 
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You will now create your recipe"); //introductory title for the user

            
            Console.Write("Enter the number of ingredients: "); //prompt for user input for nr of ingredients
            int numIngredients = int.Parse(Console.ReadLine()); //string manipulation for int.parse when the program takes in user input


            // These are our arrays that will serve to store user inputs
            string[] ingredientNames = new string[numIngredients];
            double[] ingredientQuantities = new double[numIngredients];
            string[] ingredientUnits = new string[numIngredients];

            
            for (int i = 0; i < numIngredients; i++) //for loop which will re-iterate everytime depending on how many ingredients the user has chosen to have in their recipe 
            {
                Console.WriteLine($"\nIngredient {i + 1}:"); //how many ingredients will be implemented
                Console.Write("Name: "); //name of the ingredient
                ingredientNames[i] = Console.ReadLine();
                Console.Write("Quantity: "); //quantity in integer form
                ingredientQuantities[i] = double.Parse(Console.ReadLine());
                Console.Write("Unit of Measurement: "); //the unit of measurement to be used 
                ingredientUnits[i] = Console.ReadLine();
            }

            
            Console.Write("\nEnter the number of steps: "); //prompts user to enter how many steps they require for their ingredient
            int nrSteps = int.Parse(Console.ReadLine());

            
            string[] steps = new string[nrSteps]; //array which will hold the number of steps in the recipe

            for (int i = 0; i < nrSteps; i++) //for loop which will iterate for the amount of steps the user requires 
            {
                Console.WriteLine($"\nStep {i + 1}:"); //retrieve nr steps for iteration
                Console.Write("Description: "); //prompts user to enter a description for their step
                steps[i] = Console.ReadLine();
            }

            Console.WriteLine("\nRecipe Details:"); //displays user recipe details 
            Console.WriteLine("\nIngredients:"); //displays user recipe ingredients
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}"); //calls arrays to display all stored values for final recipe details.
            }

            Console.WriteLine("\nSteps:"); //displays steps for the following final result
            for (int i = 0; i < nrSteps; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }
    }
}