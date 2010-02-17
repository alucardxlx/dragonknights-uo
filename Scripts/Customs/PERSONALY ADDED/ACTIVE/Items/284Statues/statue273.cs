using System;

namespace Server.Items
{
	public class Statue273 : Item
	{
		[Constructable]
		public Statue273() : base( 0x212C )
		{
			Weight = 1.0;
		}

		public Statue273( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}