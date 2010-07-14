using System;

namespace Server.Items
{
	
	public class CarpetRedSouthEastPart : Item
	{
		[Constructable]
		public CarpetRedSouthEastPart() : base( 2761 )
		{
			Weight = 1.0;
		}

		public CarpetRedSouthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
