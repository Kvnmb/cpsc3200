// Kevin Bui
// October 5, 2023
// Last Revised: November 7, 2023
// Plan.cpp
// IDE: Visual Studio Code
// Implementation file for Plan.h

#include <iostream>
#include "Plan.h"

using namespace std;

Plan::Plan()
{
    numFormulas = 0;
    formulaArr = new Formula[maxSize];
}

Plan::~Plan()
{ 
    delete [] formulaArr;
}


Plan::Plan(Formula* array, unsigned int size)
{
    try{
        if(!array) throw invalid_argument("Inputted array is null.");

        numFormulas = 0;
        formulaArr = new Formula[maxSize];
        for(unsigned int x = 0; x < size; x++){
            add(array[x]);
        }
    }

    catch(exception& ex){
        cout << "Exception: " << ex.what();
        numFormulas = 0;
        formulaArr = new Formula[maxSize];
    }
}

void Plan::copy(const Plan& src)
{
    maxSize = src.maxSize;
    numFormulas= src.numFormulas;
    formulaArr = new Formula[maxSize];
    for(unsigned int x = 0; x < numFormulas; x++){
        formulaArr[x] = src.formulaArr[x];
    }
}
Plan::Plan(const Plan& src)
{ 
    copy(src);
}

Plan& Plan::operator=(const Plan& src)
{ 
    if (this == &src) return *this; // guard clause
    delete[] formulaArr;
    copy(src);
    return *this;
}

Plan::Plan(Plan&& src)
{
    swap(*this, src);

    src.maxSize = 0;
    src.numFormulas = 0;
    src.formulaArr = nullptr;
}

void Plan::swap(Plan &lhs, Plan &rhs)
{
    Plan temp;
    temp.formulaArr = lhs.formulaArr;
    temp.maxSize = lhs.maxSize;
    temp.numFormulas = lhs.numFormulas;

    lhs.formulaArr = rhs.formulaArr;
    lhs.maxSize = rhs.maxSize;
    lhs.numFormulas = rhs.numFormulas;

    rhs.formulaArr = temp.formulaArr;
    rhs.maxSize = temp.maxSize;
    rhs.numFormulas = temp.numFormulas;
}

Plan& Plan::operator=(Plan&& src)
{
    swap(*this, src);
    return *this;
}


void Plan::add(const Formula& newFormula)
{
    if(numFormulas == maxSize){
        resize();
    }
    formulaArr[numFormulas] = newFormula;
    numFormulas++;

}

void Plan::resize()
{
    maxSize = maxSize * increase;
    Formula* newArray = new Formula[maxSize];
    for(unsigned int x = 0; x < numFormulas; x++){
        newArray[x] = formulaArr[x];
    }
    delete [] formulaArr;
    formulaArr = newArray;
    newArray = nullptr;
}

void Plan::remove()
{
    try {
        if(numFormulas == 0) throw invalid_argument("No Formulas in the array.");
        numFormulas--;
    }
    catch(exception& ex){
        cout << "Exception: " << ex.what();
    }
}

void Plan::replace(unsigned int replaceNum, const Formula& replaceFormula)
{
    try {
        if(formulaNumNotInRange(replaceNum)) throw out_of_range("Number inputted is not within range.");

        formulaArr[replaceNum - 1] = replaceFormula;
    }

    catch(exception& ex){
        cout << "Exception: " << ex.what();
    }
}

unsigned int Plan::getNumFormulas() const
{
    return numFormulas;
}

int Plan::getLevel(unsigned int formulaNum) const
{
    try {
        if(formulaNumNotInRange(formulaNum)) throw out_of_range("Number inputted is not within range.");

        return formulaArr[formulaNum - 1].getLevel();
    }

    catch(exception& ex){
        cout << "Exception: " << ex.what();
        return -1;
    }
}

Resource* Plan::apply(unsigned int formulaNum)
{
    try {
        if(formulaNumNotInRange(formulaNum)) throw out_of_range("Number inputted is not within range.");
    
        return formulaArr[formulaNum - 1].apply();
    }
    
    catch(exception& ex){
        cout << "Exception: " << ex.what();
        return nullptr;
    }
}

Resource* Plan::getInput(unsigned int formulaNum) const
{
    try {
        if(formulaNumNotInRange(formulaNum)) throw out_of_range("Number inputted is not within range.");

        return formulaArr[formulaNum - 1].getInput();
    }
    catch(exception& ex){
        cout << "Exception: " << ex.what();
        return nullptr;
    }
}

Resource* Plan::getOutput(unsigned int formulaNum) const
{
    try {
        if(formulaNumNotInRange(formulaNum)) throw out_of_range("Number inputted is not within range.");

        return formulaArr[formulaNum - 1].getOutput();
    }
    catch(exception& ex){
        cout << "Exception: " << ex.what();
        return nullptr;
    }
}

int Plan::getInputSize(unsigned int formulaNum) const
{
    try {
        if(formulaNumNotInRange(formulaNum)) throw out_of_range("Number inputted is not within range.");

        return formulaArr[formulaNum - 1].getInputSize();
    }
    catch(exception& ex){
        cout << "Exception: " << ex.what();
        return -1;
    }
}

int Plan::getOutputSize(unsigned int formulaNum) const
{
    try {
        if(formulaNumNotInRange(formulaNum)) throw out_of_range("Number inputted is not within range.");

        return formulaArr[formulaNum - 1].getOutputSize();
    }
    catch(exception& ex){
        cout << "Exception: " << ex.what();
        return -1;
    }
}

bool Plan::formulaNumNotInRange(unsigned int formulaNum) const
{
    return (numFormulas == 0 || (formulaNum - 1) >= numFormulas);
}

bool Plan::operator==(const Plan& src) const
{
    if (this == &src) return true;

    if (numFormulas == src.numFormulas
    && maxSize == src.maxSize){
        for(unsigned int x = 0; x < numFormulas; x++){
            if(formulaArr[x] == src.formulaArr[x]){
                continue;
            }
            return false;
        }
        return true;
    }
    return false;
}

bool Plan::operator!=(const Plan& src) const
{
    if (this == &src) return false;

    if (numFormulas != src.numFormulas || maxSize != src.maxSize) return true;

    for(unsigned int x = 0; x < numFormulas; x++){
        if(formulaArr[x] != src.formulaArr[x]) return true; 
    }
    
    return false;
}


Plan Plan::operator+(const Plan& other)
{
    Plan result(*this);

    for(unsigned int x = 0; x < other.numFormulas; x++){
        result.add(other.formulaArr[x]);
    }

    return result;
}

Plan& Plan::operator+=(const Plan& other)
{
    for(unsigned int x = 0; x < other.numFormulas; x++){
        add(other.formulaArr[x]);
    }

    return *this;
}

Plan& Plan::operator--()
{
    if(numFormulas > 0) remove();

    return *this;
}


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