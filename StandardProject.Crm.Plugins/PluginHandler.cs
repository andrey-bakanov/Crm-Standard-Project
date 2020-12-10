namespace StandardProject.Crm.Plugins
{
    /// <summary>
    /// Base plugin handler
    /// </summary>
    public abstract class PluginEventHandler
    {

        /// <summary>
        /// Component logic
        /// </summary>
        /// <param name="state"></param>
        public abstract void Handle(PluginState state);
    }
}
