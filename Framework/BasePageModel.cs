using Coypu;

namespace SeleniumFramework.Framework
{
    public class BasePageModel
    {

        protected static BrowserSession browser;

        public static void Init(BrowserSession session)
        {
            browser ??= session;
        }
    }
}
