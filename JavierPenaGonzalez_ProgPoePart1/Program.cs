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
            Console.WriteLine("You will now create your recipe"); //prompts user to enter recipe

            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());

            string[] ingredientNames = new string[numIngredients]; //create arrays to hold values
            double[] ingredientQuantities = new double[numIngredients];
            string[] ingredientUnits = new string[numIngredients];

            for (int i = 0; i < numIngredients; i++) //for loop interates the amount of times the user inputs the amount of ingredients,quantity
            {
                Console.WriteLine($"\nIngredient {i + 1}:");
                Console.Write("Name: ");
                ingredientNames[i] = Console.ReadLine();
                Console.Write("Quantity: ");
                ingredientQuantities[i] = double.Parse(Console.ReadLine());
                Console.Write("Unit of Measurement: ");
                ingredientUnits[i] = Console.ReadLine();
            }

            Console.Write("\nEnter the number of steps: "); //prompts user for nr of steps
            int nrSteps = int.Parse(Console.ReadLine());

            string[] steps = new string[nrSteps];

            for (int i = 0; i < nrSteps; i++) //for loop iterates for amount of steps user inputs
            {
                Console.WriteLine($"\nStep {i + 1}:");
                Console.Write("Description: ");
                steps[i] = Console.ReadLine();
            }

            Console.WriteLine("\nRecipe Details:"); //next lines display the final details of the user inputs
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

            // scaling function
            scalingFunction scaling = new scalingFunction(ingredientQuantities);

            bool continueScaling = true;
            while (continueScaling)
            {
                Console.Write("\nDo you want to scale the ingredient quantities? (y/n): "); //prompt user with yes and no inputs for scaling choice
                string response = Console.ReadLine().ToLower();

                if (response == "y") //simple if else to execute decision
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

            // Revert the scaling done to its original values and display
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
            Array.Copy(originalQuantities, this.scaledQuantities, originalQuantities.Length); //copies the array values for when the scaling occurs, it can still display with fixed values
        }

        public void ScaleIngredients(double scaleFactor)
        {
            for (int i = 0; i < originalQuantities.Length; i++) //for loop which iterates through the amount of quantities to scale all of them
            {
                scaledQuantities[i] = originalQuantities[i] * scaleFactor;
            }
        }

        public void RevertScaling()
        {
            Array.Copy(originalQuantities, scaledQuantities, originalQuantities.Length); //this will return the copied array of the original values to revert them
        }

        public void DisplayIngredients(string[] ingredientNames, string[] ingredientUnits) //displays ingredients after scaling changes or reverts.
        {
            for (int i = 0; i < scaledQuantities.Length; i++)
            {
                Console.WriteLine($"{scaledQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }
        }
    }
}