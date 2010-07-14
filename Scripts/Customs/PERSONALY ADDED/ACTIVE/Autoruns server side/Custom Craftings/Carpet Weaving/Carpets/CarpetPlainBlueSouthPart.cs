using System;

namespace Server.Items
{
	
	public class CarpetPlainBlueSouthPart : Item
	{
		[Constructable]
		public CarpetPlainBlueSouthPart() : base( 2809 )
		{
			Weight = 1.0;
		}

		public CarpetPlainBlueSouthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
