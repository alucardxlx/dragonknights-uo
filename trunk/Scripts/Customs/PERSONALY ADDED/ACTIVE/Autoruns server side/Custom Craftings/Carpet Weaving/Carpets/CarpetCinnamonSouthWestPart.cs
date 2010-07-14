using System;

namespace Server.Items
{
	
	public class CarpetCinnamonSouthWestPart : Item
	{
		[Constructable]
		public CarpetCinnamonSouthWestPart() : base( 2789 )
		{
			Weight = 1.0;
		}

		public CarpetCinnamonSouthWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
