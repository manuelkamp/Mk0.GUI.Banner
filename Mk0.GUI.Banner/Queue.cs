using System.Collections.Generic;

namespace Mk0.GUI.Banner
{
    public class Queue
    {
        internal readonly Queue<BannerObject> bol;

        internal Queue()
        {
            bol = new Queue<BannerObject>();
        }
    }
}
