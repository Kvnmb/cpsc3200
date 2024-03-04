// Kevin Bui
// November 5, 2023
// Last Revised: November 7, 2023
// Stockpile.cpp
// IDE: Visual Studio Code
// Implementation file for Stockpile.h

#include <iostream>
#include <memory>
#include <unordered_map>
#include "ExecutablePlan.h"
#include "Plan.h"
#include "Formula.h"
#include "Stockpile.h"


Stockpile::Stockpile()
{}

Stockpile::Stockpile(const Resource* insert, unsigned int size)
{
    for (unsigned int x = 0; x < size; x++){
        if (map.find(insert[x].name) == map.end()){
            map[insert[x].name] = insert[x].quantity;
        }else{
            map[insert[x].name] += insert[x].quantity;
        }
    }
}

void Stockpile::increase(const string& name, const unsigned int increment = 0)
{
    if (map.find(name) == map.end()) map[name] = increment;

    else map[name] += increment;
}

void Stockpile::decrease(const string& name, const unsigned int decrement = 0)
{
    if (map.find(name) == map.end()) return;

    if(map[name] <= decrement) map[name] = 0;
    else map[name] -= decrement;
}

unsigned int Stockpile::getQuantity(const string& name) const
{
    if (map.find(name) == map.end()) return 0;
    return map.at(name);
}

void Stockpile::printStockpile()
{
    unordered_map<string, unsigned int>::iterator itr;
    for (itr = map.begin(); itr != map.end(); itr++){
        cout << "\n\n" << itr->first << " " << itr->second << "\n";
    }
}


// Implementation Invariants:

// STL functions used to search through map for keys

// increase will either add onto a key value or create a new key

// decrease will decrease quantity of specific resource or return if not found

// getQuantity returns the value stored 

// decrease will never make the value go below 0

// print function prints all keys with corresponding values using an iterator

// getQuantity will return 0 if either quantity = 0 or key is not in map
