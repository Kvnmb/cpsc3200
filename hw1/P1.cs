// Kevin Bui
// September 25, 2023
// Last Revised: September 27, 2023
// P1.cs
// IDE: Visual Studio
// Overview: this program creates an array of Formula objects and initializes them, then prints out the inputs
// and outputs of each object. Then, the program calls the Apply() function to simulate outcomes of the Formulas, 
// and is called once more to test Proficiency level increases. Results will vary.

using System;
using FormulaClass;

public class P1 {
    static void Main(string [] args){
        int formulaCount = 5;
        Formula [] list = InitFormulas(formulaCount);

        PrintInputsAndOutPuts(list);

        ApplyFormulas(list);
        
        // run function twice to attempt proficiency level increase
        ApplyFormulas(list);

        CheckLevels(list);
    }

    // initializing a seemingly random distribution of Formulas with a specified number of objects
    static Formula [] InitFormulas(int count)
    {
        Formula [] list = new Formula[count];

        for(uint x = 0; x < list.Length; x++){
            list[x] = new Formula(x);
        }

        return list;
    }

    // Prints out the proficiency levels of each Formula object
    static void CheckLevels(Formula [] list)
    {
        for(int x = 0; x < list.Length; x++){
            Console.WriteLine("The proficiency level of formula " + (x + 1) + " is:" + list[x].GetLevel());
        }
            
    }

    // prints all the inputs and outputs of the Formulas in the list, get functions return string arrays of unknown length
    static void PrintInputsAndOutPuts(Formula [] list){
        
        for(int x = 0; x < list.Length; x++){
            Console.WriteLine("The inputs of formula " + (x + 1) + " are:");
            string [] input = list[x].GetInput();

            for(int y = 0; y < input.Length; y++){
                Console.WriteLine(input[y]);
            }
            Console.WriteLine("The outputs of formula " + (x + 1) + " are:");
            string [] output = list[x].GetOutput();

            for(int z = 0; z < output.Length; z++){
                Console.WriteLine(output[z]);
            }
            Console.WriteLine();
        }
    }

    // Function to simulate outcomes with all Formula objects in the list
    static void ApplyFormulas(Formula [] list)
    {
        for(int x = 0; x < list.Length; x++){
            string [] output = list[x].Apply();

            for(int y = 0; y < output.Length; y++){
                Console.WriteLine(output[y]);
            }
            Console.WriteLine();
        }
    }
}