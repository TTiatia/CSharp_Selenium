using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumFramework.Demo.Models;
using SeleniumFramework.Framework;

namespace SeleniumFramework.Demo.Tests
{
    [TestClass]
    public class DemoTests : UITestController
    {
        [TestMethod, TestProperty("Browser", "Chrome")]
        public void Browser_Chrome() => BasicSearch();

        [TestMethod, TestProperty("Browser", "Chrome_Headless")]
        public void Browser_ChromeHeadless() => BasicSearch();

        [TestMethod, TestProperty("Browser", "Firefox")]
        public void Browser_Firefox() => BasicSearch();

        [TestMethod, TestProperty("Browser", "Edge")]
        public void Browser_Edge() => BasicSearch();

        /// <summary>
        /// Opens the google home page and performs a search.
        /// </summary>
        protected void BasicSearch()
        {
            Page_GoogleHome.Do((page) =>
            {
                page.Visit();
                page.Text_SearchBar.FillInWith("Text");
                page.Button_GoogleSearch.Click();
            });
            Page_GoogleResultList.Do((page) =>
            {
                validate.IsNotNull(page.Label_ResultStats.Text, "No result stats were found.");
            });
        }
    }
}
