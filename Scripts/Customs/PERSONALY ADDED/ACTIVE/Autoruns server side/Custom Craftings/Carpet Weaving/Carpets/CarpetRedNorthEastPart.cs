using System;

namespace Server.Items
{
	
	public class CarpetRedNorthEastPart : Item
	{
		[Constructable]
		public CarpetRedNorthEastPart() : base( 2764 )
		{
			Weight = 1.0;
		}

		public CarpetRedNorthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
