using System;
using System.IO;
using Server;
using Server.Commands;

namespace Server.Misc
{
    public class InvokeShutdown
    {
        public static void Initialize()
        { CommandSystem.Register("Shutdown", AccessLevel.Owner, new CommandEventHandler(InvokeShutdown_OnCommand)); }

        [Usage("Shutdown")]
        [Description("Save and shutdown the server.")]
        public static void InvokeShutdown_OnCommand(CommandEventArgs e)
        {
            try
            {
                World.Broadcast(0x22, true, "The world is shutting down.");
                AutoSave.Save();
                Core.Process.Kill();
            }
            catch
            { e.Mobile.SendMessage("Shutdown Failed"); }
        }
    }
}
