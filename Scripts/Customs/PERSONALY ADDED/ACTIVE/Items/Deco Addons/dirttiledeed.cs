using System;
using Server;
using Server.Items;
using Server.Network; //added to try and make sound and text
using Server.Targeting;




namespace Server.Items
{
	public class DirtTileHouseAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new DirtTileHouseAddonDeed(); } }

		[ Constructable ]
		public DirtTileHouseAddon()
		{

			AddonComponent ac = null;
//			ac = new AddonComponent( 13001 );			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 13001 );			AddComponent( ac, 0, 0, 0 );
//			ac = new AddonComponent( 13001 );			AddComponent( ac, 0, 1, 0 );
//			ac = new AddonComponent( 13001 );			AddComponent( ac, 1, 1, 0 );
		}

		public DirtTileHouseAddon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class DirtTileHouseAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new DirtTileHouseAddon(); } }

		[Constructable]
		public DirtTileHouseAddonDeed()
		{
			Hue = 642;
			Weight= 40.0;
			Name = "Dirt Tile Deed";
		}
//		public override void OnDoubleClick(Mobile from)
//		{
//			base.OnDoubleClick(from);
//		{
//				from.SendMessage("You place the dirt on the floor.");
//				Effects.PlaySound( from.Location, from.Map, 0x201 );
//		}
//		}

//		public override void OnTarget( Mobile from, object target )
//		{
//			Effects.PlaySound( from.Location, from.Map, 0x201);
//			return;
//		}
			
		
		
		public DirtTileHouseAddonDeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}
