using System;

namespace Server.Items
{
	
	public class CarpetGoldenWestPart : Item
	{
		[Constructable]
		public CarpetGoldenWestPart() : base( 2783 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
