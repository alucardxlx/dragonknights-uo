using System;

namespace Server.Items
{
	
	public class CarpetPinkNorthEastPart : Item
	{
		[Constructable]
		public CarpetPinkNorthEastPart() : base( 2801 )
		{
			Weight = 1.0;
		}

		public CarpetPinkNorthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
