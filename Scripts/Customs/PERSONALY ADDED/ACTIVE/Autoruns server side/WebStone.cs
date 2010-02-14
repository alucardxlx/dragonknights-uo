using System;
using System.Text;
using Server.Gumps;
using Server.Network;
using Server.Items;

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
		
		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new WebstoneGump() );
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
			this.AddLabel(260, 145, 52, "-DragonKnight's Main WebSite"); //#1 Site's Name
			
			this.AddButton(228, 165, 4023, 4025, 2, GumpButtonType.Reply, 0);
			this.AddLabel(260, 165, 52, "-Forums"); //#2 Site's Name
			
			this.AddButton(228, 185, 4023, 4025, 3, GumpButtonType.Reply, 0);
			this.AddLabel(260, 185, 52, "-Donation"); //#3 Site's Name
			
			this.AddButton(228, 205, 4023, 4025, 4, GumpButtonType.Reply, 0);
			this.AddLabel(260, 205, 52, "-All Around Associate Services"); //#4 Site's Name
			
			this.AddButton(228, 225, 4023, 4025, 5, GumpButtonType.Reply, 0);
			this.AddLabel(260, 225, 52, "-Stratics"); //#5 Site's Name
			
			this.AddButton(228, 245, 4023, 4025, 6, GumpButtonType.Reply, 0);
			this.AddLabel(260, 245, 52, "-UOAM"); //#6 Site's Name

			this.AddButton(228, 265, 4023, 4025, 7, GumpButtonType.Reply, 0);
			this.AddLabel(260, 265, 52, "-Jeff6o6-hosting"); //#7 Site's Name
			
			this.AddButton(228, 285, 4023, 4025, 8, GumpButtonType.Reply, 0);
			this.AddLabel(260, 285, 52, "Vote for us Top100"); //#8 Site's Name

			this.AddButton(228, 305, 4023, 4025, 9, GumpButtonType.Reply, 0);
			this.AddLabel(260, 305, 52, "Vote for us Top200"); //#9 Site's Name
			
			this.AddButton(302, 357, 2453, 4454, 0, GumpButtonType.Reply, 0); // Close Button
			
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
			switch ( info.ButtonID )
			{
				case 1: // #1 Site's Url
					{
						sender.LaunchBrowser( "http://dragonknights.kicks-ass.net/" );
						break;
					}
				case 2: // #2 Site's url
					{
						sender.LaunchBrowser( "http://dragonknights.kicks-ass.net/modules.php?name=Forums" );
						break;
					}
				case 3: // #3 Site's url
					{
						sender.LaunchBrowser( "http://dragonknights.kicks-ass.net/modules.php?name=Donations" );
						break;
					}
				case 4: // #4 Site's Url
					{
						sender.LaunchBrowser( "http://aaaservices.homeip.net/index.html" );
						break;
					}
				case 5: // #5 Site's url
					{
						sender.LaunchBrowser( "http://uo.stratics.com/" );
						break;
					}
				case 6: // #6 Site's url
					{
						sender.LaunchBrowser( "http://www.uoam.net/" );
						break;
					}
				case 7: // #7 Site's url
					{
						sender.LaunchBrowser( "http://www.jeff6o6-hosting.com/" );
						break;
					}
					
				case 8: // #8 Site's url
					{
						sender.LaunchBrowser( "http://www.xtremetop100.com/in.php?site=1132300022" );
						break;
					}

				case 9: // #9 Site's url
					{
						sender.LaunchBrowser( "http://www.gamesites200.com/ultimaonline/in.php?id=1874" );
						break;
					}

			}
		}
	}
}
	
