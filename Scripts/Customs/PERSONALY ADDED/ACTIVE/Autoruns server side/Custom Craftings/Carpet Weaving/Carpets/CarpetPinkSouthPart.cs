using System;

namespace Server.Items
{
	
	public class CarpetPinkSouthPart : Item
	{
		[Constructable]
		public CarpetPinkSouthPart() : base( 2805 )
		{
			Weight = 1.0;
		}

		public CarpetPinkSouthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
