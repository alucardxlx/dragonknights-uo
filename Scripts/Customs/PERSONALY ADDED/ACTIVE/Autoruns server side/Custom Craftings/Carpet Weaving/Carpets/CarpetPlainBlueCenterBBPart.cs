using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueCenterBBPart : Item
	{
		[Constructable]
		public CarpetPlainBlueCenterBBPart() : base( 2753 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueCenterBBPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
