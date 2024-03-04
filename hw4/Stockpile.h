// Kevin Bui
// November 4, 2023
// Last Revised: November 8, 2023
// Stockpile.h
// IDE: Visual Studio Code

#ifndef STOCKPILE_H
#define STOCKPILE_H
#include <iostream>
#include <memory>
#include <unordered_map>
#include "ExecutablePlan.h"
#include "Plan.h"
#include "Formula.h"
using namespace std;

// Class Invariants:

// unordered map is used as the container for the sets of resources, strings and unsigned ints

// constructor injection takes in an array of resources to instantiate the object

// Resources are identified by their names as key, quantities is the value

// all resource names are unique

// Stockpile can start as empty container but must be initialized for use with increase

// increase function will increase quantities of specific resources

// decrease function will decrease quantities of specific resources to a minimum of zero



class Stockpile {
    private:
        unordered_map<string, unsigned int> map;
    public:

        // Precondition: none
        Stockpile();
        // Postcondition: empty map

        // Precondition: none
        Stockpile(const Resource* insert, unsigned int size);
        // Postcondition: map instantiated with values

        // Precondition: name may be in key
        void increase(const string& name, const unsigned int increment);
        // Postcondition: new key created or existing key incremented

        // Precondition: name may be in key
        void decrease(const string& name, const unsigned int decrement);
        // Postcondition:if found, value will be decremented to minimum of 0

        // Precondition: name may be in map
        unsigned int getQuantity(const string& name) const;
        // Postcondition: returns value stored in key or 0 if not found / empty

        // Precondition: none
        void printStockpile();
        // Postcondition: none
};

#endif

// Implementation Invariants:

// STL functions used to search through map for keys

// increase will either add onto a key value or create a new key

// decrease will decrease quantity of specific resource or return if not found

// getQuantity returns the value stored 

// decrease will never make the value go below 0

// print function prints all keys with corresponding values using an iterator

// getQuantity will return 0 if either quantity = 0 or key is not in map

