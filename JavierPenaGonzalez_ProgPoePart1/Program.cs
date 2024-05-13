// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;
//namescape for what we are going to use in every class
namespace JavierPenaGonzalez_ProgPoePart1
{
    // Define delegate type
    public delegate void CaloriesAlert(string recipeName, int totalCalories);

    class Program 
    {
        static void Main(string[] args)
        {
            List<Recipe> recipes = new List<Recipe>(); // List to store recipes

            Recipe.CaloriesExceeded += HandleCaloriesExceeded; //(HitSubscribe, 2024)

            bool continueProgram = true; //while the program keeps iterating
            while (continueProgram) 
            {
                Console.WriteLine("Welcome to the recipe app, we will now take in your recipe");

                Console.Write("Enter recipe name: ");
                string recipeName = Console.ReadLine(); //takes in user input for recipe name

                Console.Write("Enter the number of ingredients you would like to have in this recipe: ");
                int numIngredients = int.Parse(Console.ReadLine()); //changes to int 

                List<string> ingredients = new List<string>(); //List for ingredients
                List<double> ingredientQuantity = new List<double>(); //List for ingredients
                List<string> ingredientUnits = new List<string>(); //List for ingredients
                List<int> ingredientCalories = new List<int>(); //List for ingredients
                List<int> ingredientFoodGroup = new List<int>(); //List for ingredients

                for (int i = 0; i < numIngredients; i++) //loop to iterate as many times as the user wants to enter recipes
                {
                    Console.WriteLine($"\n Ingredient nr {i + 1}:"); //takes in user input 
                    Console.Write("Name: ");
                    ingredients.Add(Console.ReadLine());
                    Console.Write("Quantity to be used: ");
                    ingredientQuantity.Add(double.Parse(Console.ReadLine()));
                    Console.Write("Unit of Measurement for ingredients: ");
                    ingredientUnits.Add(Console.ReadLine());
                    Console.Write("Enter the number of calories: ");
                    ingredientCalories.Add(int.Parse(Console.ReadLine()));

                    Console.WriteLine("Choose a food group for the ingredient:"); //displays options for the food group which will later be used in the case statements
                    Console.WriteLine("1. Starchy foods");
                    Console.WriteLine("2. Vegetables and fruits");
                    Console.WriteLine("3. Dry beans, peas, lentils, and soya");
                    Console.WriteLine("4. Chicken, fish, meat, and eggs");
                    Console.WriteLine("5. Milk and dairy products");
                    Console.WriteLine("6. Fats and oil");
                    Console.WriteLine("7. Water");
                    Console.Write("Enter your choice (1-7): ");
                    int foodGroupChoice = int.Parse(Console.ReadLine()); //takes in food group choice.
                    ingredientFoodGroup.Add(foodGroupChoice);
                }

                Console.Write("\nEnter the number of steps: ");
                int nrSteps = int.Parse(Console.ReadLine());

                List<string> steps = new List<string>(); //populates list with the steps of the recipe

                for (int i = 0; i < nrSteps; i++)
                {
                    Console.WriteLine($"\nStep {i + 1}:");
                    Console.Write("Description: ");
                    steps.Add(Console.ReadLine());
                }

                // Create and add recipe object to recipes List
                Recipe newRecipe = new Recipe(recipeName, ingredients, ingredientQuantity, ingredientUnits, ingredientCalories, ingredientFoodGroup, steps);
                recipes.Add(newRecipe); 

                Console.Write("\nDo you want to scale the ingredient quantities? y- For yes, n - for No: "); //prompts user for scaling 
                string scaleResponse = Console.ReadLine().ToLower();
                if (scaleResponse == "y")
                {
                    ScalingFunction scaling = new ScalingFunction(ingredientQuantity); //prompts user for scaling options aswell as sending to scaling function method
                    Console.Write("Enter the scale factor (0.5 for half, 2 for double, 3 for triple): ");
                    double scaleFactor = double.Parse(Console.ReadLine());
                    scaling.ScaleIngredients(scaleFactor);
                    Console.WriteLine("\nScaled Recipe Details:");
                    scaling.DisplayIngredients(ingredients, ingredientUnits);
                }

                Console.Write("\nDo you want to enter a new recipe? y- For yes, n - for No: "); //asks user if they want to restart the recipe process or not 
                string continueResponse = Console.ReadLine().ToLower();
                if (continueResponse != "y")
                {
                    continueProgram = false;
                    Console.WriteLine("\nYour recipes have now been saved and compiled!");
                }
            }

            // Display recipes in different colors
            Console.WriteLine("\nRecipes in different colors:");
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Magenta };

            int colorIndex = 0;
            foreach (Recipe recipe in recipes)
            {
                Console.ForegroundColor = colors[colorIndex % colors.Length];
                recipe.DisplayRecipe();
                recipe.DisplayTotalCalories();
                colorIndex++;
            }

            // Reset console color
            Console.ResetColor();
        }

        static void HandleCaloriesExceeded(string recipeName, int totalCalories) //handles the delegate whether the calories exceed 300 (Japikse,2022)
        {
            Console.WriteLine($"Warning: The recipe '{recipeName}' exceeds 300 calories with a total of {totalCalories} calories.");
        }
    }

    class Recipe : IComparable
    {
        private string recipeName;
        private List<string> ingredientNames;
        private List<double> ingredientQuantities;
        private List<string> ingredientUnits;
        private List<int> ingredientCalories;
        private List<int> ingredientFoodGroup;
        private List<string> steps;

        // Delegate instance for alerting the user (Japikse,2022)
        public static event CaloriesAlert CaloriesExceeded;

        public Recipe(string recipeName, List<string> ingredientNames, List<double> ingredientQuantities, List<string> ingredientUnits, List<int> ingredientCalories, List<int> ingredientFoodGroup, List<string> steps)
        {
            this.recipeName = recipeName;
            this.ingredientNames = ingredientNames;
            this.ingredientQuantities = ingredientQuantities;
            this.ingredientUnits = ingredientUnits;
            this.ingredientCalories = ingredientCalories;
            this.ingredientFoodGroup = ingredientFoodGroup;
            this.steps = steps;
        }

        public void DisplayRecipe() //method for displaying the recipe for the program (HitSubscribe, 2024)
        {
            Console.WriteLine($"\nRecipe Name: {recipeName}");
            Console.WriteLine("\nIngredients:");
            for (int i = 0; i < ingredientNames.Count; i++)
            {
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
                Console.WriteLine($"- Calories: {ingredientCalories[i]}");
                Console.WriteLine($"- Food Group: {GetFoodGroupName(ingredientFoodGroup[i])}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }

        public void DisplayTotalCalories() //method to calculate and display the total calories (Japikse,2022) (HitSubscribe, 2024)
        {
            int totalCalories = 0;
            for (int i = 0; i < ingredientCalories.Count; i++)
            {
                totalCalories += ingredientCalories[i];
            }
            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            // Check if total calories exceed 300 and raise event if so
            if (totalCalories > 300)
            {
                CaloriesExceeded?.Invoke(recipeName, totalCalories);
            }
        }

        private string GetFoodGroupName(int groupNumber) //method for handling the case of food groups (Japikse,2022)
        {
            switch (groupNumber)
            {
                case 1:
                    return "Starchy foods";
                case 2:
                    return "Vegetables and fruits";
                case 3:
                    return "Dry beans, peas, lentils, and soya";
                case 4:
                    return "Chicken, fish, meat, and eggs";
                case 5:
                    return "Milk and dairy products";
                case 6:
                    return "Fats and oil";
                case 7:
                    return "Water";
                default:
                    return "Unknown";
            }
        }

        public int CompareTo(object obj)
        {
            Recipe otherRecipe = obj as Recipe;
            return this.recipeName.CompareTo(otherRecipe.recipeName);
        }
    }
}

//Pro C# 10 with .NET 6, Troelsen, Andrew; Japikse, phil. 11th edition,Chambersburg,PA. 2022, apress
//S, J., 2010. Stackoverflow. [Online] 
//Available at: https://stackoverflow.com/questions/2675196/c-sharp-method-to-scale-values
//[Accessed 11 April 2024].
//Stackify, HitSubscribe, 2024. [Online] Available: https://stackify.com/c-delegates-definition-types-examples/
