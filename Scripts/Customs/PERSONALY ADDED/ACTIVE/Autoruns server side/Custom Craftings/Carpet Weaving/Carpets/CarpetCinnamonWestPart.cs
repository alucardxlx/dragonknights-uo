using System;

namespace Server.Items
{
	
	public class CarpetCinnamonWestPart : Item
	{
		[Constructable]
		public CarpetCinnamonWestPart() : base( 2791 )
		{
			Weight = 1.0;
		}

		public CarpetCinnamonWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
