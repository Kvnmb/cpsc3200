// Kevin Bui
// November 4, 2023
// Last Revised: November 7, 2023
// ExecutablePlan.h
// IDE: Visual Studio Code

#ifndef EXECUTABLEPLAN_H
#define EXECUTABLEPLAN_H
#include <iostream>
#include <memory>
#include "Formula.h"
#include "Plan.h"
#include "Stockpile.h"
using namespace std;


// Class Invariants:

// base class methods are set to virtual for child class methods, dynamic binding

// currentStep starts at 1 for instantiation, all Formulas in array with element less
// than value currentStep have been completed

// A Formula being completed means they have called Apply() once

// Dependency injection of ExecutablePlan is calling the constructor injection in Parent Plan for instantiation

// Apply() must be called in order to advance currentStep, afterwards any Formulas previously applied can be called

// Replace(), Remove(), and Apply() are dependent on value of currentStep

// Plan can start out valid in empty state

// Overloaded apply function used with unique smart pointer, move semantics

// Overloaded operators add functionality

// Operator overloading of +, +=, and --


class ExecutablePlan : public Plan
{
    private:
        unsigned int currentStep;

    public:
        // Precondition: none
        ExecutablePlan();
        // Postcondition: default Plan constructor called, currentStep initiated

        // Precondition: size is equal to length of insert, insert instantiated 
        ExecutablePlan(Formula* insert, unsigned int size);
        // Postcondition: ExecutablePlan object initiated through constructor injection

        // Precondition: none
        unsigned int queryStep();
        // Postcondition: none

        // Precondition: formulaNum is less than currentStep
        Resource* apply(unsigned int formulaNum) override;
        // Postcondition: currentStep may be increased, Formula object may call apply() if valid

        // Precondition: Formula in the array has not been completed, replaceFormula is instantiated
        void replace(unsigned int replaceNum, const Formula& replaceFormula) override;
        // Postcondition: if valid, will replace Formula in the array, else will do nothing

        // Precondition: the current sequence of Formulas has not been completed
        void remove() override;
        // Postcondition: will do nothing if sequence is complete, else will remove last Formula

        // Precondition: src is instantiated
        ExecutablePlan& operator=(const ExecutablePlan& src);
        // Postcondition: object is deep copied using assignment operator

        // Precondition: src is instantiated
        ExecutablePlan(const ExecutablePlan& src);
        // Precondition: object instantiated through copy constructor

        // Precondition: src is instantiated
        ExecutablePlan(ExecutablePlan&& src);
        // Postcondition: move semantics applied

        // Precondition: src is instantiated
        ExecutablePlan& operator=(ExecutablePlan&& src);
        // Postcondition: move semantics applied

        // Precondition: src is instantiated
        bool operator==(const ExecutablePlan& src) const;
        // Postcondition: boolean comparison of objects returned

        // Precondition: src is instantiated
        bool operator!=(const ExecutablePlan& src) const;
        // Postcondition: boolean comparison of objects returned
        
        // Precondition: Stockpile ptr is instantiated
        unique_ptr<Stockpile> apply(unique_ptr<Stockpile> ptr);
        // Postcondition: Stockpile ptr is either unaffected or a new smart pointer containing result returned

        // Precondition: other is instantiated
        ExecutablePlan operator+(const ExecutablePlan& other);
        // Postcondition: formulas added to object

        // Precondition: other is instantiated
        ExecutablePlan& operator+=(const ExecutablePlan& other);
        // Postcondition: formulas added to object

        // Precondition: Plan is nonempty, last formula is not completed
        ExecutablePlan& operator--();
        // Postcondition: numFormulas decremented
};


#endif


// Implementation Invariants:

// virtual methods allow child class to add preconditions before calling parent function

// currentStep will never decrease, only stay the same or increase

// Formula objects that are in position of array greater than or equal to currentStep cannot be modified,
// array must be applied in order

// bounds checking to ensure that function calls will never be beyond currentStep

// When the entire array is complete, remove will do nothing

// incrementing currentStep is only done by calling a successful Apply() function

// unique_ptr apply function will use move semantics to transfer ownership

// overloaded + and += operators call the add function to add the formulas of plan object together

// operator-- calls overloaded remove with simpler syntax