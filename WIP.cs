using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeleniumFramework
{
    [TestClass]
    public class UITestController
    {
        public TestContext TestContext { get; set; }
        protected SeleniumWebDriver driver;
        protected BrowserSession browser;
        protected SessionConfiguration sessionSettings;
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
            validate = new UIAssert(TestContext);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            browser.Dispose();
            if (validate.ErrorCount > 0)
            {
                TestContext.WriteLine("The following assertions failed:\n" + validate.ErrorString);
            }
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
        public static SessionConfiguration Edge
        {
            get
            {
                return new SessionConfiguration()
                {
                    Driver = typeof(SeleniumWebDriver),
                    Browser = Browser.Edge,
                    Timeout = TimeSpan.FromSeconds(10),
                    ConsiderInvisibleElements = false,
                    RetryInterval = TimeSpan.FromMilliseconds(200),
                    TextPrecision = TextPrecision.PreferExact,
                    WaitBeforeClick = TimeSpan.FromSeconds(0),
                };
            }
        }
    }

    [TestClass]
    public class DemoTests : UITestController
    {
        [TestMethod, TestProperty("Browser", "Chrome")]
        public async Task GoogleCoypu()
        {
            browser.Visit("https://www.google.com.au");
            var z = 0;
        }
    }
}
