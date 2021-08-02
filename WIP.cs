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
    public class CustomChromeProfileSeleniumWebDriver : SeleniumWebDriver
    {
        public CustomChromeProfileSeleniumWebDriver(Browser browser)
            : base(CustomProfile(), browser)
        {
        }

        private static RemoteWebDriver CustomProfile()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
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
                    Driver = typeof(CustomChromeProfileSeleniumWebDriver),
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

    public class CustomPageModel {

        protected static BrowserSession browser;

        public static void Init(BrowserSession session)
        {
            browser ??= session;
        }
    }

    public class PageModel<TSelf> : CustomPageModel where TSelf : new()
    {
        public virtual string URL { get; }

        public void Visit() => browser.Visit(URL);

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
        public readonly ElementScope Text_SearchBar = browser.FindXPath(".//input[@title='Search']");
        public readonly ElementScope Button_GoogleSearch = browser.FindButton("Google Search", new Options() { Match = Match.First });
        public readonly ElementScope Button_ImFeelingLucky = browser.FindButton("I'm Feeling Lucky", new Options() { Match = Match.First });
    }
}