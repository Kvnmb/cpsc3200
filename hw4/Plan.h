// Kevin Bui
// October 5, 2023
// Last Revised: November 7, 2023
// Plan.h
// IDE: Visual Studio Code


// Class Invariants: Plan object can start out empty and in valid state or be initialized 
// with copy constructor

// Plan Object stores the number of Formulas it has in a dynamic array that resizes when capacity is reached

// only add() function can call resize()

// Deep copy semantics as well as move semantics supported

// Plan uses heap memory to create array of Formula objects

// Client passes in Formula objects to add or replace in Plan object

// Client must refer to the number of Formulas as 1 to x, not 0 to x when passing in parameters

// Dependency injection: client passes in an array of Formula objects to instantiate Plan's Formula array

// Exception handling added for functions when parameters are invalid

// Operator overloading of +, +=, and --


#ifndef PLAN_H
#define PLAN_H
#include <iostream>
#include "Formula.h"
using namespace std;

class Plan {
    private:
        const unsigned int increase = 2;

        // Precondition: numFormulas = maxSize, add() is called
        void resize();
        // Postcondition: Formula array is copied into an array double the maxSize

        // Precondition: none
        void copy(const Plan& src);
        // Postcondition: src has been copied into the object

        // Precondition: none
        void swap(Plan &lhs, Plan &rhs);
        // Postcondition: the Plan objects swap their values

        // Precondition: called by public functions
        bool formulaNumNotInRange(unsigned int formulaNum) const;
        // Postcondition: returns validity check

    protected:
        Formula* formulaArr;
        unsigned int numFormulas;
        unsigned int maxSize = 10;

    public:

        // Precondition: none
        Plan();
        // Postcondition: empty object

        // Precondition: none
        virtual ~Plan();
        // Postcondition: deallocated memory

        // Precondition: client passes in array of formula objects for dependency injection and size of array
        // size must be correct length of array
        Plan(Formula* array, unsigned int size);
        // Postcondition: Plan object is instantiated and nonempty

        // Precondition: client has instantiated a Formula object to pass into function
        void add(const Formula& newFormula);
        // Postcondition: Formula object is passed into Plan's Formula array

        // Precondition: Formula array is not empty
        virtual void remove();
        // Postcondition: numFormulas decremented, or nothing happens if empty

        // Precondition: number is between 1 and numFormulas, Formula object instantiated
        virtual void replace(unsigned int replaceNum, const Formula& replaceFormula);
        // Postcondition: Array object replaced with parameter replaceFormula

        // Precondition: none
        unsigned int getNumFormulas() const;
        // Postcondition: none, returns numFormulas

        // Precondition: number is between 1 and numFormulas
        int getLevel(unsigned int formulaNum) const;
        // Postcondition: no effect to state, returns level of Formula object

        // Precondition: Formula array is non-empty
        virtual Resource* apply(unsigned int formulaNum);
        // Postcondition: apply is called for specified object in array

        // Precondition: src is instantiated
        Plan(const Plan& src);
        // Precondition: object instantiated through copy constructor

        // Precondition: src is instantiated
        Plan& operator=(const Plan& src);
        // Postcondition: object is deep copied using assignment operator

        // Precondition: src is instantiated
        Plan(Plan&& src);
        // Postcondition: move semantics applied

        // Precondition: none
        Plan& operator=(Plan&& src);
        // Postcondition: move semantics applied
        
        // Precondition: formulaNum is between 1 and numFormulas
        Resource* getInput(unsigned int formulaNum) const;
        // Postcondition: returns the inputs of the Formula object

        // Precondition: formulaNum is between 1 and numFormulas
        Resource* getOutput(unsigned int formulaNum) const;
        // Postcondition: returns the outputs of the Formula object

        // Precondition: number is between 1 and numFormulas
        int getInputSize(unsigned int formulaNum) const;
        // Postcondition: returns the number of inputs of Formula object

        // Precondition: number is between 1 and numFormulas
        int getOutputSize(unsigned int formulaNum) const;
        // Postcondition: returns the number of outputs of Formula object

        // Precondition: src is instantiated
        bool operator==(const Plan& src) const;
        // Postcondition: boolean comparison of objects returned

        // Precondition: src is instantiated
        bool operator!=(const Plan& src) const;
        // Postcondition: boolean comparison of objects returned

        // Precondition: other is instantiated
        Plan operator+(const Plan& other);
        // Postcondition: formulas added to object

        // Precondition: other is instantiated
        Plan& operator+=(const Plan& other);
        // Postcondition: formulas added to object

        // Precondition: Plan is nonempty
        Plan& operator--();
        // Postcondition: numFormulas decremented
};

#endif

// Implementation Invariants: The client will pass in Formula objects to instantiate array, done
// through add function or constructors

// numFormulas will always be equal to or less than maxSize, when array is full the next call to add() will
// allocate a new array

// apply function runs the Formula apply() function on each object in the array in order

// Class stays in valid states of empty array or non-empty

// Client must pass in positive integers only

// remove() function does not delete or modify array, only decrements the numFormulas variable

// input and output functions return copies of the real arrays

// functions will return values of specified object in array

// resize() function doubles the maxSize of the array

// overloaded + and += operators call the add function to add the formulas of plan object together, ease of access

// operator-- calls remove with simpler syntax