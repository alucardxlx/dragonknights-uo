using System;

namespace Server.Items
{
	
	public class CarpetPinkSouthEastPart : Item
	{
		[Constructable]
		public CarpetPinkSouthEastPart() : base( 2798 )
		{
			Weight = 1.0;
		}

		public CarpetPinkSouthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
