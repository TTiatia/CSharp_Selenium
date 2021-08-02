using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeleniumFramework
{
    public class CustomChromeSeleniumWebDriver : SeleniumWebDriver
    {
        public CustomChromeSeleniumWebDriver(Browser browser) : base(browser) { }

        private static RemoteWebDriver CustomChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("useAutomationExtension", "true");
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddArguments("--disable-extensions");

            return new ChromeDriver(chromeOptions);
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

        public static SessionConfiguration ChromeHeadless
        {
            get
            {
                return new SessionConfiguration()
                {
                    Driver = typeof(CustomChromeSeleniumWebDriver),
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
        [TestMethod, TestProperty("Browser", "Firefox")]
        public async Task GoogleFFExample()
        {
            Page_GoogleHome.Do((page) =>
            {
                page.Visit();
                page.Text_SearchBar.FillInWith("Text");
                page.Button_GoogleSearch.Click();
            });
        }

        [TestMethod, TestProperty("Browser", "Chrome_Headless")]
        public async Task GoogleHeadless()
        {
            Page_GoogleHome.Do((page) =>
            {
                page.Visit();
                page.Text_SearchBar.FillInWith("Text");
                page.Button_GoogleSearch.Click();
            });
        }
    }

    public abstract class CustomPageModel { }

    public class PageModel<TSelf> : CustomPageModel where TSelf : new()
    {
        protected static BrowserSession browser;
        protected static ElementScope Scope { get { return browser.FindXPath("./"); } }

        public virtual string URL { get; }

        public void Visit() => browser.Visit(URL);

        public static void Init(BrowserSession session)
        {
            browser ??= session;
        }

        public static bool Do(Action<TSelf> act)
        {
            if (!typeof(TSelf).IsSubclassOf(typeof(CustomPageModel)))
            {
                throw new InvalidOperationException($"{typeof(TSelf)} is not a valid PageModel class.");
            }

            try
            {
                act.Invoke(new TSelf());
                return true;
            }
            catch (FinderException e)
            {
                return false;
            }
        }
    }

    public class Page_GoogleHome : PageModel<Page_GoogleHome>
    {
        public override string URL => "https://www.google.com.au";
        public readonly ElementScope Text_SearchBar = Scope.FindXPath(".//input[@title='Search']");
        public readonly ElementScope Button_GoogleSearch = Scope.FindButton("Google Search", new Options() { Match = Match.First });
        public readonly ElementScope Button_ImFeelingLucky = Scope.FindButton("I'm Feeling Lucky", new Options() { Match = Match.First });
    }
}