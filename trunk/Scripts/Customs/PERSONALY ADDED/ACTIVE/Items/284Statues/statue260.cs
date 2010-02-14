using System;

namespace Server.Items
{
	public class Statue260 : Item
	{
		[Constructable]
		public Statue260() : base( 0x211D )
		{
			Weight = 1.0;
		}

		public Statue260( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
