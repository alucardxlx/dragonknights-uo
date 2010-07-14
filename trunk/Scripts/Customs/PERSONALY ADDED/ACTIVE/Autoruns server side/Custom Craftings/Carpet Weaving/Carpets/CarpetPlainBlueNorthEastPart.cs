using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueNorthEastPart : Item
	{
		[Constructable]
		public CarpetPlainBlueNorthEastPart() : base( 2757 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueNorthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
