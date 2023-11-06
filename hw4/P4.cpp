// Kevin Bui
// October 5, 2023
// Last Revised: October 9, 2023
// P2.cpp
// IDE: Visual Studio Code

#include <iostream>
#include "Plan.h"

using namespace std;

Formula* createArray(int arraySize);
void printNumFormulas(const Plan& obj, const Plan& copyObj, const Plan& assignmentCopyObj);
void testRemove(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj);
void testAdd(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj);
void testReplace(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj);
void testGetLevel(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj);
void testResize(Plan& obj);

int main()
{
    int arraySize = 4;

    // client manually creates Formula objects and puts them into an array
    Formula* insert = createArray(arraySize);

    // creates an object through dependency injection
    Plan obj(insert, arraySize);
    
    // creates an object through copy constructor
    Plan copyObj(obj);

    // creates an object through the overloaded assignment operator
    Plan assignmentCopyObj = obj;
    
    testRemove(obj, copyObj, assignmentCopyObj);

    testAdd(obj, copyObj, assignmentCopyObj);

    testReplace(obj, copyObj, assignmentCopyObj);

    testGetLevel(obj, copyObj, assignmentCopyObj);

    testResize(obj);

    return 0;
}


void testResize(Plan& obj)
{
    Resource input[2];
    Resource output[1];

    input[0] = Resource("fire", 2);
    input[1] = Resource("water", 2);
    output[0] = Resource("steam", 4);

    Formula temp(input, 2, output, 1);

    cout << "\n\nBefore adding:\n";
    cout << obj.getNumFormulas();

    for(int x = 0; x < 15; x++){
        obj.add(temp);
    }
    cout << "\n\nAfter adding:\n";
    cout << obj.getNumFormulas() << endl;
}


void testGetLevel(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj)
{
    cout << "\n\nLevels before apply: \n";
    cout << "\nOriginal object: " << obj.getLevel(1) << endl;
    cout << "\nCopy object: " << copyObj.getLevel(1) << endl;
    cout << "\nAssignment copy object: " << assignmentCopyObj.getLevel(1) << endl;

    for(int x = 0; x < 5; x++){
        obj.apply(1);
        copyObj.apply(1);
        assignmentCopyObj.apply(1);
    }

    cout << "\n\nLevels after apply: \n";
    cout << "\nOriginal object: " << obj.getLevel(1) << endl;
    cout << "\nCopy object: " << copyObj.getLevel(1) << endl;
    cout << "\nAssignment copy object: " << assignmentCopyObj.getLevel(1) << endl;
}

void testReplace(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj)
{
    Resource input[2];
    Resource output[1];

    input[0] = Resource("rock", 2);
    input[1] = Resource("heat", 2);
    output[0] = Resource("lava", 4);

    Formula temp(input, 2, output, 1);

    cout << "\n\nBefore replacing:\n";
    cout << obj.getNumFormulas() << endl;

    obj.replace(1, temp);

    cout << "\n\nAfter replacing:\n";
    cout << obj.getNumFormulas() << endl;
}

void testAdd(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj)
{
    Resource input[2];
    Resource output[1];

    input[0] = Resource("fire", 2);
    input[1] = Resource("water", 2);
    output[0] = Resource("steam", 4);

    Formula temp(input, 2, output, 1);

    cout << "\n\nBefore adding:\n";
    printNumFormulas(obj, copyObj, assignmentCopyObj);


    obj.add(temp);
    copyObj.add(temp);
    assignmentCopyObj.add(temp);

    cout << "\n\nAfter adding:\n";
    printNumFormulas(obj, copyObj, assignmentCopyObj);
}


void testRemove(Plan& obj, Plan& copyObj, Plan& assignmentCopyObj)
{   
    cout << "\n\nBefore removing:\n";
    printNumFormulas(obj, copyObj, assignmentCopyObj);

    obj.remove();

    assignmentCopyObj.remove();
    assignmentCopyObj.remove();

    cout << "\n\nAfter removing:\n";
    printNumFormulas(obj, copyObj, assignmentCopyObj);
}

void printNumFormulas(const Plan& obj, const Plan& copyObj, const Plan& assignmentCopyObj)
{
    cout << "\n\nOriginal object's number of Formulas: " << obj.getNumFormulas() << "\n\n";
    cout << "\n\nCopy constructor object's number of Formulas: " << copyObj.getNumFormulas() << "\n\n";
    cout << "\n\nAssignmentCopy object's number of Formulas: " << assignmentCopyObj.getNumFormulas() << "\n\n";
}

// client instantiating each Formula object
Formula* createArray(int arraySize)
{   
    Formula* array = new Formula[arraySize];
    Resource input1[2];
    Resource output1[1];

    input1[0] = Resource("iron", 3);
    input1[1] = Resource("coal", 2);
    output1[0] = Resource("steel", 10);

    array[0] = Formula(input1, 2, output1, 1);

    Resource input2[1];
    Resource output2[1];

    input2[0] = Resource("ice", 3);
    output2[0] = Resource("water", 6);

    array[1] = Formula(input2, 1, output2, 1);

    Resource input3[2];
    Resource output3[1];

    input3[0] = Resource("milk", 1);
    input3[1] = Resource("chocolate", 2);
    output3[0] = Resource("chocolate milk", 1);

    array[2] = Formula(input3, 2, output3, 1);

    Resource input4[1];
    Resource output4[3];

    input4[0] = Resource("rock", 3);
    output4[0] = Resource("fossil", 1);
    output4[1] = Resource("crystals", 5);
    output4[2] = Resource("gemstone", 3);

    array[2] = Formula(input4, 1, output4, 3);

    return array;
}