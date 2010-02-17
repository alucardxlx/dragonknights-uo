//Made by Sydruat
//Thanks goes to Lord_Greywolf and TheRockstar2253 for help getting this version working
using System;
using System.Xml;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
	public class BankBell : Item
	{
		[Constructable]
		public BankBell() : base( 0x1C12 )
		{
			Name = "Bank Bell";
			Hue = Utility.RandomList( 1194, 1153, 1176, 1157, 1161, 1174, 1173 );
			Weight = 0;
			LootType=LootType.Blessed;
		}
		public BankBell( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Region is TownRegion )
			{
				from.SendMessage( "You are not allowed to use this in town" );
			}
			else
			{
				if ( IsChildOf( from.Backpack ) )
				{
					BankBox box = from.BankBox;

					if ( box != null )
					{
						box.Open();
					}
					else 
					{
						from.SendMessage( "An internal error has occurred. You need to report this to a system Administrator." );
					}
				}
				else
				{
					from.SendMessage( "This item must be in your pack before you are able to use it" );
				}
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
}