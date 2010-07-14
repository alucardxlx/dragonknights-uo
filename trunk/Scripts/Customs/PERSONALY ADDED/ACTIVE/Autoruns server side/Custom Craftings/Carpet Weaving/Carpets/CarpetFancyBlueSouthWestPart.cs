using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueSouthWestPart : Item
	{
		[Constructable]
		public CarpetFancyBlueSouthWestPart() : base( 2772 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueSouthWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
