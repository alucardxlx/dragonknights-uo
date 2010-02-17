using System;

namespace Server.Items
{
	public class Statue238 : Item
	{
		[Constructable]
		public Statue238() : base( 0x20FB )
		{
			Weight = 1.0;
		}

		public Statue238( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}