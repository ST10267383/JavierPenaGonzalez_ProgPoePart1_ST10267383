// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
//namescape for what we are going to use in every class
namespace JavierPenaGonzalez_ProgPoePart1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You will now create your recipe");

            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());

            string[] ingredientNames = new string[numIngredients];
            double[] ingredientQuantities = new double[numIngredients];
            string[] ingredientUnits = new string[numIngredients];

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"\nIngredient {i + 1}:");
                Console.Write("Name: ");
                ingredientNames[i] = Console.ReadLine();
                Console.Write("Quantity: ");
                ingredientQuantities[i] = double.Parse(Console.ReadLine());
                Console.Write("Unit of Measurement: ");
                ingredientUnits[i] = Console.ReadLine();
            }

            Console.Write("\nEnter the number of steps: ");
            int nrSteps = int.Parse(Console.ReadLine());

            string[] steps = new string[nrSteps];

            for (int i = 0; i < nrSteps; i++)
            {
                Console.WriteLine($"\nStep {i + 1}:");
                Console.Write("Description: ");
                steps[i] = Console.ReadLine();
            }

            Console.WriteLine("\nRecipe Details:");
            Console.WriteLine("\nIngredients:");
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < nrSteps; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }

            // scaling 
            scalingFunction scaling = new scalingFunction(ingredientQuantities);

            bool continueScaling = true;
            while (continueScaling)
            {
                Console.Write("\nDo you want to scale the ingredient quantities? (y/n): ");
                string response = Console.ReadLine().ToLower();

                if (response == "y")
                {
                    Console.Write("Enter the scale factor (0.5 for half, 2 for double, 3 for triple): ");
                    double scaleFactor = double.Parse(Console.ReadLine());
                    scaling.ScaleIngredients(scaleFactor);
                    Console.WriteLine("\nScaled Recipe Details:");
                    scaling.DisplayIngredients(ingredientNames, ingredientUnits);
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

            // Revert 
            Console.Write("\nDo you want to revert any scaling done to the original values? (y/n): ");
            string revertResponse = Console.ReadLine().ToLower();
            if (revertResponse == "y")
            {
                scaling.RevertScaling();
                Console.WriteLine("\nReverted to original quantities:");
                scaling.DisplayIngredients(ingredientNames, ingredientUnits);
            }
            else
            {
                Console.WriteLine("\nFinal recipe details remain as scaled.");
            }
        }
    }

    class scalingFunction
    {
        private double[] originalQuantities;
        private double[] scaledQuantities;

        public scalingFunction(double[] originalQuantities)
        {
            this.originalQuantities = originalQuantities;
            this.scaledQuantities = new double[originalQuantities.Length];
            Array.Copy(originalQuantities, this.scaledQuantities, originalQuantities.Length);
        }

        public void ScaleIngredients(double scaleFactor)
        {
            for (int i = 0; i < originalQuantities.Length; i++)
            {
                scaledQuantities[i] = originalQuantities[i] * scaleFactor;
            }
        }

        public void RevertScaling()
        {
            Array.Copy(originalQuantities, scaledQuantities, originalQuantities.Length);
        }

        public void DisplayIngredients(string[] ingredientNames, string[] ingredientUnits)
        {
            for (int i = 0; i < scaledQuantities.Length; i++)
            {
                Console.WriteLine($"{scaledQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }
        }
    }
}