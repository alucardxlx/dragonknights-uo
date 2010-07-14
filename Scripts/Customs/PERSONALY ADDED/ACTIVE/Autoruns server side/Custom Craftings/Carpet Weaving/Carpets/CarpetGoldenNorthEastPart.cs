using System;

namespace Server.Items
{
	
	public class CarpetGoldenNorthEastPart : Item
	{
		[Constructable]
		public CarpetGoldenNorthEastPart() : base( 2782 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenNorthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
