using System;

namespace Server.Items
{
	
	public class CarpetGoldenCenterPart : Item
	{
		[Constructable]
		public CarpetGoldenCenterPart() : base( 2778 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenCenterPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
