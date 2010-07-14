using System;

namespace Server.Items
{
	
	public class CarpetCinnamonSouthEastPart : Item
	{
		[Constructable]
		public CarpetCinnamonSouthEastPart() : base( 2787 )
		{
			Weight = 1.0;
		}

		public CarpetCinnamonSouthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
