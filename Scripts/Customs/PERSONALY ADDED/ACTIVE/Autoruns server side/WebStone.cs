using System;
using System.Text;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.ContextMenus;//used for context menu
using System.Collections.Generic;//used for context menu
using Server;//using for ingame forms check
using Server.Mobiles;//using for ingame forms check
using Server.Forums;//using for ingame forms check

namespace Server.Gumps
{
	public class WebStone : Item
	{
		[Constructable]
		public WebStone() : base( 0xED6 )
		{
			Movable = false;
			Hue = 0;
			Name = "Double click me to access a menu for the DragonKnight's Website and forms. "; //Change shard name here! 
		}
		public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
                    from.CloseGump(typeof(WebstoneGump));
        }

		
		public override void OnDoubleClick( Mobile from )
		{
			from.CloseGump(typeof(WebstoneGump));
			from.SendSound (1224);// 1224= click noise
			from.SendGump( new WebstoneGump() );
			from.SendSound (240);
		}
		
		public WebStone( Serial serial ) : base( serial )
		{
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
		
		
#region Item Context Menu
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        	{
        	from.SendSound(1224);// 1224= click noise
   			base.GetContextMenuEntries(from, list);
   			list.Add(new MenuEntry(from, this));
        	}


        private class MenuEntry : ContextMenuEntry
        	{
        	private WebStone m_Item;
			private Mobile m_Mobile;

			public MenuEntry(Mobile from, Item item)
				: base(98) //(contextmenu list words to use, uses gothic 3000000+ zone) 3000159=time 3000098=Information  3000362=open 3002083=Open 3006122=Open 3000098=Information
			{
				m_Item = item as WebStone;
				m_Mobile = from;
			}

			public override void OnClick()
			{
			m_Mobile.CloseGump(typeof(WebstoneGump));
        	m_Mobile.SendSound(1224);// 1224= click noise
			m_Mobile.SendGump( new WebstoneGump() );
			m_Mobile.SendSound (240);
			}
        }


#endregion Item Context Menu Stuff
		
		
		
		
	}
}

namespace Server.Gumps
{
	
	public class WebstoneGump : Gump
	{
		
		
		public WebstoneGump() : base( 0, 0 )
		{	
			this.AddPage(0);
			this.AddBackground(213, 131, 234, 258, 5054);

// 			--------------(left-right, up/down, color, "name"
			this.AddButton(228, 145, 4023, 4025, 1, GumpButtonType.Reply, 0);
			this.AddLabel(260, 145, 52, "-DragonKnight's Guild WebSite"); //#1 Site's Name
			
			this.AddButton(228, 165, 4023, 4025, 2, GumpButtonType.Reply, 0);
			this.AddLabel(260, 165, 52, "-WebSite Forums"); //#2 Site's Name
			
			this.AddButton(228, 185, 4023, 4025, 3, GumpButtonType.Reply, 0);
			this.AddLabel(260, 185, 52, "-InGame Forums"); //#3 Site's Name
			
			this.AddButton(228, 205, 4023, 4025, 4, GumpButtonType.Reply, 0);
			this.AddLabel(260, 205, 52, "-UO Stratics"); //#4 Site's Name
			
			this.AddButton(228, 225, 4023, 4025, 5, GumpButtonType.Reply, 0);
			this.AddLabel(260, 225, 52, "-Ultima Online Auto Map"); //#5 Site's Name
			
			this.AddButton(228, 245, 4023, 4025, 6, GumpButtonType.Reply, 0);
			this.AddLabel(260, 245, 52, "-TeamSpeak 2"); //#6 Site's Name

			this.AddButton(228, 265, 4023, 4025, 7, GumpButtonType.Reply, 0);
			this.AddLabel(260, 265, 52, "-Server Status"); //#7 Site's Name
			
			this.AddButton(228, 285, 4023, 4025, 8, GumpButtonType.Reply, 0);
			this.AddLabel(260, 285, 52, "-Vote for us"); //#8 Site's Name

			this.AddButton(228, 305, 4023, 4025, 9, GumpButtonType.Reply, 0);
			this.AddLabel(260, 305, 52, "-All Around Associate Services"); //#9 Site's Name

//			this.AddButton(228, 325, 4023, 4025, 10, GumpButtonType.Reply, 0);
//			this.AddLabel(260, 325, 52, "-Jeff6o6-hosting"); //#10 Site's Name
			
			this.AddButton(293, 359, 12006, 12008, 0, GumpButtonType.Reply, 0); // Close Button
			
			
			
			
			
			this.AddImage(290, 53, 1417);
			this.AddImage(301, 63, 5608);
			this.AddImage(213, 69, 1419);
			this.AddImage(162, 28, 10440);
			this.AddImage(414, 28, 10441);
			this.AddImage(361, 133, 5217);
			this.AddImage(275, 133, 5217);
			this.AddImage(214, 133, 5217);
			
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			switch ( info.ButtonID )
			{
				case 1: // #1 Site's Url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://DragonKnights.homeip.net" );
						break;
					}
				case 2: // #2 Site's url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://dragonknights.kicks-ass.net/e107_plugins/forum/forum.php" );
						break;
					}
				case 3: // #3 Site's url
					{
						AuthorStatistics ast = ForumCore.GetAuthorStatistics( from.Serial.Value );
						if( ast.Banned )
						{
							from.SendMessage( "You have been banned from this forum!" );
							return;
						}
						ForumCore.Threads.Sort( new DateSort() );
						from.CloseGump( typeof( ForumGump ) );
						from.SendSound (1224);// 1224= click noise
						from.SendGump( new ForumGump( from, 0 ) );
						break;
					}
				case 4: // #4 Site's Url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://uo.stratics.com/" );
						break;
					}
				case 5: // #5 Site's url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://dragonknights.kicks-ass.net/download.php?view.3" );
						break;
					}
				case 6: // #6 Site's url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://dragonknights.kicks-ass.net/download.php?view.1" );
						//sender.LaunchBrowser( "http://aaaservices.homeip.net/TeamSpeak.htm" );
						break;
					}
				case 7: // #7 Site's url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://aaaservices.homeip.net/UO/status.html" );
						break;
					}
					
				case 8: // #8 Site's url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://www.xtremetop100.com/in.php?site=1132300022" );
						sender.LaunchBrowser( "http://www.gamesites200.com/ultimaonline/in.php?id=1874" );
						break;
					}

				case 9: // #9 Site's url
					{
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendGump( new WebstoneGump() );
						from.SendSound (1224);// 1224= click noise
						sender.LaunchBrowser( "http://aaaservices.homeip.net/index.html" );
						break;
					}
					

				case 0: // Close button
					{
						from.SendSound (1224);// 1224= click noise
	                    from.CloseGump(typeof(WebstoneGump));
						from.SendSound (240);
						break;
					}
					


//				case 10: // #10 Site's url
//					{
//						sender.LaunchBrowser( "http://www.jeff6o6-hosting.com/" );
//						break;
//					}

			}
		}
	}
}
	
