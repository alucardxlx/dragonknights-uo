using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueNorthPart : Item
	{
		[Constructable]
		public CarpetFancyBlueNorthPart() : base( 2775 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueNorthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
