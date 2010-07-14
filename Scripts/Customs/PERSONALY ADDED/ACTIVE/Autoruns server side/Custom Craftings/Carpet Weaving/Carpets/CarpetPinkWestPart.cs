using System;

namespace Server.Items
{
	
	public class CarpetPinkWestPart : Item
	{
		[Constructable]
		public CarpetPinkWestPart() : base( 2802 )
		{
			Weight = 1.0;
		}

		public CarpetPinkWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
