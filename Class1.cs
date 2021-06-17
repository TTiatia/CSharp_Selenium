using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SeleniumFramework
{
    [TestClass]
    public class UITestController
    {
        public TestContext TestContext;
        protected SeleniumWebDriver driver;
        protected BrowserSession browser;
        protected SessionConfiguration sessionSettings;

        [TestInitialize]
        public void TestSetup()
        {
            if (TestContext.Properties.Contains("Browser") && !string.IsNullOrEmpty(TestContext.Properties["Browser"].ToString()))
            {
                var b = TestContext.Properties["Browser"].ToString().ToLower();
                sessionSettings = b switch
                {
                    "chrome" => SessionTypes.Default,
                    _ => throw new ArgumentException($"The value '{b}' is not valid for the TestPropertyAttribute 'Browser'"),
                };
            }
            else
            {
                
            }
            browser = new BrowserSession(sessionSettings);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            browser.Dispose();
        }
    }

    public static class SessionTypes
    {
        public static SessionConfiguration Default { get { return Chrome; } }
        public static SessionConfiguration Chrome
        {
            get
            {
                return new SessionConfiguration()
                {
                    Driver = typeof(SeleniumWebDriver),
                    Browser = Browser.Chrome,
                    Timeout = TimeSpan.FromSeconds(10),
                    ConsiderInvisibleElements = false,
                    RetryInterval = TimeSpan.FromMilliseconds(200),
                    TextPrecision = TextPrecision.PreferExact,
                    WaitBeforeClick = TimeSpan.FromSeconds(0),
                };
            }
        }

        public static SessionConfiguration Firefox
        {
            get
            {
                return new SessionConfiguration()
                {
                    Driver = typeof(SeleniumWebDriver),
                    Browser = Browser.Firefox,
                    Timeout = TimeSpan.FromSeconds(10),
                    ConsiderInvisibleElements = false,
                    RetryInterval = TimeSpan.FromMilliseconds(200),
                    TextPrecision = TextPrecision.PreferExact,
                    WaitBeforeClick = TimeSpan.FromSeconds(0),
                };
            }
        }
    }
}
