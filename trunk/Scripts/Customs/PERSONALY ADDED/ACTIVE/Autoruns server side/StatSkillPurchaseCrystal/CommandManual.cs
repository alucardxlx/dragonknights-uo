using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class CommandHelper
    {
        public static string[] m_CommandString = new string[]
            {
                // communication
                "[c",
                "[c <message>",
                "[pm <player name>",
                "[mail",
                "[point",
                // quick communication
                "[msg",
                "[reply",
                "[last",
                // hunting useful
                "[bandSelf",
                "[grab",
//                "[grab bag",
                "[grab options",
                // bag management
//                "[own",
//                "[own bag",
                "[dump",
//                "[sort <item type>",
                "[craftbag",
                "sell bag",
                "[sell options",
//                // adventurer's quest
//                "quest",
//                "[myquest",
//                "[dropquest",
//                "[rp",
//                "[rewardcheck <number>",
//                // player town guard
//                "promote",
//                "reputation",
//                "retire",
//                "[deployguard <guard type>",
//                "[releaseGuard",
                // henchmen
                "hire",
                "<name / all> help",
                "<name / all> dress",
                "<name / all> undress",
                "<name / all> arm",
                "<name / all> disarm",
                "<name / all> mount",
                "<name / all> dismount",
                "<name / all> stats",
                "<name / all> pow",
                "<name / all> grab",
                "<name / all> unload",
                "<name / all> heal",
//                // Craft
//                "[BODPoint",
//                // Misc
//                "[mp",
//                "[door",
            };
        public static string[] m_CommandDescription = new string[]
            {
                // communication
                "See a list of players online, check their status or set your own status (afk, etc). Also brings up the chat gump where you can customize various things.",

                "Chat in the public chat channel. Everybody will hear what you say.",
                
                "Send someone else a private message. The receiver will receive a private message notice, and can click on it to check the message. The message goes to the receiver's online mail box.",
                
                "Brings up your online mailbox. Can hold a maximum of 50 private messages. You can delete the old ones to free up space for new messages.",
                
                "Brings up a cursor, when target an item, a mob, or a location, will show an emote message on top of the target to indicate where you are pointing at. Great for showing your friends exactly what/where you are referring to.",

                // quick communication
                "This is a quick private messeging system, without the fancy gumps. For example, if Rocko want to send a quick private to Joshua, Rocko would type in game:" + 
                "<br>[msg joshua hi there, how are you?" +
                "<br>Joshua would receive the following message on his screen:" +
                "<br>Rocko tells you: hi there, how are you?",
                
                "This pairs up with command [msg. It sends a rely directly to the last person who have msg'ed you, with no need to type in his name. SO in the example of [msg, Joshua can reply to Rocko with:" +
                "<br>[reply I am all good."+
                "<br>Rocko will receive:"+
                "<br>Joshua tells you: I am all good.",

                "This will send another message to the last player you used [msg to send a message to. So in the above example, Rocko wants to send another message. He could do [reply, which will rely to Joshua. But imagine another player Vothug just sent a message after Joshua's reply to Rocko, if Rocko uses [reply command, he will reply to Vothug instead. Instead, Rocko could type"+
                "<br>[last Have fun." +
                "<br>Joshua would receive the message, not Vothug.",

                // hunting usefuls
                "Apply a bandage on yourself if you have any in your backpack.",

                "Loot items from corpse and delete the corpse afterwards. Receives a very small amount of exta gold for cleaning up the corpse as well. Use command \"[grab options\" to set the types of items you want to loot.",
                
//                "Give you a target cursor so you could assign a container within your backpack as the loot bag, where all loots from the [grab command will go to. If you do not assign one, your backpack is the default loot bag.",
                
                "Allow you to set what types of items you want to loot using the [grab command. Gold, skill gems and dungeon keys are always looted. The rest are your own choice.",

//                // bag management
//                "Toggle owner's mark on an item to indicate you own an item. Item that can be owned including weapon, armor, jewelry, clothes, spellbook, and container." +
//                "<br>An owned item will show the owner's character name." +
//                "<br>An owned item can not be sold to the vendor by someone other than the owner." +
//                "<br>An owned item can not be recycled into magic points by someone other than the owner." +
//                "<br>An owner's mark can not be removed by someone other than the owner.",
//
//                "Toggle owner's mark on a bag of items. " +
//                "<br>If the bag itself is not owner marked, this command will mark the bag as well as any owner markable items inside. If the bag itself already bears an owner's mark, this command will remove the mark from the bag, as well as the owner's mark from any item inside. The same rule applies that \"An owner's mark can not be removed by someone other than the owner\"." +
//                "<br>You can drop someone else's owner marked item onto any banker or minter, and they will be put into the owner's bankbox. If you drop a bag that is owner marked, the bag as well as all items inside will be put into the owner's bankbox.",
//
                "Dump all items from one container to another container. The first container must be your backpack or a bag inside your backpack.",
//
                "Dump all items of a particular type from one container to another container. The first container must be your backpack or a bag inside your backpack. The item type is one of the following:"+
                "<br>\"gems\", \"regs\", \"scrolls\", \"potions\", \"jewelry\", \"wands\", \"hides\", \"armor\", \"clothing\", \"weapons\"." +
                "<br>You can also just type [sort and it will give you a list of valid itemtypes you can use.",
//
                "This command allows you to assign a bag inside your backpack, that all the items you crafted will go into. The default is your backpack.",
//
                "This is a keyword, not a command. You can speak this keyword within 1 tile of a vendor, and you will be prompted to target a bag inside your backpack. Once you target the bag, the vendor will try to buy anything he is interested inside that bag, without going through a gump where you have to manually select which item to sell. Saves a lot of mouse clicking if you know for sure things inside that bag is no use to you but monetary value. " +
                "<br>The best vendor to use this keyword with is the one titled \"garbage collector\". He buys anything other vendor may buy from you.",
//
                "Allows a player to specify what types of items he/she does not want to sell when using the \"sell bag\" keyword to sell everything at once inside a bag.",
//
//                // adventurer's quest
//                "Speak this keyword when you are next to the master of adventurer (MOA) will initiate a solo quest request. To request a party quest, you need to use the context menu entry \"party\" on MOA.",
//
//                "Give you information of the adventurer's quest you are currently holding. Which mob group, what level, and which quest type.",
//
//                "Drop your current adventurer's quest. This will abandon the current quest and all the kills/rescues you have made for it. It will also expire the quest dungeon you initiated, and if you or your pets are still inside of it, will eject everybody out to the entrance.",
//
//                "Shows you how much reward point you currently have.",
//
//                "This will convert that much of reward point you have into a reward check. The number need to be equal or larger than 10. Then you can give the reward check to other people for trading. To cash it, simply double click the check while it is in your backpack.",
//
//                // player town guard
//                "Speak this keyword next to a defense minister will promote you up along the rank, given you have met the requirement."+ 
//                "<br>Requirement for the first rank, town guard, is you have at least 80.1 in either tactics or meditation."+
//                "<br>Requirement for the second rank, guard captain, is you have acumulated 10K in defense reputation point (you gain them by killing offensive mobs in the town you are in active town guard duty).",
//
//                "Speak this keyword next to the defense minister will show you how much defense reputation you got.",
//
//                "Speak this keyword next to the defense minister will remove you of any town guard rank you currently holds. You do not lose any reputation by doing so.",
//
//                "When you are a guard captain, use this command inside the town you are in active guard captain duty to deploy a NPC town guard at your current world location. guard type can be either \"warrior\" or \"archer\"." +
//                "There is a cap of 5 NPC guards per town, currently shared by all captains of that town. When you have reached the cap and want to switch some of them to other locations, you would want to remove those from the less desired location, which is the purpose of the [releaseguard command.",
//
//                "When you are a guard captain, use this command and target a NPC guard to release him from his duty. It is actually a good practice to remove town guards whenever you don't need them anymore, so other players can use them when such need rises elsewhere in the town.",
//
                // henchmen
                "Say this keyword next to a henchman not in employment yet, and he will tell you how much he charges per UO day (which is 2 real life hours). Drop a pile of gold on him to hire him. ",

                "A keyword command you speak to your henchmen. This list all the special commands the henchman will follow.",
                
                "A keyword command you speak to your henchmen. The henchman will attempt to equip himself from the top layer of his backpack. Equipments include clothing, armors, shields, jewelries - pretty much anything a player can wear. " +
                "<br>This is probably the most important command to remember, as when your henchman is dressed up with something you gave him and then dies, once you ressurect him, he will not attempt to re-dress, until you told him to. When you are still in a hostile environment, you want to issue this command quickly to bring him back under protection.",

                "A keyword command you speak to your henchmen. The henchman will get everything undressed into his backpack, including weapons. ",
                
                "A keyword command you speak to your henchmen. The henchman will attempt to equip himself with weapon/shield from the top layer of his backpack. If there are multiple weapons/shields in his backpack, re-issuing the command will make him cycle through each one of them.",
                
                "A keyword command you speak to your henchmen. The henchman will put any weapons/shield he wields into his backpack. ",
                
                "A keyword command you speak to your henchmen. Have the henchman ride a mount you designate to him. ",
                
                "A keyword command you speak to your henchmen. Have the henchman dismount. ",
                
                "A keyword command you speak to your henchmen. This will bring up a gump that shows the vital stats and skills of the henchman. This is a gump similar to the animal lore gump, and tailored for henchman. ",
                
                "A keyword command you speak to your henchmen. This will command the henchman to use special attack ability for his next attack, more or less like a player. If the henchman has enough mana, he will charge it and use it. Otherwise he will wait until he has enough mana. This is only useful for fighter and archer, as currently the mage AI is bare handed and will not try engage the mob and use the only possible special attack \"disarm\". ",
                
                "A keyword command you speak to your henchmen. The henchman will try grab anything around him on the ground, and try loot any corpse around him that he has looting right to. The henchman's looting right is the same as your looting right. There is a special bag called lootbag in every henchman's backpack. Anything he picked up will be dumped into his lootbag.",

                "A keyword command you speak to your henchmen. The henchman will unload everything in his lootbag as well as anything in his backpack top layer (saving supplies and his original gear) to your backpack.",

                "A keyword command you speak to your henchmen. The henchman will try to heal you with bandages if he is not occupied with healing himself, and you are close enough.",

//                // Craft
//                "Gives you a target cursor, then you can target a BOD (any type) to evaluate its reward point, which will decide what reward you can get when you do fill and return said BOD.",
//
//                // Misc
//                "This command gives you a target cursor, and you can target an item to evaluate its magic point, which is how much the magic dealer will reward you if you drop that item on him to recycle. Always good to use this command to check an item before you recycle it to be sure it is worth it.",
//
//                "Opens or closes all doors within 2 tiles range. A locked door will open only if you have the right key in your backpack to unlock it.",

            };
    }
    public class CommandDescriptionGump : Gump
    {

        public string Center(string text)
        {
            return String.Format("<CENTER>{0}</CENTER>", text);
        }

        public string Color(string text, int color)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        }

        public CommandDescriptionGump(Mobile from, CommandName name)
            : base(125, 125)
        {
            from.CloseGump(typeof(CommandDescriptionGump));

            AddPage(0);

            AddBackground(0, 0, 400, 200, 0x2436);

            AddHtml(10, 10, 390, 20, Color(Center(CommandHelper.m_CommandString[(int)name]), 0x52D017), false, false);

            AddHtml(10, 30, 390, 145, Color(CommandHelper.m_CommandDescription[(int)name], 0xFFFFFF), false, true);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            if (info.ButtonID == 0)
            {
            }
            //    from.SendMessage("Closed.");
        }
    }

    public class CommandManualGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("Command", AccessLevel.Player, new CommandEventHandler(Command_OnCommand));
        }

        [Usage("Command")]
        [Description("List most commonly used player commands and their descriptions.")]
        private static void Command_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new CommandManualGump());
        }


        public static bool OldStyle = PropsConfig.OldStyle;

        public static readonly int GumpOffsetX = PropsConfig.GumpOffsetX;
        public static readonly int GumpOffsetY = PropsConfig.GumpOffsetY;

        public static readonly int TextHue = PropsConfig.TextHue;
        public static readonly int TextOffsetX = PropsConfig.TextOffsetX;

        public static readonly int OffsetGumpID = PropsConfig.OffsetGumpID;
        public static readonly int HeaderGumpID = PropsConfig.HeaderGumpID;
        public static readonly int EntryGumpID = PropsConfig.EntryGumpID;
        public static readonly int BackGumpID = PropsConfig.BackGumpID;
        public static readonly int SetGumpID = PropsConfig.SetGumpID;

        public static readonly int SetWidth = PropsConfig.SetWidth;
        public static readonly int SetOffsetX = PropsConfig.SetOffsetX, SetOffsetY = PropsConfig.SetOffsetY;
        public static readonly int SetButtonID1 = PropsConfig.SetButtonID1;
        public static readonly int SetButtonID2 = PropsConfig.SetButtonID2;

        public static readonly int PrevWidth = PropsConfig.PrevWidth;
        public static readonly int PrevOffsetX = PropsConfig.PrevOffsetX, PrevOffsetY = PropsConfig.PrevOffsetY;
        public static readonly int PrevButtonID1 = PropsConfig.PrevButtonID1;
        public static readonly int PrevButtonID2 = PropsConfig.PrevButtonID2;

        public static readonly int NextWidth = PropsConfig.NextWidth;
        public static readonly int NextOffsetX = PropsConfig.NextOffsetX, NextOffsetY = PropsConfig.NextOffsetY;
        public static readonly int NextButtonID1 = PropsConfig.NextButtonID1;
        public static readonly int NextButtonID2 = PropsConfig.NextButtonID2;

        public static readonly int OffsetSize = PropsConfig.OffsetSize;

        public static readonly int EntryHeight = PropsConfig.EntryHeight;
        public static readonly int BorderSize = PropsConfig.BorderSize;

        /*
        private static bool PrevLabel = OldStyle, NextLabel = OldStyle;

        private static readonly int PrevLabelOffsetX = PrevWidth + 1;
		
        private static readonly int PrevLabelOffsetY = 0;

        private static readonly int NextLabelOffsetX = -29;
        private static readonly int NextLabelOffsetY = 0;
         * */

        private static readonly int NameWidth = 107;
        private static readonly int ValueWidth = 128;

        private static readonly int EntryCount = 15;

        private static readonly int TypeWidth = NameWidth + OffsetSize + ValueWidth;

        private static readonly int TotalWidth = OffsetSize + NameWidth + OffsetSize + ValueWidth + OffsetSize + SetWidth + OffsetSize;
        private static readonly int TotalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (EntryCount + 1));

        private static readonly int BackWidth = BorderSize + TotalWidth + BorderSize;
        private static readonly int BackHeight = BorderSize + TotalHeight + BorderSize;

        private static readonly int IndentWidth = 12;

        private CommandsGumpGroup[] m_Groups;
        private CommandsGumpGroup m_Selected;

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile m_From = sender.Mobile;
            int buttonID = info.ButtonID - 1;

            int index = buttonID / 3;
            int type = buttonID % 3;

            switch (type)
            {
                case 0:
                    {
                        if (index >= 0 && index < m_Groups.Length)
                        {
                            CommandsGumpGroup newSelection = m_Groups[index];

                            if (m_Selected != newSelection)
                                m_From.SendGump(new CommandManualGump(newSelection));
                            else
                                m_From.SendGump(new CommandManualGump(null));
                        }

                        break;
                    }
                case 1:
                    {
                        if (m_Selected != null && index >= 0 && index < m_Selected.Commands.Length)
                        {
                            CommandName cn = m_Selected.Commands[index];

                            m_From.SendGump(new CommandDescriptionGump(sender.Mobile, cn));
                            m_From.SendGump(new CommandManualGump(m_Selected));
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        public int GetButtonID(int type, int index)
        {
            return 1 + (index * 3) + type;
        }

        public CommandManualGump()
            : this(null)
        {
        }

        public CommandManualGump(CommandsGumpGroup selected)
            : base(GumpOffsetX, GumpOffsetY)
        {
            m_Groups = CommandsGumpGroup.Groups;
            m_Selected = selected;

            int count = m_Groups.Length;

            if (selected != null)
                count += selected.Commands.Length;

            int totalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (count + 1));

            AddPage(0);

            AddBackground(0, 0, BackWidth, BorderSize + totalHeight + BorderSize, BackGumpID);
            AddImageTiled(BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID);

            int x = BorderSize + OffsetSize;
            int y = BorderSize + OffsetSize;

            int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0);

            if (OldStyle)
                AddImageTiled(x, y, TotalWidth - (OffsetSize * 3) - SetWidth, EntryHeight, HeaderGumpID);
            else
                AddImageTiled(x, y, PrevWidth, EntryHeight, HeaderGumpID);

            x += PrevWidth + OffsetSize;

            if (!OldStyle)
                AddImageTiled(x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, HeaderGumpID);

            x += emptyWidth + OffsetSize;

            if (!OldStyle)
                AddImageTiled(x, y, NextWidth, EntryHeight, HeaderGumpID);

            for (int i = 0; i < m_Groups.Length; ++i)
            {
                x = BorderSize + OffsetSize;
                y += EntryHeight + OffsetSize;

                CommandsGumpGroup group = m_Groups[i];

                AddImageTiled(x, y, PrevWidth, EntryHeight, HeaderGumpID);

                if (group == selected)
                    AddButton(x + PrevOffsetX, y + PrevOffsetY, 0x15E2, 0x15E6, GetButtonID(0, i), GumpButtonType.Reply, 0);
                else
                    AddButton(x + PrevOffsetX, y + PrevOffsetY, 0x15E1, 0x15E5, GetButtonID(0, i), GumpButtonType.Reply, 0);

                x += PrevWidth + OffsetSize;

                x -= (OldStyle ? OffsetSize : 0);

                AddImageTiled(x, y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, EntryGumpID);
                AddLabel(x + TextOffsetX, y, TextHue, group.Name);

                x += emptyWidth + (OldStyle ? OffsetSize * 2 : 0);
                x += OffsetSize;

                if (SetGumpID != 0)
                    AddImageTiled(x, y, SetWidth, EntryHeight, SetGumpID);

                if (group == selected)
                {
                    int indentMaskX = BorderSize;
                    int indentMaskY = y + EntryHeight + OffsetSize;

                    for (int j = 0; j < group.Commands.Length; ++j)
                    {
                        CommandName cn = group.Commands[j];

                        x = BorderSize + OffsetSize;
                        y += EntryHeight + OffsetSize;

                        x += OffsetSize;
                        x += IndentWidth;

                        AddImageTiled(x, y, PrevWidth, EntryHeight, HeaderGumpID);

                        AddButton(x + PrevOffsetX, y + PrevOffsetY, 0x15E1, 0x15E5, GetButtonID(1, j), GumpButtonType.Reply, 0);

                        x += PrevWidth + OffsetSize;

                        x -= (OldStyle ? OffsetSize : 0);

                        AddImageTiled(x, y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0) - OffsetSize - IndentWidth, EntryHeight, EntryGumpID);
                        AddLabel(x + TextOffsetX, y, TextHue, CommandHelper.m_CommandString[(int)cn]);

                        x += emptyWidth + (OldStyle ? OffsetSize * 2 : 0) - OffsetSize - IndentWidth;
                        x += OffsetSize;

                        if (SetGumpID != 0)
                            AddImageTiled(x, y, SetWidth, EntryHeight, SetGumpID);

                    }

                    AddImageTiled(indentMaskX, indentMaskY, IndentWidth + OffsetSize, (group.Commands.Length * (EntryHeight + OffsetSize)) - (i < (m_Groups.Length - 1) ? OffsetSize : 0), BackGumpID + 4);
                }
            }
        }
    }

    public enum CommandName
    	{
    	// communication
    	C,
        CMessage,
        Pm,
        Mail,
        Point,
        // quick communication
        Msg,
        Reply,
        Last,
        // hunting useful
        BandSelf,
        Grab,
        //GrabBag,
        GrabOptions,
        // bag management
        //Own,
        //OwnBag,
        Dump,
        //Sort,
        CraftBag,
        SellBag,
        SellOptions,
        // adventurer's quest
		//Quest,
		//MyQuest,
		//DropQuest,
		//Rp,
		//RewardCheck,
		//// player town guard
		//Promote,
		//Reputation,
		//Retire,
		//DeployGuard,
		//ReleaseGuard,
        // henchmen
        Hire,
        HHelp,
        HDress,
        HUndress,
        HArm,
        HDisarm,
        HMount,
        HDismount,
        HStats,
        HPow,
        HGrab,
        HUnload,
        HHeal,
        //// Craft
		//BODPoint,
		//// Misc
		//MP,
		//Door,
    }

    public class CommandsGumpGroup
    {
        private string m_Name;
        private CommandName[] m_Commands;

        public string Name { get { return m_Name; } }
        public CommandName[] Commands { get { return m_Commands; } }

        public CommandsGumpGroup(string name, CommandName[] commands)
        {
            m_Name = name;
            m_Commands = commands;

            //Array.Sort(m_Skills, new SkillNameComparer());
        }

        private class CommandNameComparer : IComparer
        {
            public CommandNameComparer()
            {
            }

            public int Compare(object x, object y)
            {
                CommandName a = (CommandName)x;
                CommandName b = (CommandName)y;

                string aName = a.ToString();
                string bName = a.ToString();

                return aName.CompareTo(bName);
            }
        }

        private static CommandsGumpGroup[] m_Groups = new CommandsGumpGroup[]
        	{
        	new CommandsGumpGroup( "Communication", new CommandName[]
        	                      {
        	                      	CommandName.C,
        	                      	CommandName.CMessage,
        	                      	CommandName.Pm,
        	                      	CommandName.Mail,
        	                      	CommandName.Point,
        	                      	}
        	                     ),
        	new CommandsGumpGroup( "Quick Communication", new CommandName[]
        	                      {
        	                      	CommandName.Msg,
        	                      	CommandName.Reply,
        	                      	CommandName.Last,
        	                      	}
        	                     ),
        	new CommandsGumpGroup( "Hunting Usefuls", new CommandName[]
        	                      {
        	                      	CommandName.BandSelf,
        	                      	CommandName.Grab,
        	                      	//CommandName.GrabBag,
        	                      	CommandName.GrabOptions,
        	                      	}
        	                     ),
        	new CommandsGumpGroup( "Bag Management", new CommandName[]
        	                      {
        	                      	//CommandName.Own,
        	                      	//CommandName.OwnBag,
        	                      	CommandName.Dump,
        	                      	//CommandName.Sort,
        	                      	//CommandName.CraftBag,
        	                      	CommandName.SellBag,
        	                      	CommandName.SellOptions,
        	                      	}
        	                     ),
        	//new CommandsGumpGroup( "Adventurer's Quest", new CommandName[]
        	//				{
			//                    CommandName.Quest,
			//                    CommandName.MyQuest,
			//                    CommandName.DropQuest,
			//                    CommandName.Rp,
			//                    CommandName.RewardCheck,
			//                } ),
			//				new CommandsGumpGroup( "Player Town Guard", new CommandName[]
			//				{
			//                    CommandName.Promote,
			//                    CommandName.Reputation,
			//                    CommandName.Retire,
			//                    CommandName.DeployGuard,
			//                    CommandName.ReleaseGuard,
			//				} ),
			new CommandsGumpGroup( "Henchmen", new CommandName[]
			                      {
			                      	CommandName.Hire,
			                      	CommandName.HHelp,
			                      	CommandName.HUndress,
			                      	CommandName.HArm,
			                      	CommandName.HDisarm,
			                      	CommandName.HMount,
			                      	CommandName.HDismount,
			                      	CommandName.HStats,
			                      	CommandName.HPow,
			                      	CommandName.HGrab,
			                      	CommandName.HUnload,
			                      	CommandName.HHeal,
			                      }
			                     ),
			//				new CommandsGumpGroup( "Craft Related", new CommandName[]
			//				{
			//                    CommandName.BODPoint,
			//				} ),
			//				new CommandsGumpGroup( "Misc", new CommandName[]
			//				{
			//                    CommandName.MP,
			//                    CommandName.Door,
			//				} ),
			};
        
        public static CommandsGumpGroup[] Groups
        	{
        	get { return m_Groups; }
        	}
        }
    }
    
