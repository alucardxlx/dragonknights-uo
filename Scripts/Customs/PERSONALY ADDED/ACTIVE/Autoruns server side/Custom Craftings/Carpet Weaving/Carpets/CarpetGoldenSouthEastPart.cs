using System;

namespace Server.Items
{
	
	public class CarpetGoldenSouthEastPart : Item
	{
		[Constructable]
		public CarpetGoldenSouthEastPart() : base( 2779 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenSouthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
