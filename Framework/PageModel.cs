using Coypu;
using System;

namespace SeleniumFramework.Framework
{
    public class PageModel<TSelf> : BasePageModel where TSelf : new()
    {
        public virtual string URL { get; }

        public void Visit() => browser.Visit(URL);

        public static bool Do(Action<TSelf> act)
        {
            if (!typeof(TSelf).IsSubclassOf(typeof(BasePageModel)))
            {
                throw new InvalidOperationException($"{typeof(TSelf)} is not a valid PageModel class.");
            }

            try
            {
                act.Invoke(new TSelf());
                return true;
            }
            catch (FinderException)
            {
                return false;
            }
        }
    }
}
