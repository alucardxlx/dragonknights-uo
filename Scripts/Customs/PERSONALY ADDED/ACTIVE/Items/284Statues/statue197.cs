using System;

namespace Server.Items
{
	public class Statue197 : Item
	{
		[Constructable]
		public Statue197() : base( 0x20D1 )
		{
			Weight = 1.0;
		}

		public Statue197( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}