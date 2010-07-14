using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueSouthWestPart : Item
	{
		[Constructable]
		public CarpetPlainBlueSouthWestPart() : base( 2756 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueSouthWestPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
