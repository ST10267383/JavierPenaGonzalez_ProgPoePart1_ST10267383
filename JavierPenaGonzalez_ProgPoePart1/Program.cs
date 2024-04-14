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
            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine("Welcome to the recipe app, we will now take in your recipe"); //input user info

                Console.Write("Enter the number of ingredients you would like to have in this recipe: "); //prompt user to enter ingredient number to used later within iterating loop
                int numIngredients = int.Parse(Console.ReadLine());
                
                string[] ingredientNames = new string[numIngredients]; //declare arrays
                double[] ingredientQuantities = new double[numIngredients];
                string[] ingredientUnits = new string[numIngredients];

                            for (int i = 0; i < numIngredients; i++) //for loop to iterate through user inputs
                            {
                                Console.WriteLine($"\n Ingredient nr {i + 1}:"); //collects user input for ingredients
                                Console.Write("Name: ");
                                ingredientNames[i] = Console.ReadLine();
                                Console.Write("Quantity to be used: "); //collects quantity of ingredients
                                ingredientQuantities[i] = double.Parse(Console.ReadLine());
                                Console.Write("Unit of Measurement for ingredients: "); //collects the unit of measurement that the user requires for their ingredients
                                ingredientUnits[i] = Console.ReadLine();
                            }

                Console.Write("\nEnter the number of steps: "); //prompts user to input their number of steps for their recipe
                int nrSteps = int.Parse(Console.ReadLine());

                string[] steps = new string[nrSteps];

                            for (int i = 0; i < nrSteps; i++) //reiterates the descriptions from the arrays to the output depending on amount of descriptions the user input
                            {
                                Console.WriteLine($"\nStep {i + 1}:"); //indicates to user what step is being iterated
                                Console.Write("Description: "); //indicates the user to input their description
                                steps[i] = Console.ReadLine(); //reads user input for above lines
                            }

                Console.WriteLine("\nRecipe Details:");
                Console.WriteLine("\nIngredients:");

                            for (int i = 0; i < numIngredients; i++) //iterates depending on number of ingredients
                            {
                                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}"); //displays recipe details
                            }

                Console.WriteLine("\nSteps:");

                            for (int i = 0; i < nrSteps; i++)
                            {
                                Console.WriteLine($"{i + 1}. {steps[i]}");
                            }

                // scalingFunction Functionality
                scalingFunction scaling = new scalingFunction(ingredientQuantities);

                bool continueScaling = true; //bool to check whether the user has wanted to scale their recipe or not

                                    while (continueScaling)
                                    {
                                        Console.Write("\nDo you want to scale the ingredient quantities? y- For yes, n - for No: ");
                                        string response = Console.ReadLine().ToLower(); //changes string to lowercase (string manipulation) as to create a legible program if user input is in full caps

                                        if (response == "y") //the function for if the user inputs yes 
                                        {
                                            Console.Write("Enter the scale factor (0.5 for half, 2 for double, 3 for triple): "); //prompts user to enter the scale factor they wish to use
                                            double scaleFactor = double.Parse(Console.ReadLine());
                                            scaling.ScaleIngredients(scaleFactor);
                                            Console.WriteLine("\nScaled Recipe Details:"); //gives user the recipe after the scale factor is applied
                                            scaling.DisplayIngredients(ingredientNames, ingredientUnits);
                                        }
                                        else if (response == "n") //runs an else if the user inputs no which moves the program along, away from the scaling
                                        {
                                            continueScaling = false;
                                            Console.WriteLine("\nRecipe details remain unchanged.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter 'y' or 'n'."); //returns user to invalid input incase they didnt type y or n
                                        }
                                    }

                // Revert scalingFunction Functionality
                Console.Write("\nDo you want to revert any scaling done to the original values? y- for Yes, n - for No: ");
                string revertResponse = Console.ReadLine().ToLower();

                                    if (revertResponse == "y")
                                    {
                                        scaling.RevertScaling(); //calls revert scaling function
                                        Console.WriteLine("\nReverted to original quantities:");
                                        scaling.DisplayIngredients(ingredientNames, ingredientUnits); //displays scaling
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nFinal recipe details remain as scaled.");
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

    class scalingFunction //class that will perform the scaling function
    {
        private double[] originalQuantities;
        private double[] scaledQuantities;

        public scalingFunction(double[] originalQuantities)
        {
            this.originalQuantities = originalQuantities;
            this.scaledQuantities = new double[originalQuantities.Length];
            Array.Copy(originalQuantities, this.scaledQuantities, originalQuantities.Length); //creates a copy of the array of stored ingredients to create a possibility of both scaling all values at once, and in future, being able to scale back
        }

        public void ScaleIngredients(double scaleFactor)
        {
            for (int i = 0; i < originalQuantities.Length; i++)
            {
                scaledQuantities[i] = originalQuantities[i] * scaleFactor; //performs mathematical formula for scaling the values
            }
        }

        public void RevertScaling() //functionf or reverting the scaling to default
        {
            Array.Copy(originalQuantities, scaledQuantities, originalQuantities.Length); //once again takes the array copy
        }

        public void DisplayIngredients(string[] ingredientNames, string[] ingredientUnits) //code to finally display the final ingredients in the recipe as a final screen for the user.
        {
            for (int i = 0; i < scaledQuantities.Length; i++)
            {
                Console.WriteLine($"{scaledQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }
        }
    }
}