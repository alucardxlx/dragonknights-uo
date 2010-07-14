using System;

namespace Server.Items
{
	
	public class CarpetPinkEastPart : Item
	{
		[Constructable]
		public CarpetPinkEastPart() : base( 2804 )
		{
			Weight = 1.0;
		}

		public CarpetPinkEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
