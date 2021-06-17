using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeleniumFramework
{
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
        [TestMethod, TestProperty("Browser", "Chrome"), TestProperty("Debug", "true")]
        public async Task GoogleCoypu()
        {
            browser.Visit("https://www.google.com.au");
            validate.AreNotEqual(true, false, "Force passes");
            validate.AreEqual("One", "1", "Woopsidaisy", true);
            validate.ForceFail("KABOOOOOOM!");
        }
    }
}
