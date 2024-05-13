// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;
//namescape for what we are going to use in every class
namespace JavierPenaGonzalez_ProgPoePart1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine("Welcome to the recipe app, we will now take in your recipe");

                Console.Write("Enter the number of ingredients you would like to have in this recipe: ");
                int numIngredients = int.Parse(Console.ReadLine());

                ArrayList ingredients = new ArrayList(); // Change to ArrayList

                ArrayList ingredientQuantity = new ArrayList(); // Change to ArrayList

                ArrayList ingredientUnits = new ArrayList(); // Change to ArrayList

                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine($"\n Ingredient nr {i + 1}:");
                    Console.Write("Name: ");
                    ingredients.Add(Console.ReadLine());
                    Console.Write("Quantity to be used: ");
                    ingredientQuantity.Add(double.Parse(Console.ReadLine()));
                    Console.Write("Unit of Measurement for ingredients: ");
                    ingredientUnits.Add(Console.ReadLine());
                }

                Console.Write("\nEnter the number of steps: ");
                int nrSteps = int.Parse(Console.ReadLine());

                ArrayList steps = new ArrayList(); // Change to ArrayList

                for (int i = 0; i < nrSteps; i++)
                {
                    Console.WriteLine($"\nStep {i + 1}:");
                    Console.Write("Description: ");
                    steps.Add(Console.ReadLine());
                }

                Console.WriteLine("\nRecipe Details:");
                Console.WriteLine("\nIngredients:");

                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine($"{ingredientQuantity[i]} {ingredientUnits[i]} of {ingredients[i]}");
                }

                Console.WriteLine("\nSteps:");

                for (int i = 0; i < nrSteps; i++)
                {
                    Console.WriteLine($"{i + 1}. {steps[i]}");
                }

                // ScalingFunction Functionality
                ScalingFunction scaling = new ScalingFunction(ingredients, ingredientQuantity, ingredientUnits, steps);

                bool continueScaling = true;

                while (continueScaling)
                {
                    Console.Write("\nDo you want to scale the ingredient quantities? y- For yes, n - for No: ");
                    string response = Console.ReadLine().ToLower();

                    if (response == "y")
                    {
                        Console.Write("Enter the scale factor (0.5 for half, 2 for double, 3 for triple): ");
                        double scaleFactor = double.Parse(Console.ReadLine());
                        scaling.ScaleIngredients(scaleFactor);
                        Console.WriteLine("\nScaled Recipe Details:");
                        scaling.DisplayIngredients();
                    }
                    else if (response == "n")
                    {
                        continueScaling = false;
                        Console.WriteLine("\nRecipe details remain unchanged.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    }
                }

                // Ask if the user wants to continue or exit
                Console.Write("\nDo you want to clear the stored data and enter a new recipe? y- For yes, n - for No: ");
                string continueResponse = Console.ReadLine().ToLower();
                if (continueResponse != "y")
                {
                    continueProgram = false;
                    Console.WriteLine("\nYour recipe has now been saved and compiled!");
                }
            }
        }
    }
}
//Pro C# 10 with .NET 6, Troelsen, Andrew; Japikse, phil. 11th edition,Chambersburg,PA. 2022, apress
//S, J., 2010. Stackoverflow. [Online] 
//Available at: https://stackoverflow.com/questions/2675196/c-sharp-method-to-scale-values
//[Accessed 11 April 2024].
