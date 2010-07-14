using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueWestPart : Item
	{
		[Constructable]
		public CarpetFancyBlueWestPart() : base( 2774 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
