using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueCenterPart : Item
	{
		[Constructable]
		public CarpetFancyBlueCenterPart() : base( 2769 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueCenterPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
