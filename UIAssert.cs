using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
namespace SeleniumFramework
{
    public class UIAssert
    {
        public List<string> ErrorList { get; protected set; }
        public int ErrorCount { get { return ErrorList.Count; } }
        public string ErrorString { get { return string.Join("\nFAIL ", ErrorList); } }
        public bool ExitOnError { get; set; } = true;
        public bool FullLog { get; private set; }
        public List<string> Log { get; private set; }
        protected TestContext TestContext;

        public UIAssert(TestContext testContext, bool enableLog)
        {
            TestContext = testContext;
            ErrorList = new List<string>();
            Log = new List<string>();
            FullLog = enableLog;
        }

        public bool AreEqual(IComparable expected, IComparable actual, string message = null, bool continueOnFail = false) => Assert<IComparable>(expected, actual, "AreEqual", (e, a) => e.CompareTo(a) == 0, message, continueOnFail);

        public bool AreNotEqual(IComparable expected, IComparable actual, string message = null, bool continueOnFail = false) => Assert<IComparable>(expected, actual, "AreNotEqual", (e, a) => e.CompareTo(a) != 0, message, continueOnFail);

        public bool IsNull(object actual, string message = null, bool continueOnFail = false) => Assert<object>(null, actual, "IsNull", (e, a) => a == null, message, continueOnFail);

        public bool IsNotNull(object actual, string message = null, bool continueOnFail = false) => Assert<object>(null, actual, "IsNotNull", (e, a) => a != null, message, continueOnFail);

        public bool IsTrue(bool? actual, string message = null, bool continueOnFail = false) => Assert<bool?>(null, actual, "IsTrue", (e, a) => a.GetValueOrDefault() == true, message, continueOnFail);

        public bool IsFalse(bool? actual, string message = null, bool continueOnFail = false) => Assert<bool?>(null, actual, "IsFalse", (e, a) => a.GetValueOrDefault() == false, message, continueOnFail);

        /// <summary>
        /// Immediately ends the test.
        /// </summary>
        /// <param name="message">The message to report.</param>
        public void ForceFail(string message) => throw new AssertFailedException("FAIL " + message);

        /// <summary>
        /// Includes a WARN level message in the test log if it is enabled.
        /// </summary>
        /// <param name="message">The message to report.</param>
        public void Warning(string message) => WriteLine("WARN " + message);

        /// <summary>
        /// Includes an INFO level message in the test log if it is enabled.
        /// </summary>
        /// <param name="message">The message to report.</param>
        public void Infomation(string message) => WriteLine("INFO " + message);

        protected void WriteLine(string message)
        {
            if (FullLog)
            {
                TestContext.WriteLine(message);
            }
        }

        /// <summary>
        /// Underlaying assertion function which uses generic types and a lamda param to perform any validation required.
        /// </summary>
        /// <typeparam name="T">The Type of objects to take for the expected and actual parameters.</typeparam>
        /// <param name="expected">The expected value, either passed from the calling function or NULL.</param>
        /// <param name="actual">The actual value passed from the calling function.</param>
        /// <param name="name">The name of the type of assertion for reporting.</param>
        /// <param name="lambda">A lambda function taking two parameters of type T and returning a bool. This is the actual check to perform. If Invoke() returns true, the assertion passes, otherwise it fails.</param>
        /// <param name="message">The message passed from the calling function or NULL.</param>
        /// <param name="continueOnFail">Passed from the calling function, if true then the error will be recorded but no exception thrown.</param>
        /// <returns>True if assertion passed, false if not.</returns>
        protected bool Assert<T>(T expected, T actual, string name, Func<T, T, bool> lambda, string message, bool continueOnFail)
        {
            var msg = $"{name}<";
            if (expected != null)
            {
                msg += $"{expected},";
            }
            msg += $"{actual}> ";

            if (lambda.Invoke(expected, actual))
            {
                if (FullLog)
                {
                    TestContext.WriteLine("PASS " + msg);
                }
                return true;
            }
            else
            {
                if (!continueOnFail && ExitOnError)
                {
                    throw new AssertFailedException("FAIL " + msg + message ?? string.Empty);
                }

                ErrorList.Add("FAIL " + msg + message ?? string.Empty);
                if (FullLog)
                {
                    TestContext.WriteLine("FAIL " + msg + message ?? string.Empty);
                }

                return false;
            }
        }
    }
}
