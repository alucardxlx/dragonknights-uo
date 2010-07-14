using System;

namespace Server.Items
{
	
	public class CarpetGoldenEastPart : Item
	{
		[Constructable]
		public CarpetGoldenEastPart() : base( 2785 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
