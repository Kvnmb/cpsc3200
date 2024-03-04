// Kevin Bui
// October 5, 2023
// Last Revised: November 7, 2023
// Formula.h
// IDE: Visual Studio Code


// Class Invariants: Resource arrays are instantiated after Formula constructor is run and input, output,
// and max level will stay the same throughout object lifetime

// Resource variables holds the data while Formula class manages operations

// Percentages and currentLevel (RaiseLevel()) are subject to change when ExpectedResult() and BonusResult() are called

// Deep copy and move semantics supported 

// Resource struct is a public struct that client is able to use to instantiate Formulas

// Must use functions to acquire inputSize and outputSize for traversing arrays
#ifndef FORMULA_H
#define FORMULA_H
#include <iostream>
using namespace std;


struct Resource {
    string name;
    unsigned int quantity;

    Resource();
    Resource(string nameInit, int quantityInit);
};

class Formula {
    private:
        unsigned int currentLevel = 0;
        double failPercentage = 0.2;
        double reducedPercentage = 0.45;
        double expectedPercentage = 0.3;
        double bonusPercentage = 0.05;
        const unsigned int maxLevel = 5;

        const double proficiencyIncrement = 0.05;
        const double reduceQuantity = 0.45;
        const double bonusQuantity = 1.5;

        Resource* inputs;
        Resource* outputs;
        unsigned int inputSize;
        unsigned int outputSize;

        // Precondition: None
        bool chanceFailed(double chance);

        // Postcondition: None

        // Precondition: None
        bool chanceReduced(double chance);
        // Postcondition: None

        // Precondition: None
        bool chanceExpected(double chance);
        // Postcondition: None

        // Precondition: Resource* array has been declared
        Resource* failedProcess();
        // Postcondition: Resource* array will be returned

        // Precondition: Resource* array has been declared
        Resource* reducedOutput();
        // Postcondition: Resource* array will be returned

        // Precondition: Resource* array has been declared
        Resource* expectedResult();
        // Postcondition: Resource* array will be returned

        // Precondition: Resource* array has been declared
        Resource* bonusResult();
        // Postcondition: Resource* array will be returned

        // Precondition: called by Apply() helper functions
        void raiseLevel();
        // Postcondition: Percentages and level may be altered

        // Precondition: none
        void copy(const Formula& src);
        // Postcondition: overloaded assignment operator copy

        // Precondition: none
        void swap(Formula& lhs, Formula& rhs);
        // Postcondition: Formula objects swap values
    
    public:
        // Precondition: none
        Formula();
        // Postcondition: empty object

        // Precondition: none
        ~Formula();
        // Postcondition: deallocated memory

        // Precondition: valid Formula object passed
        Formula(const Formula& src);
        // Postcondition: deep copying of object

        // Precondition: valid Formula object passed
        Formula& operator=(const Formula& src);
        // Postcondition: deep copy semantics employed

        // Precondition: src is instantiated
        Formula(Formula&& src);
        // Postcondition: move semantics applied

        // Precondition: src is instantiated
        Formula& operator=(Formula&& src);
        // Postcondition: move semantics applied

        // Precondition: client has passed two nonempty Resource arrays
        Formula(Resource inputInit[], unsigned int numInputs, Resource outputInit[], unsigned int numOutputs);        
        // Postcondition: object has been instantiated


        // Precondition: object has been instantiated 
        Resource* apply();
        // Postcondition: currentLevel may or may not have been incremented, percentages may or may not have been incremented

        // Precondition: Resource* array has been declared
        Resource* getInput() const;
        // Postcondition: Resource* array will be returned

        // Precondition: Resource* array has been declared
        Resource* getOutput() const;
        // Postcondition: Resource* array will be returned

        // Precondition: Object instantiated
        unsigned int getLevel() const;
        // Postcondition: value 0 or greater returned

        // Precondition: Object instantiated
        unsigned int getInputSize() const;
        // Postcondition: value 0 or greater returned

        // Precondition: Object instantiated
        unsigned int getOutputSize() const;
        // Postcondition: value 0 or greater returned

        // Precondition: src is instantiated
        bool operator==(const Formula& src) const;
        // Postcondition: returns boolean comparison of objects

        // Precondition: src is instantiated
        bool operator!=(const Formula& src) const;
        // Postcondition: returns boolean comparison of objects
    };

#endif

// Implementation Invariants: The client will pass in arrays of input and output resources as well as
// the array sizes

// Input, output, and apply functions return a pointer to array, client responsible for deallocation

// The sum of the output percentages must always be 1.0

// input and output functions return a Resource array

// Apply() function uses random double generation

// Formula object holds instantiated Resource for lifetime

// Resource quantities are nonnegative

// Input and Output queries as well as Apply() can be called infinite times

// input and output functions return copies of the real arrays