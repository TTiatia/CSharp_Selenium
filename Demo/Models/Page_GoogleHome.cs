using Coypu;
using SeleniumFramework.Framework;

namespace SeleniumFramework.Demo.Models
{
    public class Page_GoogleHome : PageModel<Page_GoogleHome>
    {
        public override string URL => "https://www.google.com.au";
        public readonly ElementScope Text_SearchBar = browser.FindXPath(".//input[@title='Search']");
        public readonly ElementScope Button_GoogleSearch = browser.FindButton("Google Search", new Options() { Match = Match.First });
        public readonly ElementScope Button_ImFeelingLucky = browser.FindButton("I'm Feeling Lucky", new Options() { Match = Match.First });
    }
}
