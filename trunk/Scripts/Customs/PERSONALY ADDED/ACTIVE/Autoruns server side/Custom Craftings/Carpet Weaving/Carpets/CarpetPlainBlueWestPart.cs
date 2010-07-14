using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueWestPart : Item
	{
		[Constructable]
		public CarpetPlainBlueWestPart() : base( 2806 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
