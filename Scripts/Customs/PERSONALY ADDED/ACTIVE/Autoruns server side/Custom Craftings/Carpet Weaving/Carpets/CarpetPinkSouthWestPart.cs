using System;

namespace Server.Items
{
	
	public class CarpetPinkSouthWestPart : Item
	{
		[Constructable]
		public CarpetPinkSouthWestPart() : base( 2800 )
		{
			Weight = 1.0;
		}

		public CarpetPinkSouthWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
