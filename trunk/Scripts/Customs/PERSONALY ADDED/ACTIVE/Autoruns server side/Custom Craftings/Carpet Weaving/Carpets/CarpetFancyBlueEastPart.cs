using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueEastPart : Item
	{
		[Constructable]
		public CarpetFancyBlueEastPart() : base( 2776 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
