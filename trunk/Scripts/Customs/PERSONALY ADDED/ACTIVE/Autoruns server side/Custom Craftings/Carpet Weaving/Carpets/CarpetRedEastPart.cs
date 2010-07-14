using System;

namespace Server.Items
{
	
	public class CarpetRedEastPart : Item
	{
		[Constructable]
		public CarpetRedEastPart() : base( 2767 )
		{
			Weight = 1.0;
		}

		public CarpetRedEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
