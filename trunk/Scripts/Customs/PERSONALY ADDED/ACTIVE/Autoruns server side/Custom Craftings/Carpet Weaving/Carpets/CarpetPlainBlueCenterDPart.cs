using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueCenterDPart : Item
	{
		[Constructable]
		public CarpetPlainBlueCenterDPart() : base( 2810 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueCenterDPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
