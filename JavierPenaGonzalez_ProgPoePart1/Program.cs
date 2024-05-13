// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;
//namescape for what we are going to use in every class
namespace JavierPenaGonzalez_ProgPoePart1
{
    // Define a delegate type
    public delegate void CaloriesAlert(string recipeName, int totalCalories);

    class Program
    {
        static void Main(string[] args)
        {
            ArrayList recipes = new ArrayList(); // ArrayList to store recipes

            Recipe.CaloriesExceeded += HandleCaloriesExceeded;

            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine("Welcome to the recipe app, we will now take in your recipe");

                Console.Write("Enter recipe name: ");
                string recipeName = Console.ReadLine();

                Console.Write("Enter the number of ingredients you would like to have in this recipe: ");
                int numIngredients = int.Parse(Console.ReadLine());

                ArrayList ingredients = new ArrayList();
                ArrayList ingredientQuantity = new ArrayList();
                ArrayList ingredientUnits = new ArrayList();
                ArrayList ingredientCalories = new ArrayList();
                ArrayList ingredientFoodGroup = new ArrayList();

                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine($"\n Ingredient nr {i + 1}:");
                    Console.Write("Name: ");
                    ingredients.Add(Console.ReadLine());
                    Console.Write("Quantity to be used: ");
                    ingredientQuantity.Add(double.Parse(Console.ReadLine()));
                    Console.Write("Unit of Measurement for ingredients: ");
                    ingredientUnits.Add(Console.ReadLine());
                    Console.Write("Enter the number of calories: ");
                    ingredientCalories.Add(int.Parse(Console.ReadLine()));

                    Console.WriteLine("Choose a food group for the ingredient:");
                    Console.WriteLine("1. Starchy foods");
                    Console.WriteLine("2. Vegetables and fruits");
                    Console.WriteLine("3. Dry beans, peas, lentils, and soya");
                    Console.WriteLine("4. Chicken, fish, meat, and eggs");
                    Console.WriteLine("5. Milk and dairy products");
                    Console.WriteLine("6. Fats and oil");
                    Console.WriteLine("7. Water");
                    Console.Write("Enter your choice (1-7): ");
                    int foodGroupChoice = int.Parse(Console.ReadLine());
                    ingredientFoodGroup.Add(foodGroupChoice);
                }

                Console.Write("\nEnter the number of steps: ");
                int nrSteps = int.Parse(Console.ReadLine());

                ArrayList steps = new ArrayList();

                for (int i = 0; i < nrSteps; i++)
                {
                    Console.WriteLine($"\nStep {i + 1}:");
                    Console.Write("Description: ");
                    steps.Add(Console.ReadLine());
                }

                // Create and add recipe object to recipes ArrayList
                Recipe newRecipe = new Recipe(recipeName, ingredients, ingredientQuantity, ingredientUnits, ingredientCalories, ingredientFoodGroup, steps);
                recipes.Add(newRecipe);

                Console.Write("\nDo you want to scale the ingredient quantities? y- For yes, n - for No: ");
                string scaleResponse = Console.ReadLine().ToLower();
                if (scaleResponse == "y")
                {
                    scalingFunction scaling = new scalingFunction(ingredientQuantity);
                    Console.Write("Enter the scale factor (0.5 for half, 2 for double, 3 for triple): ");
                    double scaleFactor = double.Parse(Console.ReadLine());
                    scaling.ScaleIngredients(scaleFactor);
                    Console.WriteLine("\nScaled Recipe Details:");
                    scaling.DisplayIngredients(ingredients, ingredientUnits);
                }

                Console.Write("\nDo you want to clear the stored data and enter a new recipe? y- For yes, n - for No: ");
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
        static void HandleCaloriesExceeded(string recipeName, int totalCalories)
        {
            Console.WriteLine($"Warning: The recipe '{recipeName}' exceeds 300 calories with a total of {totalCalories} calories.");
        }
    }

    class Recipe : IComparable
    {
        private string recipeName;
        private ArrayList ingredientNames;
        private ArrayList ingredientQuantities;
        private ArrayList ingredientUnits;
        private ArrayList ingredientCalories;
        private ArrayList ingredientFoodGroup;
        private ArrayList steps;

        // Delegate instance for alerting the user
        public static event CaloriesAlert CaloriesExceeded;

        public Recipe(string recipeName, ArrayList ingredientNames, ArrayList ingredientQuantities, ArrayList ingredientUnits, ArrayList ingredientCalories, ArrayList ingredientFoodGroup, ArrayList steps)
        {
            this.recipeName = recipeName;
            this.ingredientNames = ingredientNames;
            this.ingredientQuantities = ingredientQuantities;
            this.ingredientUnits = ingredientUnits;
            this.ingredientCalories = ingredientCalories;
            this.ingredientFoodGroup = ingredientFoodGroup;
            this.steps = steps;
        }

        public void DisplayRecipe()
        {
            Console.WriteLine($"\nRecipe Name: {recipeName}");
            Console.WriteLine("\nIngredients:");
            for (int i = 0; i < ingredientNames.Count; i++)
            {
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
                Console.WriteLine($"- Calories: {ingredientCalories[i]}");
                Console.WriteLine($"- Food Group: {GetFoodGroupName((int)ingredientFoodGroup[i])}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }

        public void DisplayTotalCalories()
        {
            int totalCalories = 0;
            for (int i = 0; i < ingredientCalories.Count; i++)
            {
                totalCalories += (int)ingredientCalories[i];
            }
            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            // Check if total calories exceed 300 and raise event if so
            if (totalCalories > 300)
            {
                CaloriesExceeded?.Invoke(recipeName, totalCalories);
            }
        }

        private string GetFoodGroupName(int groupNumber)
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

    class scalingFunction //class that will perform the scaling function
    {
        private ArrayList originalQuantities;
        private ArrayList scaledQuantities;

        public scalingFunction(ArrayList originalQuantities)
        {
            this.originalQuantities = originalQuantities;
            this.scaledQuantities = new ArrayList();
            this.scaledQuantities.AddRange(originalQuantities); //creates a copy of the array of stored ingredients
        }

        public void ScaleIngredients(double scaleFactor)
        {
            for (int i = 0; i < originalQuantities.Count; i++)
            {
                double quantity = (double)originalQuantities[i];
                scaledQuantities[i] = quantity * scaleFactor;
            }
        }
        public void RevertScaling()
        {
            scaledQuantities.Clear();
            scaledQuantities.AddRange(originalQuantities); //takes the array copy
        }

        public void DisplayIngredients(ArrayList ingredientNames, ArrayList ingredientUnits)
        {
            for (int i = 0; i < scaledQuantities.Count; i++)
            {
                Console.WriteLine($"{scaledQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }
        }
    }
}

//Pro C# 10 with .NET 6, Troelsen, Andrew; Japikse, phil. 11th edition,Chambersburg,PA. 2022, apress
//S, J., 2010. Stackoverflow. [Online] 
//Available at: https://stackoverflow.com/questions/2675196/c-sharp-method-to-scale-values
//[Accessed 11 April 2024].
