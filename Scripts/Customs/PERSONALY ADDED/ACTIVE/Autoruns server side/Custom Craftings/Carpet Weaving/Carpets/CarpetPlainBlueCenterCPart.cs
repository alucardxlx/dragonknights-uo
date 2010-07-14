using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueCenterCPart : Item
	{
		[Constructable]
		public CarpetPlainBlueCenterCPart() : base( 2750 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueCenterCPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
