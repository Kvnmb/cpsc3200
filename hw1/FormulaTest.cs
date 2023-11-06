
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormulaClass;
using NuGet.Frameworks;

namespace FormulaTest
{
    [TestClass]
    public class FormulaTest
    {
        [TestMethod]
        public void TestFormulaConstructor()
        {
            Formula[] test = new Formula[5];

            // default constructor
            test[0] = new Formula();
            for (uint x = 1; x < 5; x++)
            {
                test[x] = new Formula(x);
            }
            string[] list = test[0].GetOutput();
            Assert.AreEqual("5 iron bar", list[0]);
            list = test[1].GetOutput();
            Assert.AreEqual("3 brass bar", list[0]);
            list = test[2].GetOutput();
            Assert.AreEqual("2 steel bar", list[0]);
            list = test[3].GetOutput();
            Assert.AreEqual("1 diamond", list[0]);
            list = test[4].GetOutput();
            Assert.AreEqual("5 iron bar", list[0]);
        }

        [TestMethod]
        public void TestInput()
        {
            Formula[] test = new Formula[4];


            for (uint x = 0; x < test.Length; x++)
            {
                test[x] = new Formula(x);
            }

            string[] list = test[0].GetInput();
            Assert.AreEqual("10 iron ore", list[0]);

            list = test[1].GetInput();
            Assert.AreEqual("3 copper ore", list[0]);
            Assert.AreEqual("9 zinc ore", list[1]);

            list = test[2].GetInput();
            Assert.AreEqual("15 iron ore", list[0]);
            Assert.AreEqual("5 coal", list[1]);

            list = test[3].GetInput();
            Assert.AreEqual("30 coal", list[0]);

        }

        [TestMethod]
        public void TestOutput()
        {
            Formula[] test = new Formula[4];


            for (uint x = 0; x < test.Length; x++)
            {
                test[x] = new Formula(x);
            }

            string[] list = test[0].GetOutput();
            Assert.AreEqual("5 iron bar", list[0]);

            list = test[1].GetOutput();
            Assert.AreEqual("3 brass bar", list[0]);

            list = test[2].GetOutput();
            Assert.AreEqual("2 steel bar", list[0]);
            Assert.AreEqual("1 stainless steel bar", list[1]);
            Assert.AreEqual("2 pig iron bar", list[2]);

            list = test[3].GetOutput();
            Assert.AreEqual("1 diamond", list[0]);

        }

        [TestMethod]
        public void TestLevel()
        {
            Formula[] test = new Formula[4];


            for (uint x = 0; x < test.Length; x++)
            {
                test[x] = new Formula(x);
            }

            for (uint x = 0; x < test.Length; x++)
            {
                test[x].Apply();
            }

            for (uint x = 0; x < test.Length; x++)
            {
                test[x].Apply();
            }

            for (uint x = 0; x < test.Length; x++)
            {
                Assert.IsTrue(test[x].GetLevel() >= 0);
            }
        }

        [TestMethod]
        public void TestMaxLevel()
        {
            Formula test = new Formula();
            for(int x = 0; x < 100; x++)
            {
                test.Apply();
            }

            Assert.IsTrue(test.GetLevel() == 5);

        }

        [TestMethod]
        public void TestApply()
        {
            Formula test = new Formula();

            string [] list = test.Apply();

            // only one will be true
            if(list.Contains("0"))
                Assert.AreEqual("0 iron bar", list[0]);
            if (list.Contains("2"))
                Assert.AreEqual("2 iron bar", list[0]);
            if (list.Contains("5"))
                Assert.AreEqual("5 iron bar", list[0]);
            if (list.Contains("8"))
                Assert.AreEqual("8 iron bar", list[0]);

        }
    }
}