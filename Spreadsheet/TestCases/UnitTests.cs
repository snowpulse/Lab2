﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace TestCases
{
    /// <summary>
    /// These test cases are in no sense comprehensive!  They are intended primarily to show you
    /// how to create your own, which we strong recommend that you do!  To run them, pull down
    /// the Test menu and do Run > All Tests.
    /// </summary>

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct1()
        {
            Formula f = new Formula("x");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct2()
        {
            Formula f = new Formula("2++3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct3()
        {
            Formula f = new Formula("2 3");
        }

        [TestMethod]
        public void Evaluate1()
        {
            Formula f = new Formula("2+3");
            Assert.AreEqual(f.Evaluate(s => 0), 5.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate2()
        {
            Formula f = new Formula("x5");
            Assert.AreEqual(f.Evaluate(s => 22.5), 22.5, 1e-6);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate3()
        {
            Formula f = new Formula("x5 + y6");
            f.Evaluate(s => { throw new ArgumentException(); });
            
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate4()
        {
            Formula f = new Formula("(c4 + 7) * 8");
            f.Evaluate(ValueLookup.Look);
        }

        [TestMethod]
        public void Evaluate5()
        {
            Formula f = new Formula("(cc4 + 7) * 8");
            Assert.AreEqual(f.Evaluate(ValueLookup.Look), 360.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate6()
        {
            Formula f = new Formula("((cc4 * x5) / 5 + 3) * r6");
            Assert.AreEqual(f.Evaluate(ValueLookup.Look), 684.7, 1e-6);
        }

        [TestMethod]
        public void Evaluate7()
        {
            Formula f = new Formula("cc4 * 5 / (x5 + 3 - 9) ");
            Assert.AreEqual(f.Evaluate(ValueLookup.Look), -95, 1e-6);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void gogo2()
        {
            Formula f = new Formula("((cc4) + 7) * 8 ++");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void gogo3()
        {
            Formula f = new Formula(("*"));
        }

        [TestMethod]
        public void gogo4()
        {
            Formula f = new Formula("x5");
            Assert.AreEqual(f.Evaluate(ValueLookup.Look), 4, 1e-6);
        }
    }
}
public class ValueLookup
{
    public static double Look(string s)
    {
       if(s.Equals("x5"))
        return 4;

       if (s.Equals("cc4"))
           return 38;
       if (s.Equals("z98"))
           return 12;
       if (s.Equals("r6"))
           return 20.5;
       else
           throw new ArgumentException();
    }
}