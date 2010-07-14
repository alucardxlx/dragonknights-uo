using System;

namespace Server.Items
{
	
	public class CarpetGoldenNorthPart : Item
	{
		[Constructable]
		public CarpetGoldenNorthPart() : base( 2784 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenNorthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
