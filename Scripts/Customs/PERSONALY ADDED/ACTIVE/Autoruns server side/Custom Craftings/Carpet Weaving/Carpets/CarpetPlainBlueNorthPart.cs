using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueNorthPart : Item
	{
		[Constructable]
		public CarpetPlainBlueNorthPart() : base( 2807 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueNorthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
