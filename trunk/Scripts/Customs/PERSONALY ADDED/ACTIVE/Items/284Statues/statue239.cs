using System;

namespace Server.Items
{
	public class Statue239 : Item
	{
		[Constructable]
		public Statue239() : base( 0x20FC )
		{
			Weight = 1.0;
		}

		public Statue239( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
