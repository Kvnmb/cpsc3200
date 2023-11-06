using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FormulaClass;
using PlanClass;

namespace ExecutablePlanTest
{
    [TestClass]
    public class ExecutablePlanTest
    {
        [TestMethod]

        // Constructor that creates parent Plan through child constructor
        public void TestExecutablePlanConstructor()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula[] formula = new Formula[2];
            formula[0] = new Formula(input, 2, output, 1);

            input[0] = new Resource("water", 2);
            input[1] = new Resource("fire", 3);

            output = new Resource[1];
            output[0] = new Resource("steam", 1);

            formula[1] = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan(formula, 2);

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);
        }

        [TestMethod]
        // Tests parent functionality of Resize through child class
        public void TestResize()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            for (int x = 0; x < 15; x++)
            {
                test.Add(formula);
            }

            Assert.IsTrue(test.GetNumFormulas() > 10);
        }

        [TestMethod]
        // Tests the Plan Add() function called through child ExecutablePlan
        public void TestAdd()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);

            Resource[] inputResult = test.GetInput(1);
            Resource[] outputResult = test.GetOutput(1);

            // checks for valid add of formulas
            Assert.AreEqual(inputResult[0].name, input[0].name);
            Assert.AreEqual(inputResult[1].name, input[1].name);

            Assert.AreEqual(inputResult[0].quantity, input[0].quantity);
            Assert.AreEqual(inputResult[1].quantity, input[1].quantity);

            Assert.AreEqual(outputResult[0].name, output[0].name);

            Assert.AreEqual(outputResult[0].quantity, output[0].quantity);
        }

        [TestMethod]
        // tests whether remove is called through dynamic binding or if Plan's remove() is called
        public void TestExecutableRemove()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula[] formula = new Formula[3];
            formula[0] = new Formula(input, 2, output, 1);

            input[0] = new Resource("water", 2);
            input[1] = new Resource("fire", 3);

            output = new Resource[1];
            output[0] = new Resource("steam", 1);

            formula[1] = new Formula(input, 2, output, 1);

            formula[2] = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan(formula, 3);

            Assert.AreEqual(test.GetNumFormulas(), (uint)3);

            // test removal before completion

            test.Remove();

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);

            test.Apply(1);

            test.Apply(2);

            test.Remove();

            // should be No removal

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);
        }

        [TestMethod]
        // tests Replace before and after apply called
        public void TestExecutableReplace()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Resource[] replaceInput = new Resource[2];
            replaceInput[0] = new Resource("water", 4);
            replaceInput[1] = new Resource("fire", 5);

            Resource[] replaceOutput = new Resource[1];
            replaceOutput = new Resource[1];
            replaceOutput[0] = new Resource("steam", 7);

            Formula[] formula = new Formula[2];

            formula[0] = new Formula(input, 2, output, 1);
            formula[1] = new Formula(replaceInput, 2, replaceOutput, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula[0]);

            // test regular replace, should work
            test.Replace(1, formula[1]);

            Resource[] inputResult = test.GetInput(1);
            Resource[] outputResult = test.GetOutput(1);


            // compares previous input and output values to the replaced ones to check for change, should not be equal
            Assert.AreNotEqual(inputResult[0].name, input[0].name);
            Assert.AreNotEqual(inputResult[1].name, input[1].name);

            Assert.AreNotEqual(inputResult[0].quantity, input[0].quantity);
            Assert.AreNotEqual(inputResult[1].quantity, input[1].quantity);

            Assert.AreNotEqual(outputResult[0].name, output[0].name);

            Assert.AreNotEqual(outputResult[0].quantity, output[0].quantity);

            // test replace after apply, should not work

            test.Apply(1);

            test.Replace(1, formula[0]);

            inputResult = test.GetInput(1);
            outputResult = test.GetOutput(1);

            Assert.AreEqual(inputResult[0].name, replaceInput[0].name);
            Assert.AreEqual(inputResult[1].name, replaceInput[1].name);

            Assert.AreEqual(inputResult[0].quantity, replaceInput[0].quantity);
            Assert.AreEqual(inputResult[1].quantity, replaceInput[1].quantity);

            Assert.AreEqual(outputResult[0].name, replaceOutput[0].name);

            Assert.AreEqual(outputResult[0].quantity, replaceOutput[0].quantity);
        }

        [TestMethod]
        public void TestGetNumFormulas()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula[] formula = new Formula[2];
            formula[0] = new Formula(input, 2, output, 1);

            input[0] = new Resource("water", 2);
            input[1] = new Resource("fire", 3);

            output = new Resource[1];
            output[0] = new Resource("steam", 1);

            formula[1] = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan(formula, 2);

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);
        }


        [TestMethod]
        public void TestGetLevel()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);

            for (int x = 0; x < 15; x++)
            {
                test.Apply(1);
            }

            Assert.AreEqual(test.GetLevel(1), 5);
        }

        [TestMethod]
        public void TestApply()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);

            Resource[] result = test.Apply(1);

            Assert.AreEqual(result[0].quantity, (uint)1);

            Assert.AreEqual(test.GetLevel(1), 1);

            Assert.AreEqual(test.QueryStep(), (uint)2);

            // tests apply out of range

            result = test.Apply(2);

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void TestGetInput()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);

            Resource[] inputResult = test.GetInput(1);

            Assert.AreEqual(inputResult[0].name, input[0].name);
            Assert.AreEqual(inputResult[1].name, input[1].name);

            Assert.AreEqual(inputResult[0].quantity, input[0].quantity);
            Assert.AreEqual(inputResult[1].quantity, input[1].quantity);
        }

        [TestMethod]
        public void TestGetOutput()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);

            Resource[] outputResult = test.GetOutput(1);

            Assert.AreEqual(outputResult[0].name, output[0].name);
            Assert.AreEqual(outputResult[0].name, output[0].name);
        }

        [TestMethod]
        // calls DeepCopy using ExecutablePlan object to instantiate a Plan reference
        public void TestExecutableDeepCopy ()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);
            Plan copy = test.DeepCopy();

            input[0] = new Resource("water", 4);
            input[1] = new Resource("fire", 5);

            output = new Resource[1];
            output[0] = new Resource("steam", 7);

            formula = new Formula(input, 2, output, 1);

            copy.Add(formula);

            Assert.AreNotEqual(test.GetNumFormulas(), copy.GetNumFormulas());
        }

        [TestMethod]
        public void TestClone()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);
            ExecutablePlan copy = test.Clone();

            input[0] = new Resource("water", 4);
            input[1] = new Resource("fire", 5);

            output = new Resource[1];
            output[0] = new Resource("steam", 7);

            formula = new Formula(input, 2, output, 1);

            copy.Add(formula);

            Assert.AreNotEqual(test.GetNumFormulas(), copy.GetNumFormulas());
        }

        [TestMethod]
        public void TestQueryStep()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            ExecutablePlan test = new ExecutablePlan();

            test.Add(formula);

            Assert.AreEqual(test.QueryStep(), (uint)1);

            test.Apply(1);

            Assert.AreEqual(test.QueryStep(), (uint)2);
        }
    }
}