using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueCenterAAPart : Item
	{
		[Constructable]
		public CarpetPlainBlueCenterAAPart() : base( 2749 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueCenterAAPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
