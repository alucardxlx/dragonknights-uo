using System;
using System.IO;
using Server;
using Server.Commands;

namespace Server.Misc
{
    public class InvokeEmergencyShutdown
    {
        public static void Initialize()
        { CommandSystem.Register("911sd", AccessLevel.Owner, new CommandEventHandler(InvokeShutdown_OnCommand)); }

        [Usage("911sd")]
        [Description("EMERGENCY - NO SAVE SHUTDOWN!")]
        public static void InvokeShutdown_OnCommand(CommandEventArgs e)
        {
            try
            {
                World.Broadcast(0x22, true, "EMERGENCY - NO SAVE SHUTDOWN!");
//                AutoSave.Save();
                Core.Process.Kill();
            }
            catch
            { e.Mobile.SendMessage("Shutdown Failed"); }
        }
    }
}
