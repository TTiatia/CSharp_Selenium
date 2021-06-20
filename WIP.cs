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
            Page_GoogleHome.Visit();
            Page_GoogleHome.Text_SearchBar.FillInWith("Text");
            Page_GoogleHome.Button_GoogleSearch.Click();

            var wait = "here";
        }
    }

    public abstract class PageModel
    {
        protected static BrowserSession browser;
        protected static ElementScope Body { get { return browser.FindXPath(".//body"); } }

        public static void Init(BrowserSession session)
        {
            browser = session;
        }
    }

    public class Page_GoogleHome : PageModel
    {
        public static readonly string URL = "https://www.google.com.au";
        public static readonly ElementScope Text_SearchBar = Body.FindXPath(".//input[@title='Search']");
        public static readonly ElementScope Button_GoogleSearch = Body.FindButton("Google Search", new Options() { Match = Match.First });
        public static readonly ElementScope Button_ImFeelingLucky = Body.FindButton("I'm Feeling Lucky", new Options() { Match = Match.First });

        public static void Visit() => browser.Visit("https://www.google.com.au");
    }
}

