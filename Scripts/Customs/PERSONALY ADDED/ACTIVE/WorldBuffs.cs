using System;
using Server.Network;
using Server.Gumps;
using Server.Items;


namespace Server.Items
{
	public class WorldBuffsController : Item
	{		
		#region Bless Buff
		public static string m_Give_Bless_Buff; // yes or no
		[CommandProperty(AccessLevel.GameMaster)]
		public string a_Give_Bless_Buff { get { return m_Give_Bless_Buff; } set { m_Give_Bless_Buff = value; InvalidateProperties(); } }
		
		public static int m_strbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int b_strbonustogive { get { return m_strbonustogive; } set { m_strbonustogive = value; InvalidateProperties(); } }
		
		public static int m_dexbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int c_dexbonustogive { get { return m_dexbonustogive; } set { m_dexbonustogive = value; InvalidateProperties(); } }
		
		public static int m_intbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int d_intbonustogive { get { return m_intbonustogive; } set { m_intbonustogive = value; InvalidateProperties(); } }
		#endregion Bless Buff
		
		#region Resist Buff
		public static string m_Give_Resist_Buff;//yes or no
		[CommandProperty(AccessLevel.GameMaster)]
		public string e_Give_Resist_Buff { get { return m_Give_Resist_Buff; } set { m_Give_Resist_Buff = value; InvalidateProperties(); } }
		
		public static int m_physicalresistancebonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int f_physicalresistancebonustogive { get { return m_physicalresistancebonustogive; } set { m_physicalresistancebonustogive = value; InvalidateProperties(); } }
		
		public static int m_fireresistancebonusbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int g_fireresistancebonusbonustogive { get { return m_fireresistancebonusbonustogive; } set { m_fireresistancebonusbonustogive = value; InvalidateProperties(); } }
		
		public static int m_coldresistancebonusbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int h_coldresistancebonusbonustogive { get { return m_coldresistancebonusbonustogive; } set { m_coldresistancebonusbonustogive = value; InvalidateProperties(); } }
		
		public static int m_poisonresistancebonusbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int i_poisonresistancebonusbonustogive { get { return m_poisonresistancebonusbonustogive; } set { m_poisonresistancebonusbonustogive = value; InvalidateProperties(); } }
		
		public static int m_energyresistancebonusbonustogive;//10
		[CommandProperty(AccessLevel.GameMaster)]
		public int j_energyresistancebonusbonustogive { get { return m_energyresistancebonusbonustogive; } set { m_energyresistancebonusbonustogive = value; InvalidateProperties(); } }
		#endregion Resist Buff
		
		[Constructable]
		public WorldBuffsController() : base( 0x3660 )
		{
			Name="World Buff Controller";
			Movable = false;
			Visible = false;
		}
        public override void OnDoubleClick( Mobile m )
        {
        	if (m.AccessLevel > AccessLevel.GameMaster)
        		m.SendGump( new PropertiesGump( m, this ) );
        	else
        	{
        		m.SendMessage(38,"You can not access that because you are not a GameMaster.");
        	}
        }

		public WorldBuffsController( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			
			writer.Write((string)m_Give_Bless_Buff);
			writer.Write((int)m_strbonustogive);
			writer.Write((int)m_dexbonustogive);
			writer.Write((int)m_intbonustogive);
			
			writer.Write((string)m_Give_Resist_Buff);
			writer.Write((int)m_physicalresistancebonustogive);
			writer.Write((int)m_fireresistancebonusbonustogive);
			writer.Write((int)m_coldresistancebonusbonustogive);
			writer.Write((int)m_poisonresistancebonusbonustogive);
			writer.Write((int)m_energyresistancebonusbonustogive);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_Give_Bless_Buff = reader.ReadString();
			m_strbonustogive = reader.ReadInt();
			m_dexbonustogive = reader.ReadInt();
			m_intbonustogive = reader.ReadInt();
			
			m_Give_Resist_Buff = reader.ReadString();
			m_physicalresistancebonustogive = reader.ReadInt();
			m_fireresistancebonusbonustogive = reader.ReadInt();
			m_coldresistancebonusbonustogive = reader.ReadInt();
			m_poisonresistancebonusbonustogive = reader.ReadInt();
			m_energyresistancebonusbonustogive = reader.ReadInt();
			
		}
	}
}


namespace Server.Misc
{
	public class AllWorldBuffsLoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( AllWorldBuffsEventSink_Login );
		}

		private static void AllWorldBuffsEventSink_Login( LoginEventArgs args )
		{
			Mobile m = args.Mobile;

#region Startup Buffs

#region Server Bless Buff
			if (WorldBuffsController.m_Give_Bless_Buff != "no")//dont change this from no
			{
//				int strbonustogive = 10;//changeing here will change on gump automaticly
//				int dexbonustogive = 10;//changeing here will change on gump automaticly
//				int intbonustogive = 10;//changeing here will change on gump automaticly
				
				m.PlaySound( 0x1E9 );
				m.FixedParticles( 0x375A, 10, 15, 5037, EffectLayer.Waist );
				m.Animate( 20, 5, 1, true, false, 0 );
				m.SendGump(new ServerBlessBuffIconGump(m, WorldBuffsController.m_strbonustogive, WorldBuffsController.m_dexbonustogive, WorldBuffsController.m_intbonustogive));//TRYING TO PASS OVER
				
//				string args1 = String.Format("{0}\t{1}\t{2}", strbonustogive, dexbonustogive, intbonustogive);
				m.AddStatMod( new StatMod( StatType.Str, "ServerBuffstr", WorldBuffsController.m_strbonustogive, TimeSpan.FromDays( 1.0 ) ) );
				m.AddStatMod( new StatMod( StatType.Dex, "ServerBuffdex", WorldBuffsController.m_dexbonustogive, TimeSpan.FromDays( 1.0 ) ) );
				m.AddStatMod( new StatMod( StatType.Int, "ServerBuffint", WorldBuffsController.m_intbonustogive, TimeSpan.FromDays( 1.0 ) ) );
			}
#endregion Server Bless Buff

#region Server Resistance Buff
			if (WorldBuffsController.m_Give_Resist_Buff != "no")//dont change from no
			{
//				should not give more then 10 points, anything higher would be over kill for players
//				int physicalresistancebonustogive = 10;// How many bonus points to give
//				int fireresistancebonusbonustogive = 10;// How many bonus points to give
//				int coldresistancebonusbonustogive = 10;// How many bonus points to give
//				int poisonresistancebonusbonustogive = 10;// How many bonus points to give
//				int energyresistancebonusbonustogive = 10;// How many bonus points to give
				
				m.PlaySound( 0x1E9 );
				m.FixedParticles( 0x375A, 10, 15, 5037, EffectLayer.Waist );
				m.Animate( 20, 5, 1, true, false, 0 );
				m.SendGump(new ServerResistanceBuffIconGump(m, WorldBuffsController.m_physicalresistancebonustogive, WorldBuffsController.m_fireresistancebonusbonustogive, WorldBuffsController.m_coldresistancebonusbonustogive, WorldBuffsController.m_poisonresistancebonusbonustogive, WorldBuffsController.m_energyresistancebonusbonustogive));
				
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Physical, WorldBuffsController.m_physicalresistancebonustogive) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Fire, WorldBuffsController.m_fireresistancebonusbonustogive) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Cold, WorldBuffsController.m_coldresistancebonusbonustogive) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Poison, WorldBuffsController.m_poisonresistancebonusbonustogive) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Energy, WorldBuffsController.m_energyresistancebonusbonustogive) ) ;
			}
#endregion Server Resistance Buff

#region Reset resistances
				BuffInfo.RemoveBuff(m, BuffIcon.ReactiveArmor);
				BuffInfo.RemoveBuff(m, BuffIcon.MagicReflection);
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Physical, 0) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Fire,0) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Cold,0) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Poison,0) );
				m.AddResistanceMod( new ResistanceMod( ResistanceType.Energy,0) ) ;
#endregion Reset resistances


#endregion Startup Buffs
			

#region Tintamar's Page In Queue
			if ( m.AccessLevel >= AccessLevel.Counselor )
			{
				Server.Engines.Help.PageQueue.Pages_OnCalled( m );
				m.Send( SpeedControl.MountSpeed );
				m.SendMessage( 68,"Speed boost has been enabled." );
				m.Hits = 1000000;
				m.Mana = 1000000;
				m.Stam = 1000000;
				m.SendMessage( 68,"You HP, Mana, and Stamina, has been fully charged." );
			}
#endregion Tintamar's Page In Queue
		}
	}
}

namespace Server.Gumps
{
#region Server Bless Buff Icon Gump
    public class ServerBlessBuffIconGump : Gump
    {
    	private int loop1_strbonustogive;//loops to get info to gump
    	private int loop1_dexbonustogive;
    	private int loop1_intbonustogive;

    	public ServerBlessBuffIconGump(Mobile m, int m_intbonustogive, int m_dexbonustogive, int m_strbonustogive) : base(0, 0)//needed to reverse order cause different file.
    		{
    		loop1_strbonustogive = m_strbonustogive;//loops to get info to gump
    		loop1_dexbonustogive = m_dexbonustogive;
    		loop1_intbonustogive = m_intbonustogive;
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;
            AddPage(0);
            AddImage(635, 2, 30013);
            AddImage(635, 2, 30087);
            AddButton(642, 34, 22153, 22153, 0, GumpButtonType.Reply, 0);//Help?
            AddButton(635, 2, 30087, 30087, 1, GumpButtonType.Reply, 0);//Buff info
            }
    	
    	public override void OnResponse(NetState sender, RelayInfo info )
    		{
    		Mobile from = sender.Mobile;
    		
    		if (info.ButtonID == 0)
    			{
    			from.SendSound (85);
    			from.CloseGump(typeof(ServerBlessBuffIconGump));
    			from.CloseGump(typeof(ServerBlessBuffIconHelpGump));
	   			from.SendGump(new ServerBlessBuffIconGump(from, loop1_intbonustogive, loop1_dexbonustogive, loop1_strbonustogive));
    			from.SendGump(new ServerBlessBuffIconHelpGump(from, loop1_intbonustogive, loop1_dexbonustogive, loop1_strbonustogive));
    			}
    		if (info.ButtonID == 1)
    			{
    			from.SendSound (85);
    			from.CloseGump(typeof(ServerBlessBuffIconGump));
      			from.CloseGump(typeof(ServerBlessBuffIconHelpGump));
    			from.SendGump(new ServerBlessBuffIconGump(from, loop1_intbonustogive, loop1_dexbonustogive, loop1_strbonustogive));
    			from.SendGump(new ServerBlessBuffIconHelpGump(from, loop1_intbonustogive, loop1_dexbonustogive, loop1_strbonustogive));
    			}
    		}
    	}
#endregion Server Bless Buff Icon Gump
#region Server Bless Buff Icon Help Gump
    public class ServerBlessBuffIconHelpGump : Gump
    {
    	private int loop2_strbonustogive;//loops to get info to gump
    	private int loop2_dexbonustogive;
    	private int loop2_intbonustogive;
    	
    	public ServerBlessBuffIconHelpGump(Mobile m, int loop1_intbonustogive, int loop1_dexbonustogive, int loop1_strbonustogive) : base(300, 90)
    		{
    		loop2_strbonustogive = loop1_strbonustogive;//loops to get info to gump
    		loop2_dexbonustogive = loop1_dexbonustogive;
    		loop2_intbonustogive = loop1_intbonustogive;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
			this.AddPage(0);
			this.AddImage(0, 0, 1249);
			this.AddLabel(157, 43, 1160, @"Server Buff");
			this.AddLabel(63, 71, 0, @"A special server buff has been given to you");
			
			this.AddImage(93, 38, 30013);
			this.AddImage(92, 36, 30087);
			this.AddImage(284, 38, 30013);
			this.AddImage(283, 36, 30087);
			
			this.AddLabel(166, 94, 0, @"Strength");
			this.AddLabel(181, 206, 0, String.Format("+{0}", loop2_strbonustogive));
			
			
			this.AddLabel(163, 137, 0, @"Dexterity");
			this.AddLabel(181, 161, 0, String.Format("+{0}", loop2_dexbonustogive));
			
			this.AddLabel(160, 184, 0, @"Intelligence");
			this.AddLabel(181, 115, 0, String.Format("+{0}", loop2_intbonustogive));
			
			
			this.AddLabel(154, 262, 0, @"Close Window");
			AddButton(168, 232, 1305, 1306, 0, GumpButtonType.Reply, 0);//Done Button

            }
    	
    	public override void OnResponse(NetState sender, RelayInfo info)
    		{
    		Mobile from = sender.Mobile;
    		
    		if (info.ButtonID == 0)
    			{
    			from.SendSound (85);
    			from.CloseGump(typeof(ServerBlessBuffIconHelpGump));
    			}
    		}
    	}
#endregion  Server Bless Buff Icon Help Gump
#region Server Resistance Buff Icon Gump
    public class ServerResistanceBuffIconGump : Gump
    {
    	private int loop1_physicalresistancebonustogive;
    	private int loop1_fireresistancebonusbonustogive;
    	private int loop1_coldresistancebonusbonustogive;
    	private int loop1_poisonresistancebonusbonustogive;
    	private int loop1_energyresistancebonusbonustogive;
    	
    	public ServerResistanceBuffIconGump(Mobile m, int m_physicalresistancebonustogive, int m_fireresistancebonusbonustogive, int m_coldresistancebonusbonustogive, int m_poisonresistancebonusbonustogive, int m_energyresistancebonusbonustogive) : base(0, 0)
    		{
    		
    		loop1_physicalresistancebonustogive = m_physicalresistancebonustogive;
	    	loop1_fireresistancebonusbonustogive = m_fireresistancebonusbonustogive;
    		loop1_coldresistancebonusbonustogive = m_coldresistancebonusbonustogive;
    		loop1_poisonresistancebonusbonustogive = m_poisonresistancebonusbonustogive;
			loop1_energyresistancebonusbonustogive = m_energyresistancebonusbonustogive;

    		
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;
            AddPage(0);
            AddImage(668, 2, 30041);
            AddImage(668, 2, 30087);
            AddButton(675, 34, 22153, 22153, 0, GumpButtonType.Reply, 0);//help?
            AddButton(668, 2, 30087, 30087, 1, GumpButtonType.Reply, 0);//Buff info
            }
    	
    	public override void OnResponse(NetState sender, RelayInfo info)
    		{
    		Mobile from = sender.Mobile;
    		
    		if (info.ButtonID == 0)
    			{
    			from.SendSound (85);
    			from.CloseGump(typeof(ServerResistanceBuffIconGump));
    			from.CloseGump(typeof(ServerResistanceBuffIconHelpGump));
    			from.SendGump(new ServerResistanceBuffIconGump(from, loop1_physicalresistancebonustogive, loop1_fireresistancebonusbonustogive, loop1_coldresistancebonusbonustogive, loop1_poisonresistancebonusbonustogive, loop1_energyresistancebonusbonustogive));
    			from.SendGump(new ServerResistanceBuffIconHelpGump(from, loop1_physicalresistancebonustogive, loop1_fireresistancebonusbonustogive, loop1_coldresistancebonusbonustogive, loop1_poisonresistancebonusbonustogive, loop1_energyresistancebonusbonustogive));
    			}
    		
    		if (info.ButtonID == 1)
    			{
    			from.SendSound (85);
    			from.CloseGump(typeof(ServerResistanceBuffIconGump));
    			from.CloseGump(typeof(ServerResistanceBuffIconHelpGump));
    			from.SendGump(new ServerResistanceBuffIconGump(from, loop1_physicalresistancebonustogive, loop1_fireresistancebonusbonustogive, loop1_coldresistancebonusbonustogive, loop1_poisonresistancebonusbonustogive, loop1_energyresistancebonusbonustogive));
    			from.SendGump(new ServerResistanceBuffIconHelpGump(from, loop1_physicalresistancebonustogive, loop1_fireresistancebonusbonustogive, loop1_coldresistancebonusbonustogive, loop1_poisonresistancebonusbonustogive, loop1_energyresistancebonusbonustogive));
    			}
    		}
    	}
#endregion Server Resistance Buff Icon Gump
#region Server Resistance Buff Icon Help Gump
    public class ServerResistanceBuffIconHelpGump : Gump
    {
    	private int loop2_physicalresistancebonustogive;
    	private int loop2_fireresistancebonusbonustogive;
    	private int loop2_coldresistancebonusbonustogive;
    	private int loop2_poisonresistancebonusbonustogive;
    	private int loop2_energyresistancebonusbonustogive;

    	public ServerResistanceBuffIconHelpGump(Mobile m, int loop1_physicalresistancebonustogive, int loop1_fireresistancebonusbonustogive, int loop1_coldresistancebonusbonustogive, int loop1_poisonresistancebonusbonustogive, int loop1_energyresistancebonusbonustogive) : base(300, 90)
    		{
    		loop2_physicalresistancebonustogive = loop1_physicalresistancebonustogive;
	    	loop2_fireresistancebonusbonustogive = loop1_fireresistancebonusbonustogive;
    		loop2_coldresistancebonusbonustogive = loop1_coldresistancebonusbonustogive;
    		loop2_poisonresistancebonusbonustogive = loop1_poisonresistancebonusbonustogive;
			loop2_energyresistancebonusbonustogive = loop1_energyresistancebonusbonustogive;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
			this.AddPage(0);
			this.AddImage(0, 0, 1249);
			this.AddLabel(157, 43, 1160, @"Server Buff");
			this.AddLabel(63, 71, 0, @"A special server buff has been given to you");


			
			this.AddImage(92, 36, 30087);//gold icon frame left
			this.AddImage(283, 36, 30087);//gold icon frame right

			this.AddImage(93, 38, 30041);//icons left
			this.AddImage(284, 38, 30041);//icons right
			
			this.AddLabel(54, 105, 0, @"Physical Resist");
			this.AddLabel(89, 125, 0, String.Format("+{0}", loop2_physicalresistancebonustogive));//under physical
			this.AddLabel(271, 105, 0, @"Fire Resist");
			this.AddLabel(289, 125, 0, String.Format("+{0}", loop2_fireresistancebonusbonustogive));//under fire
			this.AddLabel(161, 141, 0, @"Cold Resist");
			this.AddLabel(184, 162, 0, String.Format("+{0}", loop2_coldresistancebonusbonustogive));//under cold
			this.AddLabel(54, 201, 0, @"Poison Resist");
			this.AddLabel(89, 221, 0, String.Format("+{0}", loop2_poisonresistancebonusbonustogive));//under poison
			this.AddLabel(253, 206, 0, @"Energy Resist");
			this.AddLabel(289, 221, 0, String.Format("+{0}", loop2_energyresistancebonusbonustogive));//under energy


			
			
			this.AddLabel(154, 262, 0, @"Close Window");
			AddButton(168, 232, 1305, 1306, 0, GumpButtonType.Reply, 0);//Done Button

            }
    	
    	public override void OnResponse(NetState sender, RelayInfo info)
    		{
    		Mobile from = sender.Mobile;
    		
    		if (info.ButtonID == 0)
    			{
    			from.SendSound (85);
    			from.CloseGump(typeof(ServerResistanceBuffIconHelpGump));
    			}
    		}
    	}
#endregion Server Resistance Buff Icon Help Gump
}


