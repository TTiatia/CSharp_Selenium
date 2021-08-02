using Coypu;
using Coypu.Drivers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SeleniumFramework
{
    [TestClass]
    public abstract class UITestController
    {
        public TestContext TestContext { get; set; }
        protected SeleniumWebDriver driver;
        protected BrowserSession browser;
        protected SessionConfiguration sessionSettings;
        protected bool loggingEnabled;
        protected UIAssert validate;

        [TestInitialize]
        public void TestSetup()
        {
            if (TestContext.Properties.Contains("Browser") && !string.IsNullOrEmpty(TestContext.Properties["Browser"].ToString()))
            {
                var b = TestContext.Properties["Browser"].ToString().ToLower();
                sessionSettings = b switch
                {
                    "any" => SessionTypes.Default,
                    "chrome" => SessionTypes.Chrome,
                    "chrome_headless" => SessionTypes.ChromeHeadless,
                    "firefox" => SessionTypes.Firefox,
                    "edge" => SessionTypes.Edge,
                    _ => throw new ArgumentException($"The value '{b}' is not valid for the TestPropertyAttribute 'Browser'"),
                };
            }
            else
            {
                sessionSettings = SessionTypes.Default;
            }

            browser = new BrowserSession(sessionSettings);
            browser.MaximiseWindow();

            loggingEnabled = TestContext.Properties.Contains("DebugEnabled") || (bool)TestContext.Properties.Contains("LoggingEnabled");
            validate = new UIAssert(TestContext, loggingEnabled);
            CustomPageModel.Init(browser);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            browser.Dispose();
            if (validate.ErrorCount > 0)
            {
                throw new AssertFailedException($"\n{validate.ErrorCount} assertion/s failed without terminating execution:\n{validate.ErrorString}");
            }
        }
    }
}
