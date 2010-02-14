using System;

namespace Server.Items
{
	public class Statue99 : Item
	{
		[Constructable]
		public Statue99() : base( 0x25FC )
		{
			Weight = 1.0;
		}

		public Statue99( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
