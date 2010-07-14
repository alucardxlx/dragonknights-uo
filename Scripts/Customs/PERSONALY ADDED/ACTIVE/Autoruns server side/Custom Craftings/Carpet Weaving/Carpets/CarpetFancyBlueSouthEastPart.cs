using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueSouthEastPart : Item
	{
		[Constructable]
		public CarpetFancyBlueSouthEastPart() : base( 2770 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueSouthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
