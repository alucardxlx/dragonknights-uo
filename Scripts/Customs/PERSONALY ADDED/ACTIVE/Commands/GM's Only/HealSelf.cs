// HealSelf Command by LIACS
// 02/20/2007

using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Commands;
using System.IO;
using System.Text;
using System.Collections;
using System.Net;
using Server.Accounting;
using Server.Items;
using Server.Menus;
using Server.Menus.Questions;
using Server.Menus.ItemLists;
using Server.Spells;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;

namespace Server.Commands
{
	public class HealSelfCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("Healself", AccessLevel.Counselor, new CommandEventHandler(HealSelf_OnCommand));
            CommandSystem.Register("HS", AccessLevel.Counselor, new CommandEventHandler(HealSelf_OnCommand));
		}

        [Usage("HealSelf")]
        [Aliases("HS")]
        [Description("Heals yourself.")]


		public static void HealSelf_OnCommand(CommandEventArgs e)
		{
            Mobile m = e.Mobile;
            m.Hits = m.HitsMax;
            m.SendMessage("You heal yourself.");
		}
	}
}
