using Coypu;
using SeleniumFramework.Framework;

namespace SeleniumFramework.Demo.Models
{
    public class Page_GoogleResultList : PageModel<Page_GoogleResultList>
    {
        public readonly ElementScope Label_ResultStats = browser.FindCss("#result-stats");
    }
}
