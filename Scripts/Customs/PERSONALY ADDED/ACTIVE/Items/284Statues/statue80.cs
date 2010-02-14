using System;

namespace Server.Items
{
	public class Statue80 : Item
	{
		[Constructable]
		public Statue80() : base( 0x25CF )
		{
			Weight = 1.0;
		}

		public Statue80( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
