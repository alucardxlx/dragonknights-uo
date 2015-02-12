using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server;
using System.Collections.Generic;
using System.Collections;
using System;
using Server.Network;


namespace Server.Misc
{
	public class HiddenVillageLoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( HiddenVillageEventSink_Login );
		}

		private static void HiddenVillageEventSink_Login( LoginEventArgs args )
		{
			Mobile m = args.Mobile;

			Container pack = m.Backpack;
			
			if ( pack != null )
			{
				List<HiddenVillagePass> passs = pack.FindItemsByType<HiddenVillagePass>();
				
				for ( int i = 0; i < passs.Count; ++i )
				{
					HiddenVillagePass pass = passs[i];
					
					if ( pass != null )
					{
						m.CloseGump( typeof( HiddenVillageIconGump ) );
						m.SendGump( new HiddenVillageIconGump(m) );
					}
				}
			}
			if (m.AccessLevel >= AccessLevel.Counselor)
			{
				m.SendMessage(68,"Because your a GM you get the HV pass!!");
				m.CloseGump( typeof( HiddenVillageIconGump ) );
				m.SendGump(new HiddenVillageIconGump(m));
			}
		}
	}
}


namespace Server.Mobiles
{
	public class HiddenVillagePassNPCGuard : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[ Constructable ]
		public HiddenVillagePassNPCGuard() : base ( "the Hidden Village Pass Guard" )
		{
			RangePerception = 10;
		}

		public override void InitOutfit()
		{
			AddItem(new SpiritualityHelm());
			AddItem(new HonestyGorget());
			AddItem(new JusticeBreastplate());
			AddItem(new CompassionArms());
			AddItem(new ValorGauntlets());
			AddItem(new DupresShield());
			AddItem(new SwordOfJustice());
			AddItem(new HonorLegs());
			AddItem(new SacrificeSollerets());
			AddItem(new HumilityCloak());
		}
	
		public HiddenVillagePassNPCGuard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize (writer);

			writer.Write( 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize (reader);

			reader.ReadInt();
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			list.Add( new HiddenVillagePassNPCGuardContextEntry_Information( this ) );
			list.Add( new HiddenVillagePassNPCGuardContextEntry_Buy( this ) );
			list.Add( new HiddenVillagePassNPCGuardContextEntry_Teleport( this ) );
		}

		public class HiddenVillagePassNPCGuardContextEntry_Information : ContextMenuEntry
		{
			private HiddenVillagePassNPCGuard m_HiddenVillagePassNPCGuard;
			public HiddenVillagePassNPCGuardContextEntry_Information( HiddenVillagePassNPCGuard hiddenvillagenpcguard ) : base( 98, 10 )//3000098;Information, 2032=teleport,6146=talk,6132=use,6103=buy,
			{
				m_HiddenVillagePassNPCGuard = hiddenvillagenpcguard;
			}
			public override void OnClick()
			{
				Mobile m = Owner.From;
				
//				m_HiddenVillagePassNPCGuard.SayTo( m, "ON CLICK-Information-ok" );HiddenVillageIconHelpGump
				m.CloseGump( typeof( HiddenVillageInformationGump ) );
				m.SendGump( new HiddenVillageInformationGump(m) );
			}
		}
		
		public class HiddenVillagePassNPCGuardContextEntry_Buy : ContextMenuEntry
		{
			private HiddenVillagePassNPCGuard m_HiddenVillagePassNPCGuard;
			public HiddenVillagePassNPCGuardContextEntry_Buy( HiddenVillagePassNPCGuard hiddenvillagenpcguard ) : base( 6103, 10 )//3000098;Information, 2032=teleport,6146=talk,6132=use,6103=buy,
			{
				m_HiddenVillagePassNPCGuard = hiddenvillagenpcguard;
			}
			public override void OnClick()
			{
				Mobile m = Owner.From;
				
//				m_HiddenVillagePassNPCGuard.SayTo( m, "ON CLICK-BUY-ok" );
				m.CloseGump( typeof( HiddenVillageBillOfSalePassGump ) );
				m.SendGump( new HiddenVillageBillOfSalePassGump() );
			}
		}
		
		public class HiddenVillagePassNPCGuardContextEntry_Teleport : ContextMenuEntry
		{
			private HiddenVillagePassNPCGuard m_HiddenVillagePassNPCGuard;
			public HiddenVillagePassNPCGuardContextEntry_Teleport( HiddenVillagePassNPCGuard hiddenvillagenpcguard ) : base( 2032, 10 )//3000098;Information, 2032=teleport,6146=talk,6132=use,6103=buy,
			{
				m_HiddenVillagePassNPCGuard = hiddenvillagenpcguard;
			}
			public override void OnClick()
			{
				Mobile m = Owner.From;
				
				Container pack = m.Backpack;
				
				if( pack != null )
				{
					Item pass = pack.FindItemByType( typeof( HiddenVillagePass ));
					
					if( pass != null )
					{
//						m.SendMessage(25,"Found PASS");
						m.PlaySound (41);
						m.MoveToWorld(new Point3D(5153, 1139, 0), Map.Trammel);
						Server.Mobiles.BaseCreature.TeleportPets( m, m.Location, m.Map );
						m.Animate( 20, 5, 1, true, false, 0 );
					}
					if (m.AccessLevel > AccessLevel.GameMaster)
					{
						m.PlaySound (41);
						m.SendMessage(68,"Because you are a GM you shall pass.");
						m.MoveToWorld(new Point3D(5153, 1139, 0), Map.Trammel);
						Server.Mobiles.BaseCreature.TeleportPets( m, m.Location, m.Map );
						m.Animate( 20, 5, 1, true, false, 0 );
						return;
					}
					if (pass == null)
					{
						m.SendGump( new HiddenVillageNPC_YOUSHALLNOTPASSGump() );
					}
				}
				
//				m_HiddenVillagePassNPCGuard.SayTo( m, "ON CLICK-TELEPORT-ok" );
			}
		}

		public override void InitSBInfo()
		{
		}

		public override void OnSpeech(SpeechEventArgs e)
		{
			if ( e.Speech.ToLower().IndexOf( "pass to hidden village" ) > -1 )
			{
				e.Handled = true;
//				SayTo( e.Mobile, "I heard you say :pass to hidden village:" );
				Container pack = e.Mobile.Backpack;
				
				if( pack != null )
				{
					Item pass = pack.FindItemByType( typeof( HiddenVillagePass ));
					
					if( pass != null )
					{
						//        			m.SendMessage(25,"Found PASS");
						e.Mobile.PlaySound (41);
						e.Mobile.MoveToWorld(new Point3D(5153, 1139, 0), Map.Trammel);
						Server.Mobiles.BaseCreature.TeleportPets( e.Mobile, e.Mobile.Location, e.Mobile.Map );
						e.Mobile.Animate( 20, 5, 1, true, false, 0 );
					}
					if (e.Mobile.AccessLevel > AccessLevel.GameMaster)
					{
						e.Mobile.PlaySound (41);
						e.Mobile.SendMessage(68,"Because you are a GM you shall pass.");
						e.Mobile.MoveToWorld(new Point3D(5153, 1139, 0), Map.Trammel);
						Server.Mobiles.BaseCreature.TeleportPets( e.Mobile, e.Mobile.Location, e.Mobile.Map );
						e.Mobile.Animate( 20, 5, 1, true, false, 0 );
						return;
					}
					if (pass == null)
					{
						e.Mobile.SendGump( new HiddenVillageNPC_YOUSHALLNOTPASSGump() );
					}
				}
			}
			
			base.OnSpeech (e);
		}
		
//        public override void OnDoubleClick( Mobile m )
//        {
//        	Container pack = m.Backpack;
//        	
//        	if( pack != null )
//        	{
//        		Item pass = pack.FindItemByType( typeof( HiddenVillagePass ));
//        		
//        		if( pass != null )
//        		{
////        			m.SendMessage(25,"Found PASS");
//    				m.PlaySound (41);
//        			m.MoveToWorld(new Point3D(5153, 1139, 0), Map.Trammel);
//        			Server.Mobiles.BaseCreature.TeleportPets( m, m.Location, m.Map );
//	    			m.Animate( 20, 5, 1, true, false, 0 );
//        		}
//        		if (m.AccessLevel > AccessLevel.GameMaster)
//        		{
//    				m.PlaySound (41);
//        			m.SendMessage(68,"Because you are a GM you shall pass.");
//        			m.MoveToWorld(new Point3D(5153, 1139, 0), Map.Trammel);
//        			Server.Mobiles.BaseCreature.TeleportPets( m, m.Location, m.Map );
// 	    			m.Animate( 20, 5, 1, true, false, 0 );
// 	    			return;
//        		}
//        		if (pass == null)
//        		{
//        			m.SendGump( new HiddenVillageNPC_YOUSHALLNOTPASSGump() );
//        		}
//        	}
//				
//        }
	}
}


namespace Server.Items
{
	public class DragonKnightToken : Item
	{
		[Constructable]
		public DragonKnightToken() : this( 1 )
		{
		}
		
		[Constructable]
        public DragonKnightToken(int amount) : base( 0x12ac )
		{
			Stackable = true;
			Hue = 55;
			Amount = amount;
			Name = "DragonKnight Token";
		}
		public DragonKnightToken( Serial serial ) : base( serial )
		{
		}
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " DragonKnight Tokens" );
			}
			else
			{				
				list.Add( "A DragonKnight Token" );
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class HiddenVillagePass : Item
	{
		private TimeSpan m_LifeSpan;

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan LifeSpan
		{
			get { return m_LifeSpan; }
			set { m_LifeSpan = value; }
		}

		private DateTime m_CreationTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime CreationTime
		{
			get { return m_CreationTime; }
			set { m_CreationTime = value; }
		}

		private Timer m_Timer;

		public override bool Nontransferable { get { return true; } }
		public override void HandleInvalidTransfer( Mobile from )
		{
//			if( InvalidTransferMessage != null )
//				TextDefinition.SendMessageTo( from, InvalidTransferMessage );
			from.SendMessage(38, "You can not do that.");
//			this.Delete();
		}

//		public virtual TextDefinition InvalidTransferMessage { get { return null; } }


		public virtual void Expire( Mobile parent )
		{
			if( parent != null )
//				parent.SendLocalizedMessage( 1072515, (this.Name == null ? String.Format( "#{0}", LabelNumber ): this.Name) ); // The ~1_name~ expired...
				parent.SendMessage (38,"The Hidden Village Pass has expired.");
			parent.CloseGump(typeof(HiddenVillageIconGump));
			parent.CloseGump(typeof(HiddenVillageIconHelpGump));

			Effects.PlaySound( GetWorldLocation(), Map, 0x201 );

			this.Delete();
		}

		public virtual void SendTimeRemainingMessage( Mobile to )
		{
			to.SendLocalizedMessage( 1072516, String.Format( "{0}\t{1}", (this.Name == null ? String.Format( "#{0}", LabelNumber ): this.Name), (int)m_LifeSpan.TotalSeconds ) ); // ~1_name~ will expire in ~2_val~ seconds!
		}

		public override void OnDelete()
		{
			if( m_Timer != null )
				m_Timer.Stop();

			base.OnDelete();
		}

		public virtual void CheckExpiry()
		{
			if( (m_CreationTime + m_LifeSpan) < DateTime.Now )
				Expire( RootParent as Mobile );
			else
				InvalidateProperties();
		}

		[Constructable]
		public HiddenVillagePass( int lifeSpan)
			: this( TimeSpan.FromHours( lifeSpan ))
		{
		}
		[Constructable]
		public HiddenVillagePass(TimeSpan lifeSpan )
//		public HiddenVillagePass( )
			: base( 0x227B )
		{
			Name = "Hidden Village Pass";
			m_CreationTime = DateTime.Now;
			m_LifeSpan = lifeSpan;

			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 5 ), TimeSpan.FromSeconds( 5 ), new TimerCallback( CheckExpiry ) );
		}

		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
//				from.SendMessage( "Need to setup to get info-gump, dont have teleport cause have block on top of screen." ); // That must be in your pack for you to use it.
				from.CloseGump( typeof( HiddenVillagePassGump ) );
				from.SendGump( new HiddenVillagePassGump() );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}
		public HiddenVillagePass( Serial serial )
			: base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
//			base.GetProperties( list );

			TimeSpan remaining = ((m_CreationTime + m_LifeSpan) - DateTime.Now);
			list.Add("Hidden Village Pass");
			list.Add(1074049);//Passage of time
			list.Add( 1072517, ((int)remaining.TotalSeconds).ToString() ); // Lifespan: ~1_val~ seconds
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );

			writer.Write( m_LifeSpan );
			writer.Write( m_CreationTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_LifeSpan = reader.ReadTimeSpan();
			m_CreationTime = reader.ReadDateTime();

			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 5 ), TimeSpan.FromSeconds( 5 ), new TimerCallback( CheckExpiry ) );
		}
	}
}


namespace Server.Gumps
{
    public class HiddenVillageBillOfSalePassGump : Gump
    {
        public HiddenVillageBillOfSalePassGump() : base( 50, 50 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(0, 0, 274, 277, 9380);
			AddImage(23, 39, 53);
			AddImage(22, 51, 50);
			AddImage(104, 51, 50);
			AddImage(64, 39, 54);
			AddImage(22, 185, 50);
			AddImage(105, 185, 50);
			AddImage(71, 204, 93);
			AddImage(225, 204, 94);
			AddLabel(33, 75, 0, @"1");//Amount being sold
			AddImage(20, 172, 51);
			AddItem(59, 75, 0x227B);
			AddImage(135, 204, 93);
			AddLabel(116, 75, 0, @"Hidden Village Pass");
			AddLabel(111, 169, 0, @"DragonKnights Tokens");
			AddButton(65, 197, 12000, 12002, 1, GumpButtonType.Reply, 0);//accept
			AddButton(176, 197, 12018, 12020, 0, GumpButtonType.Reply, 0);//refuse
			AddImage(19, 204, 92);
			AddLabel(82, 169, 0, @"25");//Total amount price of sale = Grand Total-how many token
			AddLabel(100, 3, 0, @"BILL OF SALE");
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

			if ( info.ButtonID == 1 )
			{
				Container pack = from.Backpack;
				if ( pack != null )
                {
					if (pack.ConsumeTotal( typeof( DragonKnightToken ), 25 ))
					{
						List<HiddenVillagePass> hiddenvillagepasss = pack.FindItemsByType<HiddenVillagePass>();
						for ( int i2 = 0; i2 < hiddenvillagepasss.Count; ++i2 )
						{
							HiddenVillagePass hiddenvillagepass = hiddenvillagepasss[i2];
							
							if ( hiddenvillagepass == null )
								continue;
							if ( hiddenvillagepass != null )//how many token
							{
								hiddenvillagepass.LifeSpan = (hiddenvillagepass.LifeSpan + ( TimeSpan.FromDays( 7 )));
								from.SendMessage(68,"The Pass has been updated with more time.");
								return;
							}
						}
						from.SendMessage(68,"The Pass has been added to your backpack.");
						from.AddToBackpack( new HiddenVillagePass(168) );//168 hours = 1 week
						from.CloseGump( typeof( HiddenVillageIconGump ) );
						from.SendGump( new HiddenVillageIconGump(from) );
						return;
					}
					from.SendMessage( 38,"You do not have enough tokens in your bag." );
				}
			}
			
			if ( info.ButtonID == 0 )
			{
//				from.SendMessage( "refused" );
			}
        }
    }
    public class HiddenVillageIconGump : Gump
    {
    	public HiddenVillageIconGump(Mobile m) : base(0, 0)
    	{
    		this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;
            AddPage(0);
            AddImage(612, 2, 11340);// gold ticket - desktop
            AddButton(615, 34, 22153, 22153, 0, GumpButtonType.Reply, 0);//? on desktop
            AddButton(612, 2, 11340, 11340, 1, GumpButtonType.Reply, 0);//ckick on HV pass
    	}    	
    	public override void OnResponse(NetState sender, RelayInfo info)
    	{
    		Mobile from = sender.Mobile;
    		
    		if (info.ButtonID == 0)
    		{
    			from.SendSound (85);
    			from.CloseGump(typeof(HiddenVillageIconGump));
	   			from.CloseGump(typeof(HiddenVillageIconHelpGump));
    			from.SendGump(new HiddenVillageIconGump(from));
    			from.SendGump(new HiddenVillageIconHelpGump(from));
    		}
    		if (info.ButtonID == 1)
    		{
    			if (from.AccessLevel >= AccessLevel.Counselor)
    			{
    				from.PlaySound (41);
    				Server.Mobiles.BaseCreature.TeleportPets( from, new Point3D(5153,1139,0),Map.Trammel);
    				from.MoveToWorld(new Point3D(5153,1139,0),Map.Trammel);
	    			from.CloseGump(typeof(HiddenVillageIconGump));
	    			from.SendGump(new HiddenVillageIconGump(from));
	    			from.SendMessage(68,"Because your a GM I will take you there.");
	    			from.Animate( 20, 5, 1, true, false, 0 );
    			}
    			else
    			{
//    				from.CloseGump(typeof(HiddenVillageIconGump));
//    				from.SendGump(new HiddenVillageIconGump(from));
//    				from.SendMessage(38,"Sorry this has not been implamented yet. So I can not take you there.");
    				from.PlaySound (41);
    				Server.Mobiles.BaseCreature.TeleportPets( from, new Point3D(5153,1139,0),Map.Trammel);
    				from.MoveToWorld(new Point3D(5153,1139,0),Map.Trammel);
    				from.CloseGump(typeof(HiddenVillageIconGump));
    				from.SendGump(new HiddenVillageIconGump(from));
//    				from.SendMessage(68,"HV is still being worked on by the GMs.");
    				from.Animate( 20, 5, 1, true, false, 0 );
    			}
    		}
    	}
    }
    public class HiddenVillageInformationGump : Gump
    {
    	public HiddenVillageInformationGump(Mobile m) : base(50, 50)
    	{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 13);
			AddImage(1, 0, 50869);
			AddImage(1, 1, 50870);
			AddImage(1, -1, 50871);
			AddImage(1, 0, 50876);
			AddImage(1, -2, 50872);
			AddImage(2, 0, 50873);
			AddImage(1, -8, 50874);
			AddImage(2, -4, 50875);
			AddImage(9, -14, 50888);
			AddImage(1, 1, 50940);
			AddBackground(165, 11, 413, 195, 2600);
			AddLabel(331, 24, 1160, @"Hidden Village");
			AddHtml( 186, 53, 374, 111, @"<basefont color =#080808><b>The Hidden Village:</b> Initially it was an area for the Elders. Mysterious as it is, actualy it has turned into a safe haven. Where vendors from all around the world have come to sell rare items that you may not be able to find anywhere else in the world. I would not be supprised if you find something that has long been lost, or forgotten. As for the elders, they call it a forgotten place.

<b>Accessing The Hidden Village:</b> In order for you to gain access to the hidden village you need to have a pass. Once you have a pass you will have several ways of accessing the hidden village. You will have a pass icon placed on your desktop, you can select <i><b>TELEPORT</b></i> on the menu on the npc, be in close enough range to the npc and say the words <i>pass to hidden village</i>, or by double left clicking the npc. Originally only given to the prestige. The pass is vallid only for a certain amount of time. So make sure to make good use of it before it expires.

<b>Passage Icon:</b> Once you have received a pass, there will also be a icon placed to the top of your sceen. If clicked on, this icon will automaticly teleport you directly to the hidden village. The icon will only be there as long as you have a pass.</basefont>", (bool)false, (bool)true);
			AddButton(457, 171, 12006, 12008, 0, GumpButtonType.Reply, 0);
			AddButton(211, 169, 2501, 2501, 1, GumpButtonType.Reply, 0);
			AddLabel(258, 169, 0, @"Buy Pass");
			

            
    	}
    	
    	public override void OnResponse(NetState sender, RelayInfo info)
        {
    		Mobile from = sender.Mobile;
    		
            switch(info.ButtonID)
            {
            	case 0:
            		{
            			break;
            		}
            	case 1:
            		{
            			from.CloseGump( typeof( HiddenVillageInformationGump ) );
            			from.SendGump( new HiddenVillageBillOfSalePassGump() );
            			break;
            		}
            }
        }
    }
    public class HiddenVillageIconHelpGump : Gump
    {
    	public HiddenVillageIconHelpGump(Mobile m) : base(300, 90)
    	{
    		this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
			this.AddPage(0);
			this.AddImage(0, 0, 1249);//scroll background
			this.AddLabel(135, 43, 1160, @"Hidden Village Pass");
			this.AddLabel(51, 71, 0, @"Special access privileges have been given to the");
			this.AddLabel(52, 93, 0, @"character. Wich will allow you to go to a special");
			this.AddLabel(59, 115, 0, @" hidden village. In the village you can access");
			this.AddLabel(39, 137, 0, @" special npcs and items that you would not be able");
			this.AddLabel(127, 159, 0, @" to obtain in normal areas.");
			this.AddLabel(79, 184, 0, @"- Clicking on the HV Pass Icon on the");
			this.AddImage(93, 38, 11340);// gold ticket
			this.AddImage(284, 38, 11340);// gold ticket
			this.AddImage(47, 179, 30087);//surround icon box
			this.AddImage(52, 179, 11340);// gold ticket
			this.AddLabel(84, 206, 0, @"desktop will take you directly to the HV.");
			this.AddLabel(154, 262, 0, @"Close Window");
			AddButton(171, 232, 1305, 1306, 0, GumpButtonType.Reply, 0);//Done Button
    	}
    	
    	public override void OnResponse(NetState sender, RelayInfo info)
    	{
    		Mobile from = sender.Mobile;
    		
    		if (info.ButtonID == 0)
    		{
    			from.SendSound (85);
    			from.CloseGump(typeof(HiddenVillageIconHelpGump));
    		}
    	}
    }
    public class HiddenVillageNPC_YOUSHALLNOTPASSGump : Gump
    {
    	public HiddenVillageNPC_YOUSHALLNOTPASSGump() : base( 50, 50 )
    	{
    		this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 13);
			AddImage(1, 0, 50869);
			AddImage(1, 1, 50870);
			AddImage(1, -1, 50871);
			AddImage(1, -7, 50876);
			AddImage(1, -2, 50872);
			AddImage(2, 0, 50873);
			AddImage(1, -8, 50874);
			AddImage(2, -4, 50875);
			AddImage(9, -14, 50888);
			AddImage(1, 1, 50940);
			AddBackground(165, 11, 413, 195, 2600);
			AddLabel(308, 24, 38, @"You Shall Not Pass!");
			AddHtml( 186, 53, 374, 111, @"You do not have a Hidden Village Pass.

If you did have one, I would have automaticly teleported you to the hidden village.

Proceed to buy a Hidden Village Pass?", (bool)false, (bool)false);
			AddButton(211, 169, 2076, 2075, 1, GumpButtonType.Reply, 0);
			AddButton(475, 169, 2073, 2072, 0, GumpButtonType.Reply, 0);
			

            
        }
        
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
            	case 0:
            		{
            			break;
            		}
            	case 1:
            		{
            			from.CloseGump( typeof( HiddenVillageNPC_YOUSHALLNOTPASSGump ) );
            			from.SendGump( new HiddenVillageBillOfSalePassGump() );
            			break;
            		}
            }
        }
    }
	public class HiddenVillagePassGump : Gump
	{
		public HiddenVillagePassGump() : base( 150, 50 )
		{
			AddPage( 0 );

			AddImage( 0, 0, 1228 );
			AddImage( 340, 255, 9005 );
			AddLabel(133, 3, 1160, @"Hidden Village Pass");
			AddButton(171, 261, 247, 248, 0, GumpButtonType.Reply, 0);

			AddHtml( 25, 36, 350, 210,@"<basefont color =#080808><i><b>BY THE ORDER OF THE ELDERS:</b></i>

Let it be known to all! The owner of this special pass has been given full access rights threw:<br><br><I>The Passage of Time</I>.<br><br>Guardians shall not stop passage.</basefont>", (bool)false, (bool)false);
		}
		public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
            		case 0:
            		{
            			from.CloseGump( typeof( HiddenVillagePassGump ) );
            			break;
            		}
            }
		}

	}
}
