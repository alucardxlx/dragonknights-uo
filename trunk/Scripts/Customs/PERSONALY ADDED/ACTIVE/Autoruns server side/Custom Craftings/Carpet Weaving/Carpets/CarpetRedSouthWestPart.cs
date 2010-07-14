using System;

namespace Server.Items
{
	
	public class CarpetRedSouthWestPart : Item
	{
		[Constructable]
		public CarpetRedSouthWestPart() : base( 2763 )
		{
			Weight = 1.0;
		}

		public CarpetRedSouthWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
