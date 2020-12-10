using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Crm.Plugins
{
    public sealed class PluginHandlerRegistrator
    {
        private List<PluginEventHandler> _HandlerList = new List<PluginEventHandler>();



        public void Register(PluginEventHandler handler)
        {
            if (handler != null)
            {
                _HandlerList.Add(handler);
            }
        }


        public IEnumerable<PluginEventHandler> CreateHandlers()
        {
            return _HandlerList;
        }
    }
}
