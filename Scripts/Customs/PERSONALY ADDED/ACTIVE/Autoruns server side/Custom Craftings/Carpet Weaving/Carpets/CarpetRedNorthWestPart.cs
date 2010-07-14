using System;

namespace Server.Items
{
	
	public class CarpetRedNorthWestPart : Item
	{
		[Constructable]
		public CarpetRedNorthWestPart() : base( 2762 )
		{
			Weight = 1.0;
		}

		public CarpetRedNorthWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
