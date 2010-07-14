using System;

namespace Server.Items
{
	
	public class CarpetCinnamonNorthEastPart : Item
	{
		[Constructable]
		public CarpetCinnamonNorthEastPart() : base( 2790 )
		{
			Weight = 1.0;
		}

		public CarpetCinnamonNorthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
