// Kevin Bui
// September 25, 2023
// Last Revised: September 27, 2023
// Formula.cs
// IDE: Visual Studio


// Class Invariants: Resource struct is instantiated after Formula constructor is run and input, output,
// and max level will stay the same throughout object lifetime

// Resource struct holds the data while Formula class manages operations

// Percentages in resource and currentLevel (RaiseLevel()) are subject to change when ExpectedResult() and BonusResult() are called

// No copy semantics

// The class is designed so client has minimal influence on data fields

// Formula will function as long as negative number is not passed into constructor

namespace FormulaClass {

    public class Formula
    {
        private struct Resource {
            public readonly string [] inputNames;
            public readonly string [] outputNames;
            public readonly uint [] inputQuantities;
            public readonly uint [] outputQuantities;
            public double failPercentage;
            public double reducedPercentage;
            public double expectedPercentage;
            public double bonusPercentage;
            public readonly uint maxLevel;

            public Resource(uint recipeNum)
            {
                switch(recipeNum)
                {
                    case ironBarFormula:
                        inputNames = new string[1];
                        inputNames[0] = "iron ore";

                        outputNames = new string[1];
                        outputNames[0] = "iron bar";

                        inputQuantities = new uint[1];
                        inputQuantities[0] = 10;

                        outputQuantities = new uint[1];
                        outputQuantities[0] = 5;

                        failPercentage = 0.10;
                        reducedPercentage = 0.25;
                        expectedPercentage = 0.60;
                        bonusPercentage = 0.05;

                        maxLevel = 5;
                        
                    break;

                    case brassBarFormula:
                        inputNames = new string[2];
                        inputNames[0] = "copper ore";
                        inputNames[1] = "zinc ore";

                        outputNames = new string[1];
                        outputNames[0] = "brass bar";

                        inputQuantities = new uint[2];
                        inputQuantities[0] = 3;
                        inputQuantities[1] = 9;

                        outputQuantities = new uint[1];
                        outputQuantities[0] = 3;

                        failPercentage = 0.25;
                        reducedPercentage = 0.25;
                        expectedPercentage = 0.4;
                        bonusPercentage = 0.1;

                        maxLevel = 4;

                    break;

                    case steelBarFormula:
                        inputNames = new string[2];
                        inputNames[0] = "iron ore";
                        inputNames[1] = "coal";

                        outputNames = new string[3];
                        outputNames[0] = "steel bar";
                        outputNames[1] = "stainless steel bar";
                        outputNames[2] = "pig iron bar";

                        inputQuantities = new uint[2];
                        inputQuantities[0] = 15;
                        inputQuantities[1] = 5;

                        outputQuantities = new uint[3];
                        outputQuantities[0] = 2;
                        outputQuantities[1] = 1;
                        outputQuantities[2] = 2;

                        failPercentage = 0.0;
                        reducedPercentage = 0.50;
                        expectedPercentage = 0.45;
                        bonusPercentage = 0.05;

                        maxLevel = 3;

                    break;

                    case diamondFormula:
                        inputNames = new string[1];
                        inputNames[0] = "coal";

                        outputNames = new string[1];
                        outputNames[0] = "diamond";

                        inputQuantities = new uint[1];
                        inputQuantities[0] = 30;

                        outputQuantities = new uint[1];
                        outputQuantities[0] = 1;

                        failPercentage = 0.9;
                        reducedPercentage = 0.05;
                        expectedPercentage = 0.05;
                        bonusPercentage = 0;

                        maxLevel = 2;

                    break;

                    default:
                        inputNames = new string[1];
                        inputNames[0] = "iron ore";

                        outputNames = new string[1];
                        outputNames[0] = "iron bar";

                        inputQuantities = new uint[1];
                        inputQuantities[0] = 2;

                        outputQuantities = new uint[1];
                        outputQuantities[0] = 1;

                        failPercentage = 0.10;
                        reducedPercentage = 0.25;
                        expectedPercentage = 0.60;
                        bonusPercentage = 0.05;

                        maxLevel = 5;

                    break;
                }
            }
        }
        private Resource formulaPlan;
        private uint currentLevel = 0;
        private const uint ironBarFormula = 0;
        private const uint brassBarFormula = 1;
        private const uint steelBarFormula = 2;
        private const uint diamondFormula = 3;
        private const uint totalFormulas = 4;
        private const double proficiencyIncrement = 0.05;
        private const double reduceQuantity = 0.45;
        private const double bonusQuantity = 1.5;

        // Precondition: client has passed a nonnegative number
        public Formula(uint formulaNum = ironBarFormula)
        {
            formulaPlan = new Resource(formulaNum % totalFormulas);
        }
        // Postcondition: Resource struct in object has been instantiated


        // Precondition: Resource has been instantiated 
        public string [] Apply()
        {
            Random rnd = new Random();
            double chance = rnd.NextDouble();

            if(ChanceFailed(chance))
                return FailedProcess();
            if(ChanceReduced(chance))
                return ReducedOutput();
            if(ChanceExpected(chance))
                return ExpectedResult();
            
            return BonusResult();
        }
        // Postcondition: currentLevel may or may not have been incremented, percentages may or may not have been incremented


        // Precondition: None
        bool ChanceFailed(double chance)
        {
            return chance <= formulaPlan.failPercentage;
        }

        // Postcondition: None

        // Precondition: None
        bool ChanceReduced(double chance)
        {
            return chance <= (formulaPlan.reducedPercentage + formulaPlan.failPercentage);
        }
        // Postcondition: None

        // Precondition: None
        bool ChanceExpected(double chance)
        {
            return chance <= (formulaPlan.expectedPercentage + formulaPlan.reducedPercentage + formulaPlan.failPercentage);
        }
        // Postcondition: None

        // Precondition: string array has been declared
        string [] FailedProcess()
        {
            string [] list = new string[formulaPlan.outputNames.Length];
            Console.WriteLine("The conversion has failed.");
            for(int count = 0; count < formulaPlan.outputNames.Length; count++){
                list[count] = 0 + " " + formulaPlan.outputNames[count];
            }

            return list;
        }
        // Postcondition: string array will be returned

        // Precondition: string array has been declared
        string [] ReducedOutput()
        {
            string [] list = new string[formulaPlan.outputNames.Length];
            Console.WriteLine("There was an error with the conversion and the output may have been reduced");
            for(int count = 0; count < formulaPlan.outputNames.Length; count++){
                list[count] = Math.Round(reduceQuantity * formulaPlan.outputQuantities[count]) + " " + formulaPlan.outputNames[count];
            }

            return list;
        }
        // Postcondition: string array will be returned

        // Precondition: string array has been declared
        string [] ExpectedResult()
        {
            string [] list = new string[formulaPlan.outputNames.Length];
            Console.WriteLine("The conversion was a success!");
            for(int count = 0; count < formulaPlan.outputNames.Length; count++){
                list[count] = formulaPlan.outputQuantities[count] + " " + formulaPlan.outputNames[count];
            }
            RaiseLevel();

            return list;
        }
        // Postcondition: string array will be returned

        // Precondition: string array has been declared
        string [] BonusResult()
        {
            string [] list = new string[formulaPlan.outputNames.Length];
            Console.WriteLine("There was extra output from the conversion.");

            for(int count = 0; count < formulaPlan.outputNames.Length; count++){
                list[count] = Math.Round(bonusQuantity * formulaPlan.outputQuantities[count]) + " " + formulaPlan.outputNames[count];
            }
            RaiseLevel();
            RaiseLevel();
            
            return list;
        }
        // Postcondition: string array will be returned

        // Precondition: string array has been declared
        public string [] GetInput()
        {
            string [] list = new string[formulaPlan.inputNames.Length];
            for(int count = 0; count < formulaPlan.inputNames.Length; count++){
                list[count] = formulaPlan.inputQuantities[count] + " " + formulaPlan.inputNames[count];
            }

            return list;
        }
        // Postcondition: string array will be returned

        // Precondition: string array has been declared
        public string [] GetOutput()
        {
            string [] list = new string[formulaPlan.outputNames.Length];
            for(int count = 0; count < formulaPlan.outputNames.Length; count++){
                list[count] = formulaPlan.outputQuantities[count] + " " + formulaPlan.outputNames[count];
            }

            return list;
        }
        // Postcondition: string array will be returned

        // Precondition: Object instantiated
        public uint GetLevel()
        {
            return currentLevel;
        }
        // Postcondition: value 0 or greater returned

        // Precondition: called by Apply() helper functions
        void RaiseLevel()
        {   
            if(currentLevel < formulaPlan.maxLevel){
                currentLevel++;
                Console.WriteLine("Proficiency has increased to level " + currentLevel);
                if(formulaPlan.failPercentage != 0){
                    formulaPlan.failPercentage -= proficiencyIncrement;
                    formulaPlan.expectedPercentage += proficiencyIncrement; 
                }
                if (formulaPlan.reducedPercentage != 0){
                    formulaPlan.reducedPercentage -= proficiencyIncrement;
                    formulaPlan.expectedPercentage += proficiencyIncrement;
                }
                if (formulaPlan.expectedPercentage != 0){
                    formulaPlan.expectedPercentage -= proficiencyIncrement;
                    formulaPlan.bonusPercentage += proficiencyIncrement;
                }
            }else{
                Console.WriteLine("Proficiency is at the max level.");
            }
        }  

        // Precondition: Percentages and level may be altered
    }
}

// Implementation Invariants: The value that goes into the Formula constructor will be operated on with modulo to determine which Formula is instantiated

// The sum of the output percentages must always be 1.0

// new Resource conversions must be added to switch case then increment totalFormulas by 1 and set a new const int for the formula equal to (totalFormulas - 1)

// input and output functions return an allocated string array combining quantities and names 

// Apply() function uses random double generation

// Formula object holds instantiated Resource for lifetime

// Resource quantities are nonnegative

// Input and Output queries as well as Apply() can be called infinite times